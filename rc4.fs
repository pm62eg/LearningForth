: rc4-swap ( base a b -- )
    rot tuck + rot rot + dup c@
    rot dup c@
    rot rot c! swap c! ;

create rc4-S 256 allot
create rc4-i
create rc4-j

: rc4-ksa { keystr keylen -- }
    256 0 do i rc4-S i + c! loop
    0 rc4-j c!
    256 0 do
        rc4-j c@
        rc4-S i + c@
        +
        keystr i keylen mod + c@
        +
        256 mod rc4-j c!
        rc4-S i rc4-j c@ rc4-swap
    loop
    0 rc4-i c!
    0 rc4-j c!
    ;

: rc4-prga ( -- k )
    rc4-i c@ 1 + 256 mod rc4-i c!
    rc4-j c@ rc4-S rc4-i c@ + c@ + 256 mod rc4-j c!
    rc4-S rc4-i c@ rc4-j c@ rc4-swap
    rc4-S rc4-S rc4-i c@ + c@ rc4-S rc4-j c@ + c@ + 256 mod + c@ ;

: rc4-go ( txt txtlen key keylen )
    rc4-ksa
    0 do dup i + c@ rc4-prga xor hex . decimal loop drop ;

\ run examples                         \ expected output
\ s" Plaintext" s" Key" rc4-go         \ BB F3 16 E8 D9 40 AF 0A D3
\ s" pedia" s" Wiki" rc4-go            \ 10 21 BF 04 20
\ s" Attack at dawn" s" Secret" rc4-go \ 45 A0 1F 64 5F C3 5B 38 35 52 54 4B 9B F5
