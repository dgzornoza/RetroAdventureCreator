SECTION code_user

PUBLIC _push_buffer_key      ; export C decl "extern void push_buffer_key(char key) __z88dk_fastcall;"




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
   jp nc, exit             ; if length > buffer, exit routine
   
   ld hl, Buffer           ; HL = pointer to buffer offset
   ld c, a
   ld b, 0                         
   add hl, bc  
   
   inc a                   ; increment buffer index
   ld (BufferIndex), a

   ld (hl), e              ; add parameter ascii to buffer
exit:     
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
                  
