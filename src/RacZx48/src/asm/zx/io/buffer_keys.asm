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

   push bc                 ; store stack registers
   push de

   ld b, l                 ; B = input key

   ld a, (BufferIndex)
   cp BUFFER_LENGTH        ; compare with buffer size
   jr nc, push_exit        ; if index > buffer, exit routine
   
   ld hl, Buffer           ; HL = pointer to buffer
   ld e, a
   ld d, 0                         
   add hl, de              ; add index offset to HL
   
   inc a                   ; increment buffer index
   ld (BufferIndex), a

   ld (hl), b              ; add parameter ascii to buffer

push_exit:
   pop de
   pop bc   
   ret


;-------------------------------------------------------------------------------
;  Name:		      public _pop_buffer_key
;  Description:	get key from input buffer
;  Input:		   --
;  Output: 	      L = key to pop from input buffer
;-------------------------------------------------------------------------------
_pop_buffer_key:
   
   push de                 ; store stack registers

   ld hl, 0                ; by default result = 0
   
   ld a, (BufferIndex)
   or a                    ; if BufferIndex == 0 ...
   jr z, pop_exit          ; ... can not pop, otherwise, delete char
   
   ld hl, Buffer           ; HL = pointer to buffer
   ld e, a
   ld d, 0                         
   add hl, de              ; add index offset to HL
   
   dec a                   ; decrement buffer index
   ld (BufferIndex), a

   ld e, (hl)              ; E = key from buffer
   ld (hl), 0              ; reset ascii in buffer
   ld l, e

pop_exit:
   pop de
   ret


;-------------------------------------------------------------------------------
;  Name:		      public _clean_buffer_keys
;  Description:	clean all buffer keys, set to 0 all buffer
;  Input:		   --
;  Output: 	      --
;-------------------------------------------------------------------------------
_clean_buffer_keys:

   push bc                 ; store stack registers
   push de

   ld hl, Buffer           ; Set the start address
   ld de, Buffer + 1       ; Set the next address
   ld bc, (BufferIndex)    ; Set the number of bytes to set to 0
   ld (hl), 0              ; Set the first byte to 0
   ldir                    ; Repeat for the rest of the bytes

clean_exit:
   pop de
   pop bc
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
                  
