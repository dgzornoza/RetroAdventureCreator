SECTION code_user

EXTERN asm_print_char

EXTERN  _SYS_LOWRES_SCR_HEIGHT;
EXTERN _ROM_CHARSET
EXTERN _DEFAULT_FONT_ATTRIBUTES
EXTERN _SYS_LOWRES_SCR_HEIGHT

EXTERN _GLOBAL_FONT_CHARSET
EXTERN _GLOBAL_FONT_X         
EXTERN _GLOBAL_FONT_ATTRIBUTES
EXTERN _GLOBAL_FONT_STYLE    


;-------------------------------------------------------------------------------
;  Name:		internal asm_show_shell
;  Description:	routine for initialize shell cursor
;  Input:		--
;  Output: 	    --
;-------------------------------------------------------------------------------
PUBLIC asm_show_shell
asm_show_shell:

   ;;; Set character properties for shell
   ld hl, _ROM_CHARSET                
   ld (_GLOBAL_FONT_CHARSET), hl

   ld a, _DEFAULT_FONT_ATTRIBUTES                    
   ld (_GLOBAL_FONT_ATTRIBUTES), a

   ld a, 0
   ld (_GLOBAL_FONT_STYLE), a

   ld c, 0
   ld b, _SYS_LOWRES_SCR_HEIGHT - 1
   ld (_GLOBAL_FONT_X), bc

   ;;; print shell
   ld a, '>'               ; print shell symbol
   call asm_print_char
   ld a, '_'               ; print cursor 
   call asm_print_char
     
   ret

;-------------------------------------------------------------------------------
;  Name:		internal asm_hide_shell
;  Description:	routine for initialize shell cursor
;  Input:		--
;  Output: 	    --
;-------------------------------------------------------------------------------
PUBLIC asm_hide_shell
asm_hide_shell:
   ;;; print shell
   ld a, '>'               ; print shell symbol
   call asm_print_char
   ld a, '_'               ; print cursor 
   call asm_print_char
   
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
