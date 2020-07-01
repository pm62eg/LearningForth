s" xoshiro256ss.fs" required

: printcell ( n -- )
    here + c@ emit 32 emit ;

: printgame ( -- )
    10 0 do i printcell loop ;

: occupied? ( n -- f )
    here + c@ [char] - <> ;

: leftgreater? ( c n -- f )
    dup 0 = if
       2drop false exit then
    dup 1 - occupied? if
       1 - here + c@ < else
       1 - recurse then ;

: rightsmaller? ( c n -- f )
    dup 9 = if
       2drop false exit then
    dup 1 + occupied? if
       1 + here + c@ > else
       1 + recurse then ;

: validmove? ( c n -- f )
    dup occupied? if
       2drop false exit then
    2dup leftgreater? if
       2drop false exit then
    2dup rightsmaller? if
       2drop false exit then
    2drop true ;

: randletter ( -- c )
    here 10 + @                     \ get random value
    26 /mod                         \ mod and quotient
    here 10 + !                     \ save rest of random value
    [char] a + ;                    \ adjust return character

: prompt ( c -- )                   \ prompt for c
    ." pos for '" emit ." '? " ;

: getmove ( -- n )                  \ get player's move
    begin
       key dup
       [char] 0 < over [char] 9 > or while
       drop
    repeat dup emit cr [char] 0 - ;

: gamescore ( -- n )
    0 10 0 do
       i occupied? if 1 + then
    loop ;

: game10 ( -- )
    xoshiro256-timeseed
    xoshiro256ss here 10 + !         \ save 64 bits
    here 10 [char] - fill
    cr ." Can you score 10 points?" cr ." 0 1 2 3 4 5 6 7 8 9" cr
    begin
       printgame
       randletter dup prompt getmove
       2dup validmove? dup if
           -rot here + c! else
           -rot 2drop then
    invert gamescore 10 = or until
    ." You scored " gamescore . ." points." ;
