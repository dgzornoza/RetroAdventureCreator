SECTION code_user

PUBLIC _test_fn

_test_fn:
   ; ld e, l     ; parametro 

   ; ld a, (BufferIndex)
   ; cp zx_input_BUFFER_LENGTH      ; es menor que el tamaño del buffer?
   ; jp nc, zxTest_exit   ; si es mayor sale de la funcion
   
   ; ld hl, Buffer      ; HL = puntero al offset en el buffer
   ; ld c, a
   ; ld b, 0                         
   ; add hl, bc  
   
   ; inc a                ; incrementar buffer index
   ; ld (BufferIndex), a

   ; ld (hl), e           ; añadir el parametro al buffer
   
test_exit:     
   ret

