\ Funky game 'game10'.
\ The game board consists of 10 empty cells.
\ For each move the computer chooses a random character,
\ possibly a repeat from earlier moves,
\ and the player puts it in one empty cell, keeping
\ the already filled-in cells in alphabetical order.
\ When there is no possible move, the game is over.
\ The score is the number of filled-in cells.

variable gameboard 9 allot             \ space for 10 cells
variable PRNGstate

time&date + * + * + PRNGstate !

: PRNGnext ( -- u )                    \ adequate quality generator
    PRNGstate @ 1103515245 * 12345 + dup PRNGstate !
    16 rshift 32767 and ;

: randletter ( -- c )
    PRNGnext 26 mod                    \ yeah, yeah: modulo bias
    [char] a + ;                       \ adjust return character

: printcell ( n -- )
    gameboard + c@ emit 32 emit ;

: printboard ( -- )
    10 0 do i printcell loop ;

: occupied? ( n -- f )
    gameboard + c@ [char] - <> ;

: leftgreater? ( c n -- f )            \ TODO invert condition, simplify
    dup 0 = if
       2drop false exit then
    dup 1 - occupied? if
       1 - gameboard + c@ < else
       1 - recurse then ;

: rightsmaller? ( c n -- f )           \ TODO invert condition, simplify
    dup 9 = if
       2drop false exit then
    dup 1 + occupied? if
       1 + gameboard + c@ > else
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
    gameboard 10 cells [char] - fill ;

\ Seed PRNG, print game intro
: gameinit ( -- )
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
           -rot gameboard + c! else
           -rot 2drop then             \ TODO 'gamescore' just once
    invert gamescore 10 = or until
    gamescore dup 10 = if ." CONGRATULATIONS!! " then
    ." You scored " . ." points." cr ;

: game10 ( -- )
    gameinit
    begin
       playgame
       cr ." Another (Y/n)? "
    key dup emit cr [char] n = until ;
