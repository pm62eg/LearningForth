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
    2 drop false exit then
    rightsmaller? if
    false exit then true ;

: randletter ( -- c )
    here 12 + @                     \ get random value
    26 /mod                         \ mod and quotient
    here 12 + !                     \ save rest of random value
    [char] a + ;                    \ adjust return character

: getpos ( X -- X n )               \ print 'X' and ask for 0 to 9
   ." pos for '" dup emit ." '? "
   key [char] 0 - cr ;              \ TODO validation

: gamescore ( -- n )
   0 10 0 do
      i occupied? if 1 + then
   loop ;

: game10 ( -- )
   xoshiro256-timeseed
   xoshiro256ss here 12 + !         \ save 64 bits
   here 10 [char] - fill
   cr ." 0 1 2 3 4 5 6 7 8 9" cr
   begin
      printgame
      randletter getpos
      2dup validmove? dup if
          -rot here + c! else
          -rot 2drop then
      gamescore 10 < and while
   repeat
   ." You scored " gamescore . ." points." ;
