SECTION code_user

EXTERN _LOWRES_SCR_WIDTH
EXTERN _LOWRES_SCR_HEIGHT
EXTERN _ROM_CHARSET

EXTERN _GLOBAL_FONT_CHARSET
EXTERN _GLOBAL_FONT_STYLE
EXTERN _GLOBAL_FONT_X
EXTERN _GLOBAL_FONT_Y
EXTERN _GLOBAL_FONT_ATTRIBUTES

EXTERN _zxPrintString

EXTERN asm_print_char

;-------------------------------------------------------------------------------
;	Name:		      private _setFontCharset
;	Description:	set font charset
;	Input:		   HL = pointer to charset
;	Output: 	      --
;-------------------------------------------------------------------------------
PUBLIC asm_setFontCharset
asm_setFontCharset:
   ld (_GLOBAL_FONT_CHARSET), hl
   ret
 

;-------------------------------------------------------------------------------
;	Name:		      private font_set_style
;	Description:	set text style
;	Input:		   A = Font style
;	Output: 	      --
;-------------------------------------------------------------------------------
PUBLIC asm_font_set_style
asm_font_set_style:
   ld (_GLOBAL_FONT_STYLE), a
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_set_x
;	Description:	set screen X cursor coordinate
;	Input:		   A = X coordinate
;	Output: 	      --
;-------------------------------------------------------------------------------
PUBLIC asm_font_set_x
asm_font_set_x:
   ld (_GLOBAL_FONT_X), a
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_set_y
;	Description:	set screen Y cursor coordinate
;	Input:		   A = Y coordinate
;	Output: 	      --
;-------------------------------------------------------------------------------
PUBLIC asm_font_set_y
asm_font_set_y:
   ld (_GLOBAL_FONT_Y), a
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_set_xy
;	Description:	set screen x,y cursor coordinate
;	Input:		   B = y coordinate
;                 C = x coordinate
;	Output: 	      --
;-------------------------------------------------------------------------------
PUBLIC asm_font_set_xy
asm_font_set_xy:
   ld (_GLOBAL_FONT_X), bc
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_set_ink
;	Description:	set ink value in current font attribute
;	Input:		   A = ink (0-7)
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_set_ink
asm_font_set_ink:
   push bc                    ; preserve registers
   and 7                      ; remove bits 7-3
   ld b, a                    ; store in b
   ld a, (_GLOBAL_FONT_ATTRIBUTES)     ; get current attributes
   and %11111000              ; remove ink value (last 3 bits)
   or b                       ; insert ink in a
   ld (_GLOBAL_FONT_ATTRIBUTES), a     ; save ink value
   pop bc
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_set_paper
;	Description:	set paper value in current attributes
;	Input:		   A = paper (0-7)
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_set_paper
asm_font_set_paper:
   push bc                    ; preserve register
   and 7                      ; remove bits 7-3
   rlca                       ; sifht a = 00000xxx -> 0000xxx0
   rlca                       ; sifht a = 000xxx00
   rlca                       ; sifht a = 00xxx000 <-- paper value in attributes
   ld b, a                    ; store in b
   ld a, (_GLOBAL_FONT_ATTRIBUTES)     ; get current attribute
   and %11000111              ; delete paper value
   or b                       ; insert new paper
   ld (_GLOBAL_FONT_ATTRIBUTES), a     ; save paper value in var
   pop bc
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_set_attributes
;	Description:	set current attribute value 
;	Input:		   A = ink
;-------------------------------------------------------------------------------
PUBLIC asm_font_set_attributes
asm_font_set_attributes:
   ld (_GLOBAL_FONT_ATTRIBUTES), a
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_set_bright
;	Description:	set bright value in current attributes (1/0) (bit 6)
;	Input:		   A = bright (1/0)
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_set_bright
asm_font_set_bright:
   and 1                      ; only bit 0 in a
   ld a, (_GLOBAL_FONT_ATTRIBUTES)     ; set attributes in a
   jr nz, bright_1            ; if and 1 == 1 jump to set bright
   res 6, a                   ; unset bright flag
   ld (_GLOBAL_FONT_ATTRIBUTES), a     ; save value in var
   ret
bright_1:
   set 6, a                   ; set bright flag
   ld (_GLOBAL_FONT_ATTRIBUTES), a     ; save value in var
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_set_flash
;	Description:	set flash value in current attributes (1/0) (bit 7)
;	Input:		   A = flash (1/0)
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_set_flash
asm_font_set_flash:
   and 1                      ; only bit 0 in a
   ld a, (_GLOBAL_FONT_ATTRIBUTES)     ; set attributes in a
   jr nz, flash_1             ; if and 1 == 1 jump to set flash
   res 7, a                   ; unset fash flag
   ld (_GLOBAL_FONT_ATTRIBUTES), a     ; save value in var  
   ret
flash_1:
   set 7, a                   ; set flash flag
   ld (_GLOBAL_FONT_ATTRIBUTES), a     ; save value in var
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_blank
;	Description:	print space, override current position in cursor and increment X (updating y if need)
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_blank
asm_font_blank:
   ld a, ' '            ; set char space
   push bc
   push de
   push hl
   call asm_print_char    ; print char
   pop hl
   pop de
   pop bc
   call asm_font_inc_x      ; increment x coord
   ret
 
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_inc_x
;	Description:	increments x coordinate by 1 taking into account the edge on the screen (updating Y accordingly).
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_inc_x
asm_font_inc_x:
   ld a, (_GLOBAL_FONT_X)              ; increment x
   inc a                     
   cp _LOWRES_SCR_WIDTH - 1    ; Compare with right border (x > 31)
   jr c, update_x             ; jump if not is in right border
   call asm_font_crlf
   ret
 
update_x:
   ld (_GLOBAL_FONT_X), a              ; store X coordinate in var
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_lf
;	Description:	Generate un linefeed (increment Y by 1). 
;                 Takes into account the height variables of the screen.
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_lf
asm_font_lf:
   ld a, (_GLOBAL_FONT_Y)              ; get Y coordinate
   cp _LOWRES_SCR_HEIGHT - 1   ; Compare with bottom border (y > 23)
   jr nc, update_y            ; jump if (y = 23)
   inc a                      ; (y < 23), increment Y
   ld (_GLOBAL_FONT_Y), a              ; store Y coordinate in var
 
update_y:
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_cr
;	Description:	Generate carriage return (CR) => x=0.
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_cr
asm_font_cr:
   xor a
   ld (_GLOBAL_FONT_X), a
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_crlf
;	Description:	Generate linefeed and Carriage return (lf+cr).
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_crlf
asm_font_crlf:
   call asm_font_lf
   call asm_font_cr
   ret
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_tab
;	Description:	print tabulator (3 spaces) using printstring
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_tab
asm_font_tab:
   push bc
   push de
   push hl
   ld hl, FontTabString
   call _zxPrintString      ; print 3 spaces
   pop hl
   pop de
   pop bc
   ret 
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_dec_x
;	Description:	decrement X cursor coordinate withouth delete char
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_dec_x
asm_font_dec_x:
   ld a, (_GLOBAL_FONT_X)     ; get X coordinate
   or a
   ret z             ; if is Zero, exit
   dec a             ; decrement and store in var
   ld (_GLOBAL_FONT_X), a
   ret               ; exit
 
 
;-------------------------------------------------------------------------------
;	Name:		      private font_backspace
;	Description:	decrement X cursor, deleting char backspace)
;                 delete char printing space.
;	Clobbers: 	   AF
;-------------------------------------------------------------------------------
PUBLIC asm_font_backspace
asm_font_backspace:
   call asm_font_dec_x
   ld a, ' '               ; space to print
   push bc
   push de
   push hl
   call asm_print_char         ; override char with space
   pop hl
   pop de
   pop bc
   ret                     ; exit


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

;-------------------------------------------------------------
; Table with 16 control code routines adresses.
; 9 routine is empty for expand if need.
;-------------------------------------------------------------
PUBLIC FontControlCodeRoutines
   FontControlCodeRoutines:
      dw 0000, asm_font_set_style, asm_font_set_x, asm_font_set_y, asm_font_set_ink
      dw asm_font_set_paper, asm_font_set_attributes, asm_font_set_bright
      dw asm_font_set_flash, 0000, asm_font_lf, asm_font_crlf, asm_font_blank
      dw asm_font_cr, asm_font_backspace, asm_font_tab, asm_font_inc_x

.FontTabString       db  "   ", 0