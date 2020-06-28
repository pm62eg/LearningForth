: multiple? mod 0= ;
: fizzbuzz ( n -- )
   1 + 1 do
       i 5 multiple? i 3 multiple? 2dup and
       if ." fizzbuzz " 2drop else
           if ." fizz " drop else
               if ." buzz " else
                   i . then then then
    loop ;
