: compare-and-swap-with-next ( string i -- )
   2dup + dup >r c@ -rot 1 + + c@
   2dup > if
      r@ c! r> 1 + c! else
      rdrop 2drop then ;

: bubblesort ( string len -- string len )
   dup >r 1 - begin
       dup 0> while
       dup 0 do
          over i compare-and-swap-with-next
       loop 1 -
    repeat drop r> ;

\ s" abracadabra" bubblesort \ cr type
\ s" The quick brown fox" bubblesort \ cr type
\ s" a quick brown fox jumps over the lazy dog." bubblesort \ cr type

