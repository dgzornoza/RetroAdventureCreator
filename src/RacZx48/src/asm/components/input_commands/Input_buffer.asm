SECTION code_user

EXTERN asm_print_char
EXTERN asm_font_inc_x
EXTERN asm_font_dec_x

;-------------------------------------------------------------------------------
;  Name:		      internal _push_buffer_key
;  Description:	insert key into input buffer
;  Input:		   a = key to push into input buffer
;  Output: 	      --
;-------------------------------------------------------------------------------
PUBLIC asm_push_buffer_key
asm_push_buffer_key:
   
   push de                 ; store stack registers

   ex af, af'              ; A' = input key

   ld a, (BufferIndex)
   cp BUFFER_LENGTH        ; compare with buffer size
   jr nc, push_exit        ; if index > buffer, exit routine
   
   ld hl, Buffer           ; HL = pointer to buffer
   ld d, 00h 
   ld e, a
   add hl, de              ; add index offset to HL
   
   inc a                   ; increment buffer index
   ld (BufferIndex), a

   ex af, af'
   ld (hl), a              ; add key ascii to buffer

.push_exit:
   
   pop de
   ret

;-------------------------------------------------------------------------------
;  Name:		      internal _print_buffer_keys
;  Description:	print string from buffer_keys.
;  Input:		   --
;  Output: 	      --
;  Clobbers: 	   --
;-------------------------------------------------------------------------------
PUBLIC asm_print_buffer_keys 
asm_print_buffer_keys:
 
   push hl                       ; preserve stack
   push de   
   push bc                    

loop:
   ld de, (PrintedIndex)         ; get buffer indexes (D = buffer index, E = printed index)

   ld a, e                       ; increment printed index
   inc a
   ld (PrintedIndex), a

   ld a, e                       
   cp d                          ; compare buffer index with printed index
   jr z, end                     ; if equal, exit routine

   ld hl, Buffer                 ; HL = pointer to buffer
   ld d, 00h
   add hl, de                    ; add printed index offset to HL

   ld a, (hl)                    ; get char from printer index
   cp 12
   jr z, print_buffer_keys_delete         ; is delete?, delete char
 
   cp 32
   jr c, print_buffer_keys_remove_char    ; is char control (ascii < 32)?, delete char from buffer

   ;;; here is valid char (ascii >= 32)
   call asm_print_char
   call asm_font_inc_x

   jr loop                    ; repeat loop
 
;;; end of routine
.end:     
   pop bc
   pop de                
   pop hl                        ; recovery stack
   ret
 
;;; Code to execute when delete key is pressed (delete char)
.print_buffer_keys_delete:                  
   call asm_font_dec_x
   ld a, ' '                     ; delete previous char
   call asm_print_char
   
;;; Displace keys to the left for remove current char
.print_buffer_keys_remove_char:
   ld de, (PrintedIndex)      ; get buffer indexes (D = buffer index, E = printed index)

   ld a, d                    ; decrement buffer index
   dec a
   ld (BufferIndex), a
   ld a, e                    ; decrement printer index
   dec a
   ld (PrintedIndex), a

   ld a, d
   sub e                      ; calculate difference for displace

   ld de, hl                  ; Set the start address
   inc hl                     ; Set the next address      
   ld b, 00h
   ld c, a                    ; Set the number of bytes to displace
   ldir                       ; Repeat for the rest of the bytes
   
   jr loop                    ; go loop

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; CONSTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;  
.BUFFER_LENGTH       equ 20

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

.PrintedIndex         db 0  ; NOTE: PrintedIndex and BufferIndex  should be togheter on this order for use in pair registers, don't change
.BufferIndex          db 0

.Buffer              ds BUFFER_LENGTH - 1
                     db 0
