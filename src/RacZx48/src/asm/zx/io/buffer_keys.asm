SECTION code_user

PUBLIC _push_buffer_key       ; export C decl "extern void push_buffer_key(char key) __z88dk_fastcall;"
PUBLIC _pop_buffer_key        ; export C decl "extern void pop_buffer_key() __z88dk_fastcall;"



;-------------------------------------------------------------------------------
;  Name:		      public _push_buffer_key
;  Description:	insert key into input buffer
;  Input:		   L = key to push into buffer
;  Output: 	      --
;-------------------------------------------------------------------------------
_push_buffer_key:
   push hl                 ; store stack registers
   push bc
   push de

   ld e, l                 ; get parameter with ascii 

   ld a, (BufferIndex)
   cp BUFFER_LENGTH        ; compare with buffer size
   jr nc, push_exit        ; if index > buffer, exit routine
   
   ld hl, Buffer           ; HL = pointer to buffer
   ld c, a
   ld b, 0                         
   add hl, bc              ; add index offset to HL
   
   inc a                   ; increment buffer index
   ld (BufferIndex), a

   ld (hl), e              ; add parameter ascii to buffer
push_exit:
   pop de
   pop bc
   pop hl
   ret


_pop_buffer_key:
   push hl                 ; store stack registers
   push bc
   push de

   ld a, (BufferIndex)
   or a                    ; if BufferIndex == 0 ...
   jr z, pop_exit          ; ... can not pop, otherwise, delete char
   
   ld hl, Buffer           ; HL = pointer to buffer
   ld c, a
   ld b, 0                         
   add hl, bc              ; add index offset to HL
   
   dec a                   ; decrement buffer index
   ld (BufferIndex), a

   ld (hl), 0              ; reset ascii in buffer

pop_exit:
   pop de
   pop bc
   pop hl
   ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; CONSTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;                  
BUFFER_LENGTH     equ 10

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

.BufferIndex      db 0
.Buffer           ds BUFFER_LENGTH - 1
                  db 0
                  
