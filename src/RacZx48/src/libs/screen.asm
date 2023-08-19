SECTION code_user

PUBLIC _zx_open_screen_chain  ; export C decl "extern void zx_open_screen_chain(char chain) __z88dk_fastcall;"
PUBLIC _test  ; export C decl "extern void test() __z88dk_fastcall;"

;-------------------------------------------------------------------------------
;	Name:		    public zx_open_screen_chain
;	Description:	Open screen chain 
;	Input:		    L = chain to open (1 = command line screen, 2 = upper screen)
;	Output: 	    --
;-------------------------------------------------------------------------------
_zx_open_screen_chain:
    ld a, l
    call ROM_OPEN_CHAIN
    ret

_test:    
    ret

; CONsTANTS --------------------------------------------------------------------------

; ----------------------------------------------------------------------------
; ROM Routine address for open chain screen
; Input:
;----------------------------------------------
; A -> chain: 1 = command line screen, 2 = upper screen
; ----------------------------------------------------------------------------
ROM_OPEN_CHAIN:		equ $1601


