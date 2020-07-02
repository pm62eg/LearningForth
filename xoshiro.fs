: rol32 ( x k -- y )
    2dup lshift -rot 32 swap - rshift or ;

: rol64 ( x k -- y )
    2dup lshift -rot 64 swap - rshift or ;

create xstate 4 cells allot

: xoshiro-timeseed ( -- )
    time&date + * + * +
    dup  7 lshift over xor xstate 0 cells + !
    dup 11 lshift over xor xstate 1 cells + !
    dup 13 lshift over xor xstate 2 cells + !
    dup 17 lshift      xor xstate 3 cells + ! ;

: xoshiro128ss ( -- u )
    xstate 1 cells + @ dup 5 * 7 rol32 9 *
    swap 9 lshift
    xstate 2 cells + @ xstate 0 cells + @ xor xstate 2 cells + !
    xstate 3 cells + @ xstate 1 cells + @ xor xstate 3 cells + !
    xstate 1 cells + @ xstate 2 cells + @ xor xstate 1 cells + !
    xstate 0 cells + @ xstate 3 cells + @ xor xstate 0 cells + !
    xstate 2 cells + @                    xor xstate 2 cells + !
    xstate 3 cells + dup @ 11 rol32 swap ! ;

: xoshiro256ss ( -- u )
    xstate 1 cells + @ dup 5 * 7 rol64 9 *
    swap 17 lshift
    xstate 2 cells + @ xstate 0 cells + @ xor xstate 2 cells + !
    xstate 3 cells + @ xstate 1 cells + @ xor xstate 3 cells + !
    xstate 1 cells + @ xstate 2 cells + @ xor xstate 1 cells + !
    xstate 0 cells + @ xstate 3 cells + @ xor xstate 0 cells + !
    xstate 2 cells + @                    xor xstate 2 cells + !
    xstate 3 cells + dup @ 45 rol64 swap ! ;
