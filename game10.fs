\ Funky game 'game10'.
\ The game board consists of 10 empty cells.
\ For each move the computer chooses a random character,
\ possibly a repeat from earlier moves,
\ and the player puts it in one empty cell, keeping
\ the already filled-in cells in alphabetical order.
\ When there is no possible move, the game is over.
\ The score is the number of filled-in cells.

\ My (possibly faulty) implementation of xoshiro256ss
s" xoshiro256ss.fs" required

\ xoshiro256ss returns a 64-bit pseudo random number
: saverandom64 ( -- )
    xoshiro256ss here 10 + ! ;

\ Assume here+10 starts off with a large enough number < 26^10
\ otherwise the last letters will all be 'a'
: randletter ( -- c )
    here 10 + @                        \ get random value
    26 /mod                            \ mod and quotient
    here 10 + !                        \ save rest of random value
    [char] a + ;                       \ adjust return character

: printcell ( n -- )
    here + c@ emit 32 emit ;

: printboard ( -- )
    10 0 do i printcell loop ;

: occupied? ( n -- f )
    here + c@ [char] - <> ;

: leftgreater? ( c n -- f )            \ TODO invert condition, simplify
    dup 0 = if
       2drop false exit then
    dup 1 - occupied? if
       1 - here + c@ < else
       1 - recurse then ;

: rightsmaller? ( c n -- f )           \ TODO invert condition, simplify
    dup 9 = if
       2drop false exit then
    dup 1 + occupied? if
       1 + here + c@ > else
       1 + recurse then ;

: validmove? ( c n -- f )              \ TODO simplify, refactor
    dup occupied? if
       2drop false exit then
    2dup leftgreater? if
       2drop false exit then
    2dup rightsmaller? if
       2drop false exit then
    2drop true ;

\ Prompt for next move with c character
: prompt ( c -- )
    ." pos for '" emit ." '? " ;

\ Get player's move converted to integer 0 to 9
: getmove ( -- n )
    begin
       key dup
       [char] 0 < over [char] 9 > or while
       drop
    repeat dup emit cr [char] 0 - ;    \ echo user choice

\ Calculate game score for the current board
: gamescore ( -- n )
    0 10 0 do
       i occupied? if 1 + then
    loop ;

\ Print header, get random number, initialize playing board with 10 '-'
: boardinit ( -- )
    ." 0 1 2 3 4 5 6 7 8 9" cr
    saverandom64
    here 10 [char] - fill ;

\ Seed PRNG, print game intro
: gameinit ( -- )
    xoshiro256-timeseed                \ seed random number generator
    cr
    ." Place random letters in free cells," cr
    ." keeping them in alphabetical order." cr
    ." Can you score 10 points?" cr
    cr ;

: playgame ( -- )
    boardinit
    begin
       printboard
       randletter dup prompt getmove
       2dup validmove? dup if
           -rot here + c! else
           -rot 2drop then
    invert gamescore 10 = or until
    ." You scored " gamescore . ." points." cr ;

: game10 ( -- )
    gameinit
    begin
       playgame
       cr ." Another (Y/n)? "
    key dup emit cr [char] n = until ;
