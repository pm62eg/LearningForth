
: rol64 ( x k -- y )
    2dup lshift rot rot 64 swap - rshift or ;

create xstate 4 cells allot
2256914 17 lshift xstate 0 cells + !
2256914 19 lshift xstate 1 cells + !
2256914 23 lshift xstate 2 cells + !
2256914 29 lshift xstate 3 cells + !

: xoshiro256ss ( -- u )
    xstate 1 cells + @ dup 5 * 7 rol64 9 *
    swap 17 lshift
    xstate 2 cells + @ xstate 0 cells + @ xor xstate 2 cells + !
    xstate 3 cells + @ xstate 1 cells + @ xor xstate 3 cells + !
    xstate 1 cells + @ xstate 2 cells + @ xor xstate 1 cells + !
    xstate 0 cells + @ xstate 3 cells + @ xor xstate 0 cells + !
    xstate 2 cells + @                    xor xstate 2 cells + !
    xstate 3 cells + dup @ 45 rol64 swap ! ;
