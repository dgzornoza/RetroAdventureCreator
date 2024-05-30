SECTION code_user

EXTERN asm_print_char

;-------------------------------------------------------------------------------
;  Name:		internal asm_show_prompt
;  Description:	routine for show input command prompt
;  Input:		--
;  Output: 	    --
;	Clobbers: 	   DE, BC
;-------------------------------------------------------------------------------
PUBLIC asm_show_prompt
asm_show_prompt:

   ld a, '>'               ; print shell symbol
   call asm_print_char
     
   ret
   
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

