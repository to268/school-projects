.global print
.global fact
.global gcd
.global lntwo

.text
print:
    pushq           %rbp
    movq            %rsp, %rbp
    call            .print_no
    call            .print_lf
    leave
    ret

.print_convert_negative:
    # Save the number before .print_minus
    pushq           %rdi
    call            .print_minus
    # Restore the saved number
    popq            %rdi
    # Convert negative number to positive number with two's complement negation
    negq            %rdi
    jmp             .print_initialize_conversion

.print_no:
    pushq           %rbp
    movq            %rsp, %rbp
    xorq            %r8, %r8
    cmp             $0, %rdi
    jl              .print_convert_negative

.print_initialize_conversion:
    # Make sur that the number is in the rax register
    movq            %rdi, %rax

.print_convert_and_push_no:
    # Track digit count
    inc             %r8
    xorq            %rdx, %rdx
    movq            $0xa, %rcx
    divq            %rcx
    addq            $0x30, %rdx
    pushq           %rdx
    testq           %rax, %rax
    jne             .print_convert_and_push_no
    # Load the char buff
    leaq            print_char_buff(%rip), %rsi

# Print each digits on the stack (in reverse order)
.print_no_loop:
    dec             %r8
    popq            %rdx
    movq            %rdx, (%rsi)
    call            .print_char
    # Check if there are still a digit to print
    test            %r8, %r8
    jnz             .print_no_loop
    leave
    ret

.print_char:
    pushq           %rbp
    movq            %rsp, %rbp
    movq            $1, %rax
    movq            $1, %rdi
    leaq            print_char_buff(%rip), %rsi
    movq            $1, %rdx
    syscall
    leave
    ret

.print_minus:
    pushq           %rbp
    movq            %rsp, %rbp
    leaq            print_char_buff(%rip), %rsi
    movq            $0x2d, (%rsi)
    call            .print_char
    leave
    ret

.print_lf:
    pushq           %rbp
    movq            %rsp, %rbp
    leaq            print_char_buff(%rip), %rsi
    movq            $0xa, (%rsi)
    call            .print_char
    leave
    ret

fact:
    # Convert input double value to uint64_t
    pushq           %rbp
    movq            %rsp, %rbp
    movsd           sse_magic_values(%rip), %xmm1
    comisd          %xmm1, %xmm0
    jnb             .fact_convert_siged_double
    cvttsd2siq      %xmm0, %rcx
    jmp             .fact_init_loop

.fact_convert_siged_double:
    subsd           %xmm1, %xmm0
    cvttsd2siq      %xmm0, %rcx
    # Select all bits, except the sign bit
    btcq            $63, %rcx

.fact_init_loop:
    movl            $2, %edx
    movl            $1, %eax

.fact_loop:
    # Check if we need to exit
    cmpq            %rdx, %rcx
    jb              .fact_loop_end_fast_ret
    imulq           %rdx, %rax
    incq            %rdx
    jmp             .fact_loop

.fact_loop_end_fast_ret:
    testq           %rax, %rax
    js              .fact_end
    cvtsi2sdq       %rax, %xmm0
    leave
    ret

.fact_end:
    # Convert result to double and return
    movq            %rax, %rdx
    andl            $1, %eax
    shrq            %rdx
    orq             %rax, %rdx
    cvtsi2sdq       %rdx, %xmm0
    addsd           %xmm0, %xmm0
    leave
    ret

gcd:
	pushq           %rbp
	movq            %rsp, %rbp
    # Convert first double to uint64_t
    movsd           sse_magic_values(%rip), %xmm2
    comisd          %xmm2, %xmm0
    jnb             .gcd_shift_first_value_if_nedeed
    cvttsd2siq      %xmm0, %rdi

.gcd_check_if_zero:
    # Convert second double to uint64_t
    comisd          %xmm2, %xmm1
    jnb             .gcd_shift_second_value_if_nedeed
    cvttsd2siq      %xmm1, %rax
    # Test if first value is 0
    testq           %rdi, %rdi
    je              .gcd_first_number_zero

.gcd_get_shift_and_shift_first_number:
    # Test if second value is 0
    testq           %rax, %rax
    je              .gcd_second_number_zero
    # Get shift and apply shift count to first number (interleaved)
    movq            %rdi, %r8
    xorl            %ecx, %ecx
    orq             %rax, %r8
    rep bsfq        %rdi, %rcx
    rep bsfq        %r8, %r8
    shrq            %cl, %rdi

.gcd_do_while:
    # Apply shift count to second number
    xorl            %ecx, %ecx
    rep             bsfq    %rax, %rcx
    # Substract and shift first number, store to temp var (interleaved)
    shrq            %cl, %rax
    subq            %rdi, %rax
    movl            %eax, %edx
    movq            %rax, %rsi
    sarl            $31, %edx
    movslq          %edx, %rdx
    # And second number and temp val, add to first number
    andq            %rdx, %rax
    # Add second number and temp val, xor with temp val, store to first numebr
    addq            %rax, %rdi
    leaq            (%rsi,%rdx), %rax
    xorq            %rdx, %rax
    # Check if second number is zero
    testq           %rsi, %rsi
    jne             .gcd_do_while
    # Shift first number and reuse gcd_second_number_zero routine
    movl            %r8d, %ecx
    salq            %cl, %rdi

.gcd_second_number_zero:
    testq           %rdi, %rdi
    js              .gcd_handle_first_siged_number_and_ret
    pxor            %xmm0, %xmm0
    cvtsi2sdq       %rdi, %xmm0
    leave
    ret

.gcd_shift_second_value_if_nedeed:
    subsd           %xmm2, %xmm1
    cvttsd2siq      %xmm1, %rax
    # Select all bits, except the sign bit
    btcq            $63, %rax
    testq           %rdi, %rdi
    jne             .gcd_get_shift_and_shift_first_number

.gcd_first_number_zero:
    testq           %rax, %rax
    js              .gcd_handle_second_siged_number_and_ret
    pxor            %xmm0, %xmm0
    cvtsi2sdq       %rax, %xmm0
    leave
    ret

.gcd_shift_first_value_if_nedeed:
    subsd           %xmm2, %xmm0
    cvttsd2siq      %xmm0, %rdi
    # Select all bits, except the sign bit
    btcq            $63, %rdi
    jmp             .gcd_check_if_zero

.gcd_handle_first_siged_number_and_ret:
    movq            %rdi, %rax
    andl            $1, %edi
    pxor            %xmm0, %xmm0
    shrq            %rax
    orq             %rdi, %rax
    cvtsi2sdq       %rax, %xmm0
    addsd           %xmm0, %xmm0
    leave
    ret

.gcd_handle_second_siged_number_and_ret:
    movq            %rax, %rdx
    andl            $1, %eax
    pxor            %xmm0, %xmm0
    shrq            %rdx
    orq             %rax, %rdx
    cvtsi2sdq       %rdx, %xmm0
    addsd           %xmm0, %xmm0
    leave
    ret

lntwo:
	pushq           %rbp
	movq            %rsp, %rbp
    xorpd           %xmm1, %xmm1
    ucomisd         %xmm1, %xmm0
    jne             .lntwo_compute
    jnp             .lntwo_end

.lntwo_compute:
    # Fast log2 compute
    cvttsd2si       %xmm0, %rax
    movq            %rax, %rcx
    sarq            $63, %rcx
    subsd           lntwo_magic_value(%rip), %xmm0
    cvttsd2si       %xmm0, %rdx
    andq            %rcx, %rdx
    orq             %rax, %rdx
    movq            %rdx, %rax
    shrq            %rax
    orq             %rdx, %rax
    movq            %rax, %rcx
    shrq            $2, %rcx
    orq             %rax, %rcx
    movq            %rcx, %rax
    shrq            $4, %rax
    orq             %rcx, %rax
    movq            %rax, %rcx
    shrq            $8, %rcx
    orq             %rax, %rcx
    movq            %rcx, %rax
    shrq            $16, %rax
    orq             %rcx, %rax
    movq            %rax, %rcx
    shrq            $32, %rcx
    orq             %rax, %rcx
    movq            %rcx, %rax
    shrq            %rax
    subq            %rax, %rcx
    movabsq         $571347909858961602, %rax
    imulq           %rcx, %rax
    shrq            $56, %rax
    andl            $-4, %eax
    leaq            lntwo_tab64(%rip), %rcx
    xorps           %xmm1, %xmm1
    cvtsi2sdl       (%rax,%rcx), %xmm1

.lntwo_end:
    movapd  %xmm1, %xmm0
    leave
    ret

.data
print_char_buff:
    .space 1

sse_magic_values:
    .long   0
    .long   1138753536

lntwo_see_signed_magin_values:
    .quad   -9223372036854775808
    .quad   0

lntwo_magic_value:
    .quad   4890909195324358656

lntwo_tab64:
    .long   63
    .long   0
    .long   58
    .long   1
    .long   59
    .long   47
    .long   53
    .long   2
    .long   60
    .long   39
    .long   48
    .long   27
    .long   54
    .long   33
    .long   42
    .long   3
    .long   61
    .long   51
    .long   37
    .long   40
    .long   49
    .long   18
    .long   28
    .long   20
    .long   55
    .long   30
    .long   34
    .long   11
    .long   43
    .long   14
    .long   22
    .long   4
    .long   62
    .long   57
    .long   46
    .long   52
    .long   38
    .long   26
    .long   32
    .long   41
    .long   50
    .long   36
    .long   17
    .long   19
    .long   29
    .long   10
    .long   13
    .long   21
    .long   56
    .long   45
    .long   25
    .long   31
    .long   35
    .long   16
    .long   9
    .long   12
    .long   44
    .long   24
    .long   15
    .long   8
    .long   23
    .long   7
    .long   6
    .long   5
