SECTION code_user

PUBLIC _openScrChannel  ; export C decl "extern void openScrChannel(char chain);"
PUBLIC _print           ; export C decl "extern void print(uint8_t *chain);"


_openScrChannel:
    ld a, l
    call	OPENCHAN
    ret
    
_print: 
    ld a, (hl)
    rst	$10
    ret


; ----------------------------------------------------------------------------
; ROM Routine for open chain screen
;
; Input: A -> Chain	1 = command line
; 				    2 = upper screen
; ----------------------------------------------------------------------------
OPENCHAN:		equ $1601