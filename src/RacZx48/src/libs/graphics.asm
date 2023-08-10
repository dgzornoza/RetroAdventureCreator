SECTION code_user

PUBLIC _printChar8x8    ; export C decl "extern void printChar8x8(char character) __z88dk_fastcall;"


EXTERN _FontCharset     ; access global C variable "FontCharset" (uint8_t *FontCharset;) 
EXTERN _FontCoordinates  ;access global C variable "_FontCoordinates" struct Coordinates { uint8_t X; uint8_t Y; }; (struct Coordinates FontCoordinates;)
EXTERN _FontAttributes  ; access global C variable "FontAttributes" (uint8_t FontAttributes;)
EXTERN _FontStyle       ; access global C variable "FontStyle" (uint8_t FontStyle;)

;-------------------------------------------------------------
; printChar8x8:
; print 8x8 pixels char from charset.
;
; Input:
; -----------------------------------------------------
; FontCharset       = Charset memory address.
; FontCoordinates   = X,Y Coordinate in low-res (0-31, 0-23) (struct with X,Y)
; FontAttributes    = Print attribute to use
; Registro A        = ASCII char to print
;-------------------------------------------------------------
_printChar8x8:
 
    ld a, l                     ; set to A function parameter in HL (TODO: dgzornoza intentar eliminar modificando el registro)

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
    
    ;;; draw 7 scanlines (DE) -> (HL) and down scanline (DE++)
    ld b, 7                 ; 7 scanlines to draw
    
drawchar8_loop:
    ld a, (de)              ; get char data
    ld (hl), a              ; set value in screen memory
    inc de                  ; increment pointer in char
    inc h                   ; increment pointer in screen (scanline += 1)
    djnz drawchar8_loop
    
    ;;; eight iteration (8º scanline) separate for avoid incs
    ld a, (de)              ; get char data
    ;or (hl)                ; or between data and screen  TODO: dgzornoza: over?
    ld (hl), a              ; set value in screen memory

    ld a, h                 ; get initial value from hl
    sub 7                   ; subtract the 7 "inc h" made
    
    ;;; calculate destination position in attributes area in DE (currently A = H)
    rrca                    ; code from get_attr_offset_from_image
    rrca
    rrca
    and 3
    or $58
    ld d, a
    ld e, l
    
    ;;; write attribute in memory
    ld a, (_FontAttributes)
    ld (de), a              ; write attribute in memory
    ret

; CONsTANTS --------------------------------------------------------------------------

FONT_NORMAL      EQU   0
FONT_BOLD        EQU   1
FONT_UNDERSC     EQU   2
FONT_ITALIC      EQU   3

LOWRES_SCR_WIDTH    EQU   32
LOWRES_SCR_HEIGHT   EQU   24