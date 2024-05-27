SECTION code_user

EXTERN asm_print_char


;-------------------------------------------------------------------------------
;  Name:		internal asm_show_cursor
;  Description:	routine for show shell cursor
;  Input:		--
;  Output: 	    --
;-------------------------------------------------------------------------------
PUBLIC asm_show_cursor
asm_show_cursor:

   ld a, '_'               ; print cursor 
   call asm_print_char
     
   ret
   
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

