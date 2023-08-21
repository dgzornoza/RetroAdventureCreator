SECTION code_user

PUBLIC _openScreenChain  ; export C decl "extern void _openScreenChain(char chain) __z88dk_fastcall;"

;-------------------------------------------------------------
; openScrChain:
; Open screen chain  
;
; Input:
; -----------------------------------------------------
; Register L = chain to open (1 = command line screen, 2 = upper screen)
;-------------------------------------------------------------
_openScreenChain:
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


