SECTION code_user

PUBLIC _open_screen_chain  ; export C decl "extern void open_screen_chain(char chain) __z88dk_fastcall;"

EXTERN _ROM_OPEN_CHAIN

;-------------------------------------------------------------------------------
;  Name:		    public _open_screen_chain
;  Description:     Open screen chain 
;  Input:		    L = chain to open (1 = command line screen, 2 = upper screen)
;  Output: 	        --
;-------------------------------------------------------------------------------
_open_screen_chain:
    ld a, l
    call _ROM_OPEN_CHAIN
    ret




