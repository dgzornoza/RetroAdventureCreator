SECTION code_user

; TODO: revisar si hace falta, en principio no deberia de necesitarse
;PUBLIC _print_char    ; export C decl "extern void print_char(char *ascii) __z88dk_fastcall;"
PUBLIC asm_print_char

EXTERN _GLOBAL_FONT_X
EXTERN _GLOBAL_FONT_CHARSET
EXTERN _GLOBAL_FONT_STYLE
EXTERN _GLOBAL_FONT_ATTRIBUTES


;-------------------------------------------------------------------------------
;  Name:		      public _print_char
;	Description:	print 8x8 pixels char from charset.
;	Input:		   HL = ASCII char to print
;	Output: 	      --
;	Clobbers: 	   --
;  Remarks:       _GLOBAL_FONT_CHARSET define charset memory address to use.
;                 _GLOBAL_FONT_X              = X Coordinate in low-res (0-31)
;                 _GLOBAL_FONT_Y              = Y Coordinate in low-res (0-23)
;                 _GLOBAL_FONT_ATTRIBUTES     = Print attributes to use
;                 _GLOBAL_FONT_STYLE          = Font style to use.
;-------------------------------------------------------------------------------
; TODO: dgzornoza - actualmente no se usa
; _print_char:
;    push bc
;    push de
;    ld a, (hl)                ; store in A char to print
;    call asm_print_char       ; print char
;    pop de
;    pop bc
;    ret



;-------------------------------------------------------------------------------
;	Name:		      internal asm_print_char
;	Description:	print 8x8 pixels char from charset.
;	Input:		   A = ASCII char to print
;	Output: 	      --
;	Clobbers: 	   DE, BC
;  Remarks:       _GLOBAL_FONT_CHARSET define charset memory address to use.
;                 _GLOBAL_FONT_X              = X Coordinate in low-res (0-31)
;                 _GLOBAL_FONT_Y              = Y Coordinate in low-res (0-23)
;                 _GLOBAL_FONT_ATTRIBUTES     = Print attributes to use
;                 _GLOBAL_FONT_STYLE          = Font style to use.
; This routine don't preserve registers (only internal use)
;-------------------------------------------------------------------------------
asm_print_char:
 
   ld bc, (_GLOBAL_FONT_X)          ; B = Y,  C = X
   ex af, af'                       ; store char in A'
   
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
   ld bc, (_GLOBAL_FONT_CHARSET)
   ld h, 0
   ld l, a
   add hl, hl
   add hl, hl
   add hl, hl
   add hl, bc              ; HL = BC + HL = FONT_CHARSET + (A * 8)
   
   ex de, hl               ; exchange DE y HL (DE=origin, HL=destination)
 
   ;;; Validate style to use
   ld a, (_GLOBAL_FONT_STYLE)   ; Get style
   or a
   jr nz, not_normal_style      ; jump if not normal style ( != 0)

;;;;;; NORMAL style ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
   ld B, 8                       ; 8 scanlines to draw
.drawchar_loop_normal:
   ld a, (de)                    ; get char data
   ld (hl), a                    ; set value in video memory
   inc de
   inc h
   djnz drawchar_loop_normal
   jr print_attribute            ; jupm to print attributes
 
.not_normal_style:
   cp FONT_BOLD                  ; is bold style?
   jr nz, not_bold_style         ; jump if not bold style
 
   ;;;;;; BOLD style ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
   ld b, 8                       ; 8 scanlines to draw
.drawchar_loop_bold:          
   ld a, (de)                    ; get char data
   ld c, a                       ; copy A
   rrca                          ; Shift A
   or c                          ; aggregate original in C
   ld (hl), a                    ; set value in video memory
   inc de                        ; increment character pointer
   inc h                         ; increment screen pointer (scanline+=1)
   djnz drawchar_loop_bold
   jr print_attribute
 
.not_bold_style:
   cp FONT_UNDERSCORE            ; is underscore style?
   jr nz, no_underscore_style    ; jump if not underscore style
 
   ;;;;;; UNDERSCORE style ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
   ld b, 7                       ; 7 scanlines to draw (last one is bottom line)
.drawchar_loop_underscore:
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
 
.no_underscore_style:
   cp FONT_ITALIC                ; is italic style?
   jr nz, unknown_style          ; jump if not italic style
 
   ;;;;;; ITALIC style ;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
   ;;; 3 first scanlines to right
   ld b, 3
.drawchar_loop_italic1:
   ld a, (de)                    ; get char data
   sra a                         ; shift right
   ld (hl), a                    ; set value in video memory
   inc de
   inc h
   djnz drawchar_loop_italic1
 
   ;;; 2 central scanlines are normal
   ld b, 2
.drawchar_loop_italic2:
   ld a, (de)                    ; get char data
   ld (hl), a                    ; set value in video memory
   inc de
   inc h
   djnz drawchar_loop_italic2
 
   ld b, 3
.drawchar_loop_italic3:
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
 
.unknown_style:                  ; unknown style ...
   ld B, 8                       ; print with normal style
   jr drawchar_loop_normal       

   ;;; print attributes ........................................................
.print_attribute:
 
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
   ld a, (_GLOBAL_FONT_ATTRIBUTES)
   ld (de), a                    ;  draw attibute in memory
   ret



; ----------------------------------------------------------------------------
; CONSTANTS
; ----------------------------------------------------------------------------

; ----------------------------------------------------------------------------
; FONT styles, should be equals to _GLOBAL_FONT_STYLEEnum in io.h
; ----------------------------------------------------------------------------
FONT_NORMAL      equ   0
FONT_BOLD        equ   1
FONT_UNDERSCORE  equ   2
FONT_ITALIC      equ   3
