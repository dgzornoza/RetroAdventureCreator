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
; TODO: dgzornoza, queda implementar correctamente, posiblemente se tenga que usar en input_buffer
; ya que al borrar debe eliminarse el ultimo cursor puesto.
; PUBLIC asm_show_cursor
; asm_show_cursor:

;    ld a, '_'               ; print cursor 
;    call asm_print_char

;    ret
   
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

