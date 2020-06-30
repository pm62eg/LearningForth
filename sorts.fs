: compare-and-swap-next ( string i -- )
   2dup + dup >r c@ rot rot 1 + + c@ 2dup >
   if     r@ c! r> 1 + c!
   else   r> drop 2drop
   then ;

: bubblesort ( string len -- string len )
   dup 1 -
   begin  dup 0>
   while  dup 0
          do   2 pick i compare-and-swap-next
          loop
          1 -
   repeat
   drop ;

\ s" abracadabra" bubblesort \ cr type
\ s" The quick brown fox" bubblesort \ cr type
\ s" a quick brown fox jumps over the lazy dog." bubblesort \ cr type

