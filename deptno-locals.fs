\ if department numbers are valid, print them on a single line
: fire { fireno policeno sanitationno }
    fireno sanitationno = if exit then
    fireno policeno = if exit then
    policeno .  sanitationno .  fireno .  cr ;

\ tries to assign numbers with given policeno and sanitationno
\ and fire = 12 - policeno - sanitationno
: sanitation { policeno sanitationno }
    policeno sanitationno = if exit then \ no repeated numbers
    12 policeno - sanitationno -         \ calculate fireno
    dup 1 < if exit then                 \ cannot be less than 1
    dup 7 > if exit then                 \ cannot be more than 7
    policeno sanitationno fire ;

\ tries to assign numbers with given policeno
\ and sanitation = 1, 2, 3, ..., or 7
: police { policeno }
    8 1 do policeno i sanitation loop ;

\ tries to assign numbers with police = 2, 4, or 6
: departments cr                         \ leave input line
    8 2 do i police 2 +loop ;
