SECTION code_user

EXTERN _ROM_CHARSET

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

PUBLIC _GLOBAL_FONT_CHARSET
    _GLOBAL_FONT_CHARSET:       dw _ROM_CHARSET

PUBLIC _GLOBAL_FONT_ATTRIBUTES
    _GLOBAL_FONT_ATTRIBUTES:    db 56      ; black over gray

PUBLIC _GLOBAL_FONT_STYLE
    _GLOBAL_FONT_STYLE:         db 0

PUBLIC _GLOBAL_FONT_X
    _GLOBAL_FONT_X:             db 0       ; FontX and FontY should be togheter on this order, don't change
PUBLIC _GLOBAL_FONT_Y
    _GLOBAL_FONT_Y:             db 0  

