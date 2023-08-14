SECTION code_user

PUBLIC _printChar8x8    ; export C decl "extern void printChar8x8(char character) __z88dk_fastcall;"
PUBLIC _printString8x8

EXTERN _FontCharset     ; access global C variable "FontCharset" (uint8_t *FontCharset;) 
EXTERN _FontCoordinates  ;access global C variable "FontCoordinates" struct Coordinates { uint8_t X; uint8_t Y; }; (struct Coordinates FontCoordinates;)
EXTERN _FontAttributes  ; access global C variable "FontAttributes" (uint8_t FontAttributes;)
EXTERN _FontStyle       ; access global C variable "FontStyle" (uint8_t FontStyle;)
EXTERN _FontStyleEnum

;-------------------------------------------------------------
; printChar8x8:
; print 8x8 pixels char from charset.
;
; Input
; -----------------------------------------------------
; FontCharset       = Charset memory address.
; FontCoordinates   = X,Y Coordinate in low-res (0-31, 0-23) (struct with X,Y)
; FontAttributes    = Print attribute to use
; FontStyle         = Font style to use (0-N).
; Registro A        = ASCII char to print
;-------------------------------------------------------------
_printChar8x8:
 
   ;ld a, l                     ; set to A function parameter in HL (TODO: dgzornoza intentar eliminar modificando el registro)

   ld bc, (_FontCoordinates)   ; B = Y,  C = X
   ex af, af'                  ; store char in A'
   
   ;;; calculate destination screen coordinates in DE.
   ld a, b
   and $18
   add a, $40
   ld d, a
   ld a, b
   and 7
   rrca
   rrca
   rrca
   add a, c
   ld e, a                 ; DE contains destination address.

   ;;; calculate origin position (array sprites) in HL: adress = base_sprites + (num_sprite * 8)
   ex af, af'              ; get char to print in A'
   ld bc, (_FontCharset)
   ld h, 0
   ld l, a
   add hl, hl
   add hl, hl
   add hl, hl
   add hl, bc              ; HL = BC + HL = FONT_CHARSET + (A * 8)
   
   ex de, hl               ; exchange DE y HL (DE=origin, HL=destination)
 
   ;;; Validate style to use
   ld a, (_FontStyle)           ; Get style
   or a
   jr nz, not_normal_style      ; jump if not normal style ( != 0)

;;;;;; NORMAL style ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
   ld B, 8                       ; 8 scanlines to draw
drawchar_loop_normal:
   ld a, (de)                    ; get char data
   ld (hl), a                    ; set value in video memory
   inc de
   inc h
   djnz drawchar_loop_normal
   jr print_attribute            ; jupm to print attributes
 
not_normal_style:
   cp FONT_BOLD                  ; is bold style?
   jr nz, not_bold_style         ; jump if not bold style
 
   ;;;;;; BOLD style ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
   ld b, 8                       ; 8 scanlines to draw
drawchar_loop_bold:          
   ld a, (de)                    ; get char data
   ld c, a                       ; copy A
   rrca                          ; Shift A
   or c                          ; aggregate original in C
   ld (hl), a                    ; set value in video memory
   inc de                        ; increment character pointer
   inc h                         ; increment screen pointer (scanline+=1)
   djnz drawchar_loop_bold
   jr print_attribute
 
not_bold_style:
   cp FONT_UNDERSCORE            ; is underscore style?
   jr nz, no_underscore_style    ; jump if not underscore style
 
   ;;;;;; UNDERSCORE style ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
   ld b, 7                       ; 7 scanlines to draw (last one is bottom line)
drawchar_loop_underscore:
   ld a, (de)                    ; get char data
   ld (hl), a                    ; set value in video memory
   inc de
   inc h
   djnz drawchar_loop_underscore
 
   ;;; 8 scanline, underscore line
   ld a, 255                     ; underscore
   ld (hl), a
   inc h                         
   jr print_attribute
 
no_underscore_style:
   cp FONT_ITALIC                ; is italic style?
   jr nz, unknown_style          ; jump if not italic style
 
   ;;;;;; ITALIC style ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
   ;;; 3 first scanlines to right
   ld b, 3
drawchar_loop_italic1:
   ld a, (de)                    ; get char data
   sra a                         ; shift right
   ld (hl), a                    ; set value in video memory
   inc de
   inc h
   djnz drawchar_loop_italic1
 
   ;;; 2 central scanlines are normal
   ld b, 2
drawchar_loop_italic2:
   ld a, (de)                    ; get char data
   ld (hl), a                    ; set value in video memory
   inc de
   inc h
   djnz drawchar_loop_italic2
 
   ld b, 3
drawchar_loop_italic3:
   ;;; 3 last scanlines to left
   ld a, (de)                    ; get char data
   sla a                         ; shift left
   ld (hl), a                    ; set value in video memory
   inc de
   inc h
   djnz drawchar_loop_italic3
   jr print_attribute
 
;;; add more styles ............................................................
;;; ...
 
unknown_style:                   ; unknown style ...
   LD B, 8                       ; print with normal style
   JR drawchar_loop_normal       

   ;;; print attributes ........................................................
print_attribute:
 
   ld a, h                       ; get HL initial value
   sub 8                         ; substract 8 advanced scanlines
 
   ;;; calculate attributes area position in DE (currently A = H)
   rrca                          ; shift A 3 times (A = A >> 3)
   rrca
   rrca               
   and 3                         ; A = A AND 00000011 = 2 high bits from row
   or $58
   ld d, a
   ld e, l
 
   ;;; draw attibute in memory
   ld a, (_FontAttributes)
   ld (de), a                    ;  draw attibute in memory
   ret



;-------------------------------------------------------------
; _printString8x8:
; print string with format in screen
;
; Input
; -----------------------------------------------------
; FontCharset       = Charset memory address.
; FontCoordinates   = X,Y Coordinate in low-res (0-31, 0-23) (struct with X,Y)
; FontAttributes    = Print attribute to use
; FontStyle         = Font style to use (0-N).
; Registro HL       = ASCII char to print
;-------------------------------------------------------------
_printString8x8:

   ;;; Bucle de impresion de caracter
string_loop:
   LD A, (HL)                ; Leemos un caracter de la cadena
   OR A
   RET Z                     ; Si es 0 (fin de cadena) volver
   INC HL                    ; Siguiente caracter en la cadena
   PUSH HL                   ; Salvaguardamos HL
   CALL _printChar8x8        ; Imprimimos el caracter
   POP HL                    ; Recuperamos HL
 
   ;;; Ajustamos coordenadas X e Y
   LD A, (_FontCoordinates)            ; Incrementamos la X
   INC A                     ; pero comprobamos si borde derecho
   CP 31                     ; X > 31?
   JR C, noedge_x    ; No, se puede guardar el valor
 
   LD A, (_FontCoordinates + 1)            ; Cogemos coordenada Y
   CP 23                     ; Si ya es 23, no incrementar
   JR NC, noedge_y   ; Si es 23, saltar
 
   INC A                     ; No es 23, cambiar Y
   LD (_FontCoordinates + 1), A
 
noedge_y:
   LD (_FontCoordinates + 1), A        ; Guardamos la coordenada Y
   XOR A                               ; Y ademas hacemos A = X = 0
 
noedge_x:
   LD (_FontCoordinates), A            ; Almacenamos el valor de X
   JR string_loop

   

   
; CONsTANTS --------------------------------------------------------------------------



;;; FONT styles, should be equals to FontStyleEnum in graphics.h
FONT_NORMAL      EQU   0
FONT_BOLD        EQU   1
FONT_UNDERSCORE  EQU   2
FONT_ITALIC      EQU   3


LOWRES_SCR_WIDTH    EQU   32
LOWRES_SCR_HEIGHT   EQU   24