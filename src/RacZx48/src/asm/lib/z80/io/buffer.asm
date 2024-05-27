SECTION code_user


;-------------------------------------------------------------------------------
;  Name:		      internal _push_buffer_item
;  Description:	insert item into queue buffer (FIFO)
;  Input:		   HL = items buffer
;                 A = item to insert
;                 B = BufferIndex
;                 C = Buffer length
;
;  Output: 	      B = buffer index
;-------------------------------------------------------------------------------
PUBLIC asm_push_buffer_item
asm_push_buffer_item:
   
   push bc                 ; store stack registers
   push de

   ex af, af'              ; A' = input item

   ld a, b                 ; A = B = buffer index
   cp c                    ; compare with buffer size
   jr nc, push_exit        ; if index > buffer, exit routine
   
   ld d, 00h                        
   ld e, a
   add hl, de              ; add index offset to HL (buffer)
   
   ld b, a                 ; B = buffer index (return value)
   inc b                   ; increment buffer index

   ex af, af'
   ld (hl), a              ; add item to buffer

.push_exit:
   
   pop de
   pop bc   
   ret


;-------------------------------------------------------------------------------
;  Name:		      internal _pop_buffer_item
;  Description:	get item from input queue buffer (FIFO)
;  Input:		   HL = items buffer
;                 B = BufferIndex
;                 C = Buffer length
;
;  Output: 	      A = key to pop from input queue buffer
;                 B = buffer index
;-------------------------------------------------------------------------------
PUBLIC asm_pop_buffer_item
asm_pop_buffer_item:
   
   push de                    ; store stack registers

   ;ld hl, Buffer             
   ld a, (hl)                 ; store first byte queued in A'
   ex af, af'
   
   ld a, b                    ; A = B = buffer index
   or a
   jr z, pop_exit             ; if BufferIndex == 0, exit routine

   ; move bytes to the left in buffer to pop first byte
   ld de, hl                  ; Set the start buffer address
   inc hl
   ;ld hl, Buffer + 1          ; Set the next address   
   ;ld bc, (BufferIndex)       ; Set the number of bytes to set to 0 in BC
   ld b, 00h
   ld c, a                    ; Set the number of bytes to move (HL<->DE) in BC
   ldir                       ; Repeat BC number of times
   
   ex de,hl                   ; last byte to 0
   dec hl
   ld (hl), 00h

   ld b, a                 ; B = buffer index (return value)
   dec b                   ; decrement buffer index

.pop_exit:

   ex af, af'                 ; return result from A'
   ;ld l, a                    
   pop de
   ret


;-------------------------------------------------------------------------------
;  Name:		      internal _clean_buffer_items
;  Description:	clean all buffer items, set to 0 all buffer
;  Input:		   HL = items buffer
;                 B = BufferIndex
;                 C = Buffer length
;
;  Output: 	      B = buffer index
;-------------------------------------------------------------------------------
PUBLIC asm_clean_buffer_items
asm_clean_buffer_items:

   push bc                    ; store stack registers
   push de

   ;ld hl, Buffer              ; Set the start address
   ld de, hl + 1              ; Set the next address   
   ;ld bc, (BufferIndex)       ; Set the number of bytes to set to 0 in BC (BufferIndex - 1)   
   ld b, 00h
   ld c, b                    
   dec bc                     ; Set the number of bytes to set to 0 in BC (BufferIndex - 1) 
   ld (hl), 00h               ; Set the first byte to 0
   ldir                       ; Repeat BD number of times

   xor b                      ; buffer index = 0
   ;ld (BufferIndex), a        ; buffer index = 0

.clean_exit:
   pop de
   pop bc
   ret


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; CONSTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;                  


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;


                  
