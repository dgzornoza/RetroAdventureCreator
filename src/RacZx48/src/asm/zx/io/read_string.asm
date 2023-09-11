SECTION code_user

PUBLIC _read_string         ; export C decl "extern char* read_string(uint8_t *buffer, uint8_t length);"

EXTERN _print_char

EXTERN _pop_queue_key

EXTERN asm_font_inc_x
EXTERN asm_font_dec_x

EXTERN _ROM_LAST_KEY    
EXTERN _ROM_KEY_SCAN    

;-------------------------------------------------------------------------------
;  Name:		      public _read_string
;  Description:	get input string from keyboard
;  Input:		   HL = Pointer to string to get.
;                 A = max string length to get.
;  Output: 	      --
;-------------------------------------------------------------------------------
_read_string:
 
   push hl                       ; store string pointer
   push de
   push bc                       ; modify by ROM_KEY_SCAN
 
   ld (InputsCounter), a         ; char counter to use
   ld (InputsLimit), a           ; store max length

   ;ei                            ; enable interruptions

start:
   ld a, '_'                     ; print new cursor
   call _print_char
 
   ;xor a
   ;ld (_ROM_LAST_KEY), a         ; clean last key pressed
   
loop:
   ; push hl                       ; ROM_KEY_SCAN modify HL, (preserve)
   ; call _ROM_KEY_SCAN            ; call ROM routine for scan key
   ; pop hl   
   ; ld a, (_ROM_LAST_KEY)         ; get decoded value
   call _pop_queue_key
   ld a, l
   
   cp 13
   jr z, end                     ; is enter?, end routine
 
   cp 12
   jr z, read_string_delete      ; is delete?, delete char
 
   cp 32
   jr c, loop                    ; is char control (ascii < 32)?, repeat scan loop
 
   ;;; here is valid char (ascii >= 32)
   ex af, af'                    ; store value in A'
 
   ;;; Check max string length
   ld a, (InputsCounter)         ; A = available chars
   or a                          ; is zero?
   jr z, loop                    ; if Zero, not insert char
   dec a
   ld (InputsCounter), a         ; decrement available length
 
   ex af, af'                    ; recovery ascii from A'
   ld (hl), a                    ; store ascii char
   inc hl                        ; go to next char and print
   call _print_char
   call asm_font_inc_x
   jr start                      ; repeat to press enter key
 
;;; code to execute when enter key is pressed (end of routine)
end:                     
   ld a, ' '                     ; delete screen cursor
   call _print_char
   xor a
   ld (hl), a                    ; store end of string in HL

   ;di                           ; diable interruptions
   pop bc
   pop de                        ; recovery register values
   pop hl                        ; recovery start of input string
   ret
 
;;; Code to execute when delete key is pressed (delete char)
read_string_delete:                  
   ld a, (InputsLimit)      
   ld b, a
   ld a, (InputsCounter)
   cp b                          ; if availabe chars - limit == 0 ...
   jr z, loop                    ; ... can not delete (start of string), otherwise, delete char
 
   inc a                        
   ld (InputsCounter), a         ; increment available space
 
   dec hl                        ; decrement string pointer
   ld a, ' '                     ; delete cursor and previous char
   call _print_char
   call asm_font_dec_x
   jr start                      ; go main loop



   
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

.InputsCounter    db  0
.InputsLimit      db  0
