: printcell ( n -- )
   here + c@ emit 32 emit ;

: printgame ( -- )
    10 0 do i printcell loop ;

: randletter ( -- c )
    [char] x ;

: getpos ( c -- c n ) \ 0 to 9
   ." pos for '" dup emit ." '? "
   key [char] 0 - cr ;

: game10 ( -- )
   here 10 [char] - fill
   cr ." 0 1 2 3 4 5 6 7 8 9" cr
   begin
      printgame
      randletter getpos
      dup here + c@ [char] - = while
      here + c!
   repeat 2drop ;
