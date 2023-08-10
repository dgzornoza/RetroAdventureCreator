SECTION code_user

PUBLIC _openScrChain  ; export C decl "extern void openScrChain(char chain) __z88dk_fastcall;"

;-------------------------------------------------------------
; openScrChain:
; Open screen chain  
;
; Input:
; -----------------------------------------------------
; Register L = chain to open (1 = command line screen, 2 = upper screen)
;-------------------------------------------------------------
_openScrChain:
    ld a, l
    call ROM_OPEN_CHAIN
    ret


; CONsTANTS --------------------------------------------------------------------------

; ----------------------------------------------------------------------------
; ROM Routine address for open chain screen
; Input:
;----------------------------------------------
; A -> chain: 1 = command line screen, 2 = upper screen
; ----------------------------------------------------------------------------
ROM_OPEN_CHAIN:		equ $1601