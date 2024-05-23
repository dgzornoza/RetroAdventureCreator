SECTION code_user

PUBLIC _print_buffer_keys         ; export C decl "extern void print_buffer_keys();"

EXTERN asm_pop_buffer_key

EXTERN asm_print_char
EXTERN asm_font_inc_x
EXTERN asm_font_dec_x

;-------------------------------------------------------------------------------
;  Name:		      public _print_buffer_keys
;  Description:	print string from buffer_keys.
;  Input:		   --
;  Output: 	      --
;  Clobbers: 	   --
;-------------------------------------------------------------------------------
_print_buffer_keys:
 
   push hl                       ; preserve stack

loop:
   call asm_pop_buffer_key
   ld a, l
   
   cp 0
   jr z, end                     ; is empty buffer?, end routine
 
   cp 12
   jr z, print_buffer_keys_delete      ; is delete?, delete char
 
   cp 32
   jr c, loop                    ; is char control (ascii < 32)?, repeat loop
 
   ;;; here is valid char (ascii >= 32)
   call asm_print_char
   call asm_font_inc_x
   jr loop                       ; repeat loop
 
;;; code to execute when enter key is pressed (end of routine)
.end:                     
   pop hl                        ; recovery stack
   ret
 
;;; Code to execute when delete key is pressed (delete char)
.print_buffer_keys_delete:                  
   call asm_font_dec_x
   ld a, ' '                     ; delete cursor and go to previous char
   call asm_print_char
   jr loop                       ; repeat loop

