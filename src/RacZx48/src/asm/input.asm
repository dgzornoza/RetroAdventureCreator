SECTION code_user

PUBLIC _zxInputstring    ; export C decl "extern char* zxInputstring(uint8_t *buffer, uint8_t length) __z88dk_fastcall;"
PUBLIC _zxTest

_zxTest:
   ld e, l     ; parametro 

   ld a, (bufferIndex)
   cp bufferLength      ; es menor que el tamaño del buffer?
   jp nc, zxTest_exit   ; si es mayor sale de la funcion
   
   ld hl, buffer      ; HL = puntero al offset en el buffer
   ld c, a
   ld b, 0                         
   add hl, bc  
   
   inc a                ; incrementar buffer index
   ld (bufferIndex), a

   ld (hl), e           ; añadir el parametro al buffer
   
zxTest_exit:     
   ret

;-------------------------------------------------------------------------------
;  Name:		      public _zxInputstring
;  Description:	get input string from keyboard
;  Input:		   HL = Pointer to string to get.
;                 A = max string length to get.
;  Output: 	      --
;-------------------------------------------------------------------------------
_zxInputstring:
 
   push hl                      ; store string pointer
   push de
   push bc                      ; modify by ROM_KEY_SCAN
 
   ld (InputsCounter), a        ; char counter to use
   ld (InputsLimit), a          ; store max length

   ei                           ; enable interruptions

inputs_start:
   ld a, '_'                    ; print new cursor
   call font_safe_print_char
 
   xor a
   ld (ROM_LAST_KEY), a         ; clean last key pressed
   ;rst $38
inputs_loop:
   push hl                      ; ROM_KEY_SCAN modify HL, (preserve)
   call ROM_KEY_SCAN            ; call ROM routine for scan key
   pop hl   
   ld a, (ROM_LAST_KEY)         ; get decoded value
   
   cp 13
   jr z, inputs_end             ; is enter?, end routine
 
   cp 12
   jr z, inputs_delete          ; is delete?, delete char
 
   cp 32
   jr c, inputs_loop            ; is char control (ascii < 32)?, repeat scan loop
 
   ;;; here is valid char (ascii >= 32)
   ex af, af'                   ; store value in A'
 
   ;;; Check max string length
   ld a, (InputsCounter)       ; A = available chars
   or a                         ; is zero?
   jr z, inputs_loop            ; if Zero, not insert char
   dec a
   ld (InputsCounter), a        ; decrement available length
 
   ex af, af'                   ; recovery ascii from A'
   ld (hl), a                   ; store ascii char
   inc hl                       ; go to next char and print
   call font_safe_print_char
   call font_inc_x
   jr inputs_start              ; repeat to press enter key
 
;;; code to execute when enter key is pressed (end of routine)
inputs_end:                     
   ld a, ' '                    ; delete screen cursor
   call font_safe_print_char
   xor a
   ld (hl), a                   ; store end of string in HL

   di                           ; diable interruptions
   pop bc
   pop de                       ; recovery register values
   pop hl                       ; recovery start of input string
   ret
 
;;; Code to execute when delete key is pressed (delete char)
inputs_delete:                  
   ld a, (InputsLimit)      
   ld b, a
   ld a, (InputsCounter)
   cp b                         ; if availabe chars - limit == 0 ...
   jr z, inputs_loop            ; ... can not delete (start of string), otherwise, delete char
 
   inc a                        
   ld (InputsCounter), a        ; increment available space
 
   dec hl                       ; decrement string pointer
   ld a, ' '                    ; delete cursor and previous char
   call font_safe_print_char
   call font_dec_x
   jr inputs_start              ; go main loop
 

;-------------------------------------------------------------
; Ejecuta PrintChar_8x8 preservando registros
;-------------------------------------------------------------
;-------------------------------------------------------------------------------
;  Name:		      private font_safe_print_char
;  Description:	execute printchar function preserving registers
;-------------------------------------------------------------------------------
.font_safe_print_char
   push bc
   push de
   push hl                   ; preserve registers
   call print_char_8x8       ; print char
   pop hl                    ; recovery registers
   pop de
   pop bc
   ret



;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

.InputsCounter      db  0
.InputsLimit        db  0

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; CONsTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

ROM_LAST_KEY      equ    $5C08
ROM_KEY_SCAN      equ    $028E
ROM_FLAGS         equ 23611

.bufferLength  equ 10
.bufferIndex   db 0
.buffer        ds bufferLength - 1
               db 0
