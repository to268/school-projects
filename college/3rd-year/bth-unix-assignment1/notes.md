# Flags

`log_sum.sh [-L N] (-c | -2 | -r | -F | -t) <filename>`
`<filename>` is valid file or `-` to read from stdin

- `-L` -> Limit number of results
- `-c` -> Which IP makes the most connection attempts
- `-2` -> Which IP makes the most successful attempts (http code 2XX)
- `-r` -> What are the most common result codes and from which IP
- `-F` -> What are the most common failing result codes and from which IP (http code 4XX)
- `-t` -> Which IP do we send the most bytes to (field after http code)

# Plan

- [] Handle the arguments with `getopts` (see lecture 1)
    - [] Create a switch and check if args make sense
    - [] Check if file is valid or is stdin (see if an idiom exists about stdin)
- [] Execute arguments, 1 arg -> 1 function, (except the `-L`)
- [] Formating output
- [] DNS blackist `-e` flag
