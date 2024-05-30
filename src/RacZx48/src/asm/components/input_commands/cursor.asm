SECTION code_user

EXTERN asm_print_char

EXTERN _GLOBAL_FONT_ATTRIBUTES

;-------------------------------------------------------------------------------
;  Name:		internal asm_show_cursor
;  Description:	routine for show shell cursor
;  Input:		   --
;  Output: 	      --
;	Clobbers: 	   DE, BC
;-------------------------------------------------------------------------------
PUBLIC asm_show_cursor
asm_show_cursor:

   ; ld a, (_GLOBAL_FONT_ATTRIBUTES) 
   ; or a, 0xFF                       
   ; ld (_GLOBAL_FONT_ATTRIBUTES), a  ; set cursor flash attribute  

   ld a, '_'               ; print cursor 
   call asm_print_char

   ret
   
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

