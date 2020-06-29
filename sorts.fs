: bubblesort ( string len -- string len )
    dup 1 - begin       ( s l l-1 )
        dup 0 > while   ( s l l-1 )
        1 begin         ( s l l-1 1 )
        2dup > while    ( s l l-1 1 )
            3 pick over + c@ dup     ( s l l-1 1 Cl Cl )
            5 pick 4 pick + c@ dup rot < if ( s l l-1 1 Cl Cl1 )
                 5 pick 3 pick + c! ( s l l-1 1 Cl )
                 4 pick 3 pick + c! ( s l l-1 1 )
            else drop drop then ( s l l-1 1 )
        1 + repeat drop
    1 - repeat drop ;

\ s" abracadabra" bubblesort \ type
\ s" The quick brown fox" bubblesort \ type
