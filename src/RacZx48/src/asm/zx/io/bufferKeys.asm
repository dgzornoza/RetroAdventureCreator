SECTION code_user

PUBLIC _zx_pushBufferKey      ; export C decl "extern void zx_pushBufferKey(char key) __z88dk_fastcall;"


BUFFER_LENGTH     equ 10

;-------------------------------------------------------------------------------
;  Name:		      public _zx_input_pushBufferKey
;  Description:	insert key into input buffer
;  Input:		   L = key to push into buffer
;  Output: 	      --
;-------------------------------------------------------------------------------
_zx_pushBufferKey:
   push hl
   push bc
   push de
   ld e, l     ; parametro 

   ld a, (BufferIndex)
   cp BUFFER_LENGTH      ; es menor que el tamaño del buffer?
   jp nc, exit   ; si es mayor sale de la funcion
   
   ld hl, Buffer      ; HL = puntero al offset en el buffer
   ld c, a
   ld b, 0                         
   add hl, bc  
   
   inc a                ; incrementar buffer index
   ld (BufferIndex), a

   ld (hl), e           ; añadir el parametro al buffer
exit:     
   ret


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

.BufferIndex      db 0
.Buffer           ds BUFFER_LENGTH - 1
                  db 0
                  
