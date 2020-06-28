: rc4-swap ( base i j -- )
    rot tuck + rot rot + dup c@
    rot dup c@
    rot rot c! swap c! ;

create rc4-S 256 allot
create rc4-i
create rc4-j

: rc4-peg 256 mod ; \ 255 and \ optimizing compiler?
: rc4-i>  rc4-i c@ ;
: >rc4-i  rc4-i c! ;
: rc4-j>  rc4-j c@ ;
: >rc4-j  rc4-j c! ;
: rc4-Si> rc4-S rc4-i> + c@ ;
: rc4-Sj> rc4-S rc4-j> + c@ ;

: rc4-ksa ( keystr keylen -- )
    256 0 do i rc4-S i + c! loop
    0 >rc4-j
    256 0 do
        rc4-j> rc4-S i + c@ +
            2 pick i 3 pick mod + c@ +
            rc4-peg >rc4-j
        rc4-S i rc4-j> rc4-swap
    loop 2drop 0 >rc4-i 0 >rc4-j ;

: rc4-prga ( -- k )
    rc4-i> 1 + rc4-peg >rc4-i
    rc4-j> rc4-Si> + rc4-peg >rc4-j
    rc4-S rc4-i> rc4-j> rc4-swap
    rc4-S rc4-Si> rc4-Sj> + rc4-peg + c@ ;

: rc4-go ( txt txtlen key keylen )
    rc4-ksa
    0 do dup i + c@ rc4-prga xor hex . decimal loop drop ;

\ run examples                         \ expected output
\ s" Plaintext" s" Key" rc4-go         \ BB F3 16 E8 D9 40 AF 0A D3
\ s" pedia" s" Wiki" rc4-go            \ 10 21 BF 04 20
\ s" Attack at dawn" s" Secret" rc4-go \ 45 A0 1F 64 5F C3 5B 38 35 52 54 4B 9B F5
