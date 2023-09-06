SECTION code_user

PUBLIC _push_queue_key       ; export C decl "extern void push_queue_key(char key) __z88dk_fastcall;"
PUBLIC _pop_queue_key        ; export C decl "extern char pop_queue_key() __z88dk_fastcall;"
PUBLIC _clean_queue_keys        ; export C decl "extern void clean_queue_keys() __z88dk_fastcall;"


;-------------------------------------------------------------------------------
;  Name:		      public _push_queue_key
;  Description:	insert key into input queue buffer (FIFO)
;  Input:		   L = key to push into queue buffer
;  Output: 	      --
;-------------------------------------------------------------------------------
_push_queue_key:
   
   push bc                 ; store stack registers
   push de

   ld b, l                 ; B = input key

   ld a, (BufferIndex)
   cp BUFFER_LENGTH        ; compare with buffer size
   jr nc, push_exit        ; if index > buffer, exit routine
   
   ld hl, Buffer           ; HL = pointer to buffer
   ld e, a
   ld d, 00h                        
   add hl, de              ; add index offset to HL
   
   inc a                   ; increment buffer index
   ld (BufferIndex), a

   ld (hl), b              ; add parameter ascii to buffer

push_exit:
   
   pop de
   pop bc   
   ret


;-------------------------------------------------------------------------------
;  Name:		      public _pop_queue_key
;  Description:	get key from input queue buffer (FIFO)
;  Input:		   --
;  Output: 	      L = key to pop from input queue buffer
;-------------------------------------------------------------------------------
_pop_queue_key:
   
   push de                    ; store stack registers

   ld hl, Buffer              ; get first byte queued in A'
   ld a, (hl) 
   ex af, af'
   
   ld a, (BufferIndex)
   or a
   jr z, pop_exit             ; if BufferIndex == 0, exit routine

   ld de, Buffer              ; Set the start address
   ld hl, Buffer + 1          ; Set the next address   
   ld bc, (BufferIndex)       ; Set the number of bytes to set to 0 in BC
   ld b, 00h
   ldir                       ; Repeat for the rest of the bytes
   
   ex de,hl                   ; last byte to 0
   dec hl
   ld (hl),00h

   dec a                      ; decrement buffer index
   ld (BufferIndex), a

pop_exit:

   ex af, af'                 ; return result from A'
   ld l, a                    
   pop de
   ret


;-------------------------------------------------------------------------------
;  Name:		      public _clean_queue_keys
;  Description:	clean all buffer keys, set to 0 all buffer
;  Input:		   --
;  Output: 	      --
;-------------------------------------------------------------------------------
_clean_queue_keys:

   push bc                    ; store stack registers
   push de

   ld hl, Buffer              ; Set the start address
   ld de, Buffer + 1          ; Set the next address   
   ld bc, (BufferIndex)       ; Set the number of bytes to set to 0 in BC (BufferIndex - 1)
   ld b, 00h
   dec bc
   ld (hl), 00h               ; Set the first byte to 0
   ldir                       ; Repeat for the rest of the bytes

   xor a
   ld (BufferIndex), a        ; buffer index = 0

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
                  
