SECTION code_user

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

PUBLIC _GLOBAL_TIMER_TICKS
    _GLOBAL_TIMER_TICKS:        db 0    ; timer ticks

PUBLIC _GLOBAL_TIMER_ABS_TICKS
    _GLOBAL_TIMER_ABS_TICKS:    dw 0    ; absolute timer ticks

PUBLIC _GLOBAL_TIMER_PAUSE
    _GLOBAL_TIMER_PAUSE:        db 0    ; pause flag