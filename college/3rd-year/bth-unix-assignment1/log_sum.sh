#!/bin/sh

old=$IFS
IFS=$'\n'
blacklist=()
option=0
lines_count=0

# Check if the limit of results has been reach
# Returned status code: 0 -> limit not reached, 1 -> limit reached
check_limit() {
    if [ ! -z $limit ]; then
        if [ $limit -eq $lines_count ]; then
            return 1
        fi
    fi
    return 0
}

# Print the results to stdout
# Arguments: 1 -> array of results
print_results() {
    out=$1
    for out_line in "${out[@]}"; do
        check_limit
        [ $? -ne 0 ] && break
        echo -n "$out_line"

        check_if_blacklisted_ip "$out_line"
        [ $? -ne 0 ] && echo -ne "\t*Blacklisted!*"

        echo ""
        lines_count=$((lines_count+1))
    done
}

rank_ip_most_connection_attempts() {
    out=($(cat $filename | cut -d' ' -f1 | uniq -c | sort -unr | uniq -f 1 | awk '{printf ("%s\t%s\n", $2, $1)}'))
    print_results $out
}

rank_ip_most_successful_attempts() {
    out=($(cat $filename | cut -d' ' -f1,9 | grep -E " 2[0-9]{2}" | cut -d' ' -f1 | uniq -c | sort -nr | uniq -f 1 | awk '{printf ("%s\t%s\n", $2, $1)}'))
    print_results $out
}

rank_ip_most_common_http_codes() {
    out=($(cat thttpd.log | cut -d' ' -f1,9 | awk '{printf ("%s\t%s\n", $2, $1)}' | sort -nr | uniq -c))
    prev_code=$(echo "${out[0]}" | awk '{print $2}')
    same_http_code_lines=()
    results=()

    for line in "${out[@]}"; do
        code=$(echo $line | awk '{print $2}')

        if [ "$prev_code" != "$code" ]; then
            sorted=($(sort -nr <<<"${same_http_code_lines[*]}"))
            for sorted_line in "${sorted[@]}"; do
                check_limit
                [ $? -ne 0 ] && break

                echo -n $sorted_line | awk '{printf ("%s\t%s", $2, $3)}'
                check_if_blacklisted_ip "$sorted_line"
                [ $? -ne 0 ] && echo -ne "\t*Blacklisted!*"

                echo ""
                lines_count=$((lines_count+1))
            done

            check_limit
            [ $? -ne 0 ] && exit
            echo ""

            same_http_code_lines=()
        fi

        same_http_code_lines+=("$line")
        prev_code=$code
    done

    # Print the last HTTP code group
    sorted=($(sort -nr <<<"${same_http_code_lines[*]}"))
    for sorted_line in "${sorted[@]}"; do
        check_limit
        [ $? -ne 0 ] && break

        echo -n $sorted_line | awk '{printf ("%s\t%s", $2, $3)}'
        check_if_blacklisted_ip "$sorted_line"
        [ $? -ne 0 ] && echo -ne "\t*Blacklisted!*"

        echo ""
        lines_count=$((lines_count+1))
    done
}

rank_ip_most_common_failing_http_codes() {
    out=($(cat $filename | cut -d' ' -f1,9 | grep -E " 4[0-9]{2}" | uniq -c | sort -nr | awk '{printf ("%s\t%s\n", $3, $2)}' | sort -nr | uniq -u))
    prev_code=$(echo "${out[0]}" | awk '{print $1}')
    same_http_code_lines=()
    results=()

    for line in "${out[@]}"; do
        code=$(echo $line | awk '{print $1}')

        if [ "$prev_code" != "$code" ]; then
            sorted=($(sort -nr <<<"${same_http_code_lines[*]}"))
            for sorted_line in "${sorted[@]}"; do
                check_limit
                [ $? -ne 0 ] && break

                echo -n $sorted_line
                check_if_blacklisted_ip "$sorted_line"
                [ $? -ne 0 ] && echo -ne "\t*Blacklisted!*"

                echo ""
                lines_count=$((lines_count+1))
            done

            check_limit
            [ $? -ne 0 ] && exit
            echo ""

            same_http_code_lines=()
        fi

        same_http_code_lines+=("$line")
        prev_code=$code
    done

    # Print the last HTTP code group
    sorted=($(sort -nr <<<"${same_http_code_lines[*]}"))
    for sorted_line in "${sorted[@]}"; do
        check_limit
        [ $? -ne 0 ] && break

        echo -n $sorted_line
        check_if_blacklisted_ip "$sorted_line"
        [ $? -ne 0 ] && echo -ne "\t*Blacklisted!*"

        echo ""
        lines_count=$((lines_count+1))
    done
}

rank_ip_most_sent_bytes() {
    out=($(cat $filename | cut -d' ' -f1,10 | grep -v "-" | sort -n | uniq -c))
    prev_ip="$(echo ${out[0]} | cut -d' ' -f1)"

    # Declare a dictionary (key: ip, value: byte count)
    declare -A byte_count_per_ip
    transfered_bytes=()

    for line in "${out[@]}"; do
        ip="$(echo $line | awk '{print $2}')"
        count=$(echo $line | awk '{print $3}')

        transfered_bytes+=($count)

        if [ "$prev_ip" != "$ip" ]; then
            total_bytes=0
            for nb_bytes in "${transfered_bytes[@]}"; do
                total_bytes=$((total_bytes + nb_bytes))
            done

            byte_count_per_ip[$ip]=$total_bytes
        fi

        prev_ip="$(echo $line | cut -d' ' -f1)"
    done

    # Sort the lines
    lines=()
    for ip in "${!byte_count_per_ip[@]}"; do
        lines+=("${byte_count_per_ip[$ip]} $ip")
    done

    sorted=($(sort -nr <<<"${lines[*]}"))

    # Print the result
    for line in "${sorted[@]}"; do
        check_limit
        [ $? -ne 0 ] && break

        echo -n $line | awk '{printf ("%s\t%s", $2, $1)}'
        check_if_blacklisted_ip "$sorted_line"
        [ $? -ne 0 ] && echo -ne "\t*Blacklisted!*"

        echo ""
        lines_count=$((lines_count+1))
    done
}

resolve_blacklisted_ips() {
    for line in $(cat dns.blacklist.txt); do
        # Loop on all results and filter out any CNAME record,
        # all domain names contains at least a letter
        for ip in $(dig +short A "$line" | grep -vE "[a-z]"); do
            blacklist+=("$ip")
        done
    done
}

# Check if the IP address in the string is in the blacklist
# Arguments: 1 -> string containing ip
# Returned status code: 0 -> not in blacklist, 1 -> in blacklist
check_if_blacklisted_ip() {
    # Return earlier if nothing is in the blacklist
    [ ${#blacklist[@]} -eq 0 ] && return 0

    ip_string=$1
    for ip in "${blacklist[@]}"
    do
        contained_ip=$(echo "$ip_string" | grep "$ip")
        if [ ! -z $contained_ip ]; then
            return 1
        fi
    done
    return 0
}

print_usage() {
    echo "usage ./log_sum.sh [-L N] (-c | -2 | -r | -F | -t) [-e] <filename>"
    exit -1
}

[ "$#" -lt 2 ] && print_usage

while getopts ":L:e:c:2:r:F:t:" o; do
    case "${o}" in
        L)
            limit=$OPTARG
            ;;
        e)
            filename="$OPTARG"
            resolve_blacklisted_ips
            ;;
        c)
            filename="$OPTARG"
            option=1
            ;;
        2)
            filename="$OPTARG"
            option=2
            ;;
        r)
            filename="$OPTARG"
            option=3
            ;;
        F)
            filename="$OPTARG"
            option=4
            ;;
        t)
            filename="$OPTARG"
            option=5
            ;;
        *)
            print_usage
            ;;
    esac
done
shift $((OPTIND-1))

remaining_args=("${@}")

# Probably the `-e` option was given
if [ ! -z $remaining_args ]; then
    filename=$remaining_args
    resolve_blacklisted_ips
fi

if [ -z $filename ]; then
    print_usage
fi

case "$option" in
        1)
            rank_ip_most_connection_attempts
            ;;
        2)
            rank_ip_most_successful_attempts
            ;;
        3)
            rank_ip_most_common_http_codes
            ;;
        4)
            rank_ip_most_common_failing_http_codes
            ;;
        5)
            rank_ip_most_sent_bytes
            ;;
        *)
            print_usage
            ;;
esac

IFS=$old
