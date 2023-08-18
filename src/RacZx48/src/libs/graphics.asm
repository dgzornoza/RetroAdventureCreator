SECTION code_user

PUBLIC _printString  ; export C decl "extern void printString(char *string) __z88dk_fastcall;"

PUBLIC _FontCharset
; PUBLIC FontAttributes    DB    56      ; black over gray
; PUBLIC FontStyle         DB    0
; PUBLIC FontX             DB    0       ; FontX and FontY should be togheter on this order, don't change
; PUBLIC FontY             DB    0       

;-------------------------------------------------------------
; public _printString:
; print string with optional control codes in screen
;
; Use DE, BC
; Input
; -----------------------------------------------------
; _FontCharset        = Charset memory address.
; FontX              = X Coordinate in low-res (0-31)
; FontY              = Y Coordinate in low-res (0-23)
; FontAttributes     = Print attribute to use
; FontStyle          = Font style to use.
; Register HL        = ASCII char to print
;-------------------------------------------------------------
_printString:
 
string_loop:
   ld a, (hl)                 ; Get char from string
   inc hl                     ; pointer to next char
 
   cp 32                      ; id control code? (char < 32)
   jp c,  string_control_code ; jump if is control code
 
   push hl                    ; store HL
   call printChar8x8          ; print char
   pop hl                     ; restore HL
 
   ;;; increment cursor using font_blanck, increment x and update x and y if need
   call font_inc_x            ; increment X
   jr string_loop             ; continue with next char in string
 
string_control_code:
   or a                       ; if control code is eos (end of string), exit 
   ret z                     
 
   ;;; get control code routine and call it.
   ex de, hl
   ld hl, FontControlCodeRoutines
   rlca                             ; a = a * 2 = control code * 2 (routines addresses are 2 bytes)
   ld c, a
   ld b, 0                          ; bc = a * 2
   add hl, bc                       ; hl = dir FontControlCodeRoutines + (control code routine address)
   ld c, (hl)                       ; read low register in C...
   inc hl                           ; ... for no broken HL and read ...
   ld h, (hl)                       ; ... hight register from h ...
   ld l, c                          ; can't use A register because is used in CP
 
   ;;; if control > 0 y control < 10 -> get first parameter and jump to routine
   ;;; if control > 9 y ccontrol < 32 -> jump to routine withouth parameter
   cp 18                            ; control < 10 ((control - 1) * 2 < 18 )
   jp nc, string_call_routine       ; if is < 10, jump withouth param
 
   ;;; if control < 10 -> get parameter:
   ld a, (de)                       ; get parameter from string
   inc de                           ; pointer to next char
 
   ;;; code for call control code routine
string_call_routine:
   ld bc, string_end_call_routine   ; store return address
   push bc                          
   jp (hl)                          ; jump to routine for manage control code
 
   ;;; end code for control code routine
string_end_call_routine:
   ex de, hl                        ; set in HL string pointer
   jr string_loop                 ; continue loop
   



;-------------------------------------------------------------
; private printChar8x8:
; print 8x8 pixels char from charset.
;
; Input
; -----------------------------------------------------
; _FontCharset        = Charset memory address.
; FontX              = X Coordinate in low-res (0-31)
; FontY              = Y Coordinate in low-res (0-23)
; FontAttributes     = Print attributes to use
; FontStyle          = Font style to use.
; Register A         = ASCII char to print
;-------------------------------------------------------------
.printChar8x8:
 
   ld bc, (FontX)          ; B = Y,  C = X
   ex af, af'              ; store char in A'
   
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
   ld a, (FontStyle)            ; Get style
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
   ld a, (FontAttributes)
   ld (de), a                    ;  draw attibute in memory
   ret




 
;-------------------------------------------------------------
; set charset in use
; input :  HL = charset address memory
;-------------------------------------------------------------
.font_set_charset:
   ld (_FontCharset), hl
   ret
 
 
;-------------------------------------------------------------
; set text style
; input :  A = style
;-------------------------------------------------------------
.font_set_style:
   ld (FontStyle), a
   ret
 
 
;-------------------------------------------------------------
; set screen x cursor coordinate
; input :  A = x coordinate
;-------------------------------------------------------------
.font_set_x:
   ld (FontX), a
   ret
 
 
;-------------------------------------------------------------
; set screen y cursor coordinate
; input :  A = y coordinate
;-------------------------------------------------------------
.font_set_y:
   ld (FontY), a
   ret
 
 
;-------------------------------------------------------------
; set screen x,y cursor coordinate
; input :  B = y coordinate
;          C = x coordinate
;-------------------------------------------------------------
.font_set_xy:
   ld (FontX), bc
   ret
 
 
;-------------------------------------------------------------
; set ink value in current font attribute
; input :  A = ink (0-7)
; modify:  AF
;-------------------------------------------------------------
.font_set_ink:
   push bc                    ; preserve registers
   and 7                      ; remove bits 7-3
   ld b, a                    ; store in b
   ld a, (FontAttributes)     ; get current attributes
   and %11111000              ; remove ink value (last 3 bits)
   or b                       ; insert ink in a
   ld (FontAttributes), a     ; save ink value
   pop bc
   ret
 
 
;-------------------------------------------------------------
; set paper value in current attributes
; input :  A = paper (0-7)
; modify:  AF
;-------------------------------------------------------------
.font_set_paper:
   push bc                    ; preserve register
   and 7                      ; remove bits 7-3
   rlca                       ; sifht a = 00000xxx -> 0000xxx0
   rlca                       ; sifht a = 000xxx00
   rlca                       ; sifht a = 00xxx000 <-- paper value in attributes
   ld b, a                    ; store in b
   ld a, (FontAttributes)     ; get current attribute
   and %11000111              ; delete paper value
   or b                       ; insert new paper
   ld (FontAttributes), a     ; save paper value in var
   pop bc
   ret
 
 
;-------------------------------------------------------------
; set current attribute value 
; input :  A = ink
;-------------------------------------------------------------
.font_set_attrib:
   ld (FontAttributes), a
   ret
 
 
;-------------------------------------------------------------
; set bright value in current attributes (1/0) (bit 6)
; input :  A = bright (1/0)
; modify:  AF
;-------------------------------------------------------------
.font_set_bright:
   and 1                      ; only bit 0 in a
   ld a, (FontAttributes)     ; set attributes in a
   jr nz, bright_1            ; if and 1 == 1 jump to set bright
   res 6, a                   ; unset bright flag
   ld (FontAttributes), a     ; save value in var
   ret
bright_1:
   set 6, a                   ; set bright flag
   ld (FontAttributes), a     ; save value in var
   ret
 
 
;-------------------------------------------------------------
; set flash value in current attributes (1/0) (bit 7)
; input :  A = flash (1/0)
; modify:  AF
;-------------------------------------------------------------
.font_set_flash:
   and 1                      ; only bit 0 in a
   ld a, (FontAttributes)     ; set attributes in a
   jr nz, flash_1             ; if and 1 == 1 jump to set flash
   res 7, a                   ; unset fash flag
   ld (FontAttributes), a     ; save value in var  
   ret
flash_1:
   set 7, a                   ; set flash flag
   ld (FontAttributes), a     ; save value in var
   ret
 
 
;-------------------------------------------------------------
; print space, override current position in cursor and increment X (updating y if need)
; modify:  AF
;-------------------------------------------------------------
.font_blank:
   ld a, ' '            ; set char space
   push bc
   push de
   push hl
   call printChar8x8    ; print char
   pop hl
   pop de
   pop bc
   call font_inc_x      ; increment x coord
   ret
 
 
 
;-------------------------------------------------------------
; increments x coordinate by 1 taking into account the edge on the screen (updating Y accordingly).
; modify:  AF
;-------------------------------------------------------------
.font_inc_x:
   ld a, (FontX)              ; increment x
   inc a                     
   cp LOWRES_SCR_WIDTH - 1    ; Compare with right border (x > 31)
   jr c, update_x             ; jump if not is in right border
   call font_crlf
   ret
 
update_x:
   ld (FontX), a              ; store X coordinate in var
   ret
 
 
;-------------------------------------------------------------
; Generate un linefeed (increment Y by 1). 
; Takes into account the height variables of the screen.
; modify:  AF
;-------------------------------------------------------------
.font_lf:
   ld a, (FontY)              ; get Y coordinate
   cp LOWRES_SCR_HEIGHT - 1   ; Compare with bottom border (y > 23)
   jr nc, update_y            ; jump if (y = 23)
   inc a                      ; (y < 23), increment Y
   ld (FontY), a              ; store Y coordinate in var
 
update_y:
   ret
 
 
;-------------------------------------------------------------
; Generate carriage return (CR) => x=0.
; Modify:  AF
;-------------------------------------------------------------
.font_cr:
   xor a
   ld (FontX), a
   ret
 
 
;-------------------------------------------------------------
; Generate linefeed and Carriage return (lf+cr).
; Modify:  AF
;-------------------------------------------------------------
.font_crlf:
   call font_lf
   call font_cr
   ret
 
 
;-------------------------------------------------------------
; print tabulator (3 spaces) using printstring
; modify:  AF
;-------------------------------------------------------------
.font_tab:
   push bc
   push de
   push hl
   ld hl, FontTabString
   call _printString      ; print 3 spaces
   pop hl
   pop de
   pop bc
   ret 
 
 
;-------------------------------------------------------------
; decrement X cursor coordinate withouth delete char
; Modify:  AF
;-------------------------------------------------------------
.font_dec_x:
   ld a, (FontX)     ; get X coordinate
   or a
   ret z             ; if is Zero, exit
   dec a             ; decrement and store in var
   ld (FontX), a
   ret               ; exit
 
 
;-------------------------------------------------------------
; decrement X cursor, deleting char backspace)
; delete char printing space.
; Modify:  AF
;-------------------------------------------------------------
.font_backspace:
   call font_dec_x
   ld a, ' '               ; space to print
   push bc
   push de
   push hl
   call printChar8x8       ; override char with space
   pop hl
   pop de
   pop bc
   ret                     ; exit




; VARIABLES --------------------------------------------------------------------
._FontCharset       DW    $3C00
.FontAttributes    DB    56      ; black over gray
.FontStyle         DB    0
.FontX             DB    0       ; FontX and FontY should be togheter on this order, don't change
.FontY             DB    0

.FontTabString     DB  "   ", 0

;-------------------------------------------------------------
; Table with 16 control code routines adresses.
; 9 routine is empty for expand if need.
;-------------------------------------------------------------
.FontControlCodeRoutines:
   dw 0000, font_set_style, font_set_x, font_set_y, font_set_ink
   dw font_set_paper, font_set_attrib, font_set_bright
   dw font_set_flash, 0000, font_lf, font_crlf, font_blank
   dw font_cr, font_backspace, font_tab, font_inc_x


; CONsTANTS --------------------------------------------------------------------

;;; FONT styles, should be equals to FontStyleEnum in graphics.h
FONT_NORMAL      EQU   0
FONT_BOLD        EQU   1
FONT_UNDERSCORE  EQU   2
FONT_ITALIC      EQU   3

LOWRES_SCR_WIDTH    EQU   32
LOWRES_SCR_HEIGHT   EQU   24