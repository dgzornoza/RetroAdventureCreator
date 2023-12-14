SECTION code_user

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; ROM CONSTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

PUBLIC _ROM_FRAMES      ; 3 byte (least significant first), frame counter. Incremented every 20ms.
    _ROM_FRAMES         equ $5C78
PUBLIC _ROM_LAST_KEY    ; address for get last key pressed
    _ROM_LAST_KEY       equ $5C08
PUBLIC _ROM_KEY_SCAN    ; Routine address for scan keyboard keys
    _ROM_KEY_SCAN       equ $028E   
PUBLIC _ROM_CHARSET     ; ROM address with default charset
    _ROM_CHARSET:       equ $3C00
public _ROM_OPEN_CHAIN: ; Routine address for open chain screen (1 = command line screen, 2 = upper screen)
    _ROM_OPEN_CHAIN:    equ $1601

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; SYSTEM CONSTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

PUBLIC _SYS_TICKS_PER_SECOND    ; ticks per seccond
    _SYS_TICKS_PER_SECOND       equ 50

PUBLIC _SYS_LOWRES_SCR_WIDTH    ; Low-Resolution width
    _SYS_LOWRES_SCR_WIDTH       equ 32
PUBLIC _SYS_LOWRES_SCR_HEIGHT   ; Low-Resolution height
    _SYS_LOWRES_SCR_HEIGHT      equ 24


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; DEFAULTS CONSTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
PUBLIC _DEFAULT_FONT_ATTRIBUTES     ; default font attributes, black over gray
    _DEFAULT_FONT_ATTRIBUTES        equ 56