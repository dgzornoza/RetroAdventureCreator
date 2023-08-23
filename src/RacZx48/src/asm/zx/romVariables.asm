SECTION code_user

PUBLIC _ROM_LAST_KEY    ; address for get last key pressed
    _ROM_LAST_KEY       equ $5C08
PUBLIC _ROM_KEY_SCAN    ; Routine address for scan keyboard keys
    _ROM_KEY_SCAN       equ $028E   
PUBLIC _ROM_CHARSET     ; ROM address with default charset
    _ROM_CHARSET:       equ $3C00
public _ROM_OPEN_CHAIN: ; Routine address for open chain screen (1 = command line screen, 2 = upper screen)
    _ROM_OPEN_CHAIN:     equ $1601

