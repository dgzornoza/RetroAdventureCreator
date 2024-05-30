SECTION code_user

EXTERN asm_print_char
EXTERN asm_font_inc_x
EXTERN asm_font_dec_x

;-------------------------------------------------------------------------------
;  Name:		      internal _push_buffer_key
;  Description:	insert key into input buffer.
;                 the buffer for keys is BUFFER_LENGTH + 1 for posible control char.
;                 if buffer is full, the key is not inserted, only can insert a control char for delete, etc.
;
;  Input:		   a = key to push into input buffer
;  Output: 	      --
;  Clobbers: 	   AF, BC, DE, HL
;-------------------------------------------------------------------------------
PUBLIC asm_push_buffer_key
asm_push_buffer_key:
   
   ;exx                     ; store BC, DE, HL registers
   ld b, a                 ; move input key to b

   cp 32                   ; is char control (ascii < 32)?, C Flag = 1   
   ld a, 0                 ; NOTE: can not use xor because can change carry flag for next instruction
   adc a, BUFFER_LENGTH    ; A = 0 + BUFFER_LENGTH + Carry flag, if control char, Carry flag = 1 (BUFFER_LENGTH + 1)
   ld c, a

   ld a, (BufferIndex)
   cp c                    ; compare with buffer size
   jr nc, push_exit        ; if index > buffer, exit routine
   
   ld hl, Buffer           ; HL = pointer to buffer
   ld d, 00h 
   ld e, a
   add hl, de              ; add index offset to HL
   
   inc a                   ; increment buffer index:
   ld (BufferIndex), a

   ld (hl), b              ; add key ascii to buffer

.push_exit:
   
   ;exx         ; restore BC, DE, HL registers
   ret

;-------------------------------------------------------------------------------
;  Name:		      internal _print_buffer_keys
;  Description:	print string from input_buffer.
;                 The input buffer has 2 pointers: 
;                 cursor pointer: pointing to the last printed character.
;                 buffer pointer: pointing to the last key pressed
;                 this buffer can be used for keystrokes and screen printing
;
;  Remarks: diagram is in file 'print_buffer.drawio.svg'
;  Input:		   --
;  Output: 	      --
;	Clobbers: 	   AF, BC', DE', HL'
;-------------------------------------------------------------------------------
PUBLIC asm_print_buffer_keys 
asm_print_buffer_keys:
 
   push hl                       ; preserve stack
   push de   
   push bc                    

loop:
   ld bc, (CursorIndex)          ; get buffer indexes (B = buffer index, C = cursor index)

   ld a, c                       
   cp b                          ; compare buffer index with cursor index
   jr z, end                     ; if equal, exit routine

   ld hl, Buffer                 ; HL = pointer to buffer
   ld d, 00h
   ld e, c
   add hl, de                    ; add cursor index offset to HL

   ld a, (hl)                    ; get char from printer index
   cp 12
   jr z, print_buffer_keys_delete         ; is delete?, delete char
 
   cp 32
   jr c, print_buffer_keys_control_code   ; is char control (ascii < 32)?, process control code

   ;;; here is valid char (ascii >= 32)
   exx
   call asm_print_char        ; print char
   call asm_font_inc_x
   exx

   inc c
   ld a, c                
   ld (CursorIndex), a       ; increment cursor index
   
   jr loop                   ; repeat loop
 
;;; end of routine
.end:     
   pop bc
   pop de                
   pop hl                        ; recovery stack
   ret
 
;;; Code to execute when delete key is pressed (delete char)
.print_buffer_keys_delete:        

   ld a, b                       
   cp 1                                  ; compare buffer index with 1
   jr z, print_buffer_keys_control_code  ; if equal 1, is first char, process control code and not delete char

   dec c
   ld a, c                
   ld (CursorIndex), a           ; decrement cursor index
   dec b
   ld a, b                
   ld (BufferIndex), a           ; decrement buffer index

   exx
   call asm_font_dec_x
   ld a, ' '                     
   call asm_print_char           ; delete last printed char
   exx
   
;;; Displace keys to the left for remove current char
.print_buffer_keys_control_code:

   ld a, b
   sub c                      ; calculate difference for shift

   ld c, a                    ; Set the number of bytes to shift
   ld a, b                    ; store Buffer Index in A for decrement last
   ld b, 00h                  ; pair BC with number of bytes to shift for LDIR instruction
   ld de, hl                  ; Set the start and next address for shift left
   inc hl                     
   ldir                       ; Repeat for the rest of the bytes
   
   dec a
   ld (BufferIndex), a        ; decrement buffer index

   jr loop                    ; go loop

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; CONSTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;  
BUFFER_LENGTH equ 30       ; buffer length = screen width - 1 prompt - 1 cursor

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

.CursorIndex         db 0     ; NOTE: CursorIndex and BufferIndex  should be togheter on this order for use in pair registers, don't change
.BufferIndex         db 0

.Buffer              ds 31    ; buffer for input keys is BUFFER_LENGTH + 1 for posible control char
                     db 0
