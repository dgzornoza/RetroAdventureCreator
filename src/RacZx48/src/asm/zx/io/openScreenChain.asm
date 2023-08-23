SECTION code_user

PUBLIC _zx_openScreenChain  ; export C decl "extern void zx_openScreenChain(char chain) __z88dk_fastcall;"
EXTERN _ROM_OPEN_CHAIN

;-------------------------------------------------------------------------------
;  Name:		    public _zx_openScreenChain
;  Description:     Open screen chain 
;  Input:		    L = chain to open (1 = command line screen, 2 = upper screen)
;  Output: 	        --
;-------------------------------------------------------------------------------
_zx_openScreenChain:
    ld a, l
    call _ROM_OPEN_CHAIN
    ret




