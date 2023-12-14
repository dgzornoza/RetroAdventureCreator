SECTION code_user

PUBLIC _get_key      ; export C decl "extern unsigned int get_key(void) __z88dk_fastcall;"

EXTERN _in_inkey
EXTERN _GLOBAL_TIMER_TICKS

;-------------------------------------------------------------------------------
;	Name:		    internal get_key
;	Description:    Scans the keyboard and returns an ascii code representing a single keypress.  
;        Operates as a state machine.  
;        First it get a key. 
;        The key will be registered and then it will wait until the key has been pressed for a period "_in_KeyStartRepeat" (byte).  
;        The key will again be registered and then repeated thereafter with period "_in_KeyRepeatPeriod" (byte).
;        If more than one key is pressed, no key is registered and the state machine returns to default state.  
;        If other key is pressed, return new key and state machine returns to default state.
;        Time intervals is sync with timer from _GLOBAL_TIMER_TICKS
;  Remarks: diagram is in file 'asm_get_key.drawio.svg'
;	Input:		   --
;	Output: 	      carry = no key registered (and HL=0), else HL = ascii code of key pressed
;  Clobbers:      --
;-------------------------------------------------------------------------------
_get_key:

   push af     ; preserve registers
   push de

   call _in_inkey             ; hl = ascii code, 0x00 if no key, carry if multiple key pressed
   jr c, reset_state          ; if multiple key pressed, reset state and exit (hl = 0)

   ld a, l                    ; if not exist key, reset state and exit (hl = 0)
   or a   
   jr z, reset_state                

   ld a, (KeyboardState)      ; if last key is diferent, jump new key.
   cp l
   jr nz, new_key    
   
.try_repeat_key       
   ld a, (LastTimeTick)          ; if last time tick is equal, jump no key
   ld d, a
   ld a, (_GLOBAL_TIMER_TICKS) 
   cp d
   jr z, nokey  

   ld (LastTimeTick), a          ; store last time tick
   ld a, (KeyboardState + 1)     ; decrement counter, if not zero, return no key
   dec a
   ld (KeyboardState + 1), a
   jr nz, nokey

   ld a, KEY_REPEAT_PERIOD       ; reset counter to repeat key and return key
   ld (KeyboardState + 1), a
   jr exit

.new_key
   ld d, KEY_START_REPEAT        ; reset keyboard state with last key
   ld e, l
   ld (KeyboardState), de  
   jr exit

.reset_state 
   ld d, KEY_START_REPEAT        ; reset keyboard state
   ld e, 0
   ld (KeyboardState), de      

.nokey
   ; return no key
   xor a
   ld l, a
   scf
   jr exit

.exit         
   pop de      ; recovery registers
   pop af
   ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; CONSTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;                  
KEY_START_REPEAT     equ 40
KEY_REPEAT_PERIOD    equ 10

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

.LastTimeTick:    db 0

; keyboard state: 
; first byte => last key
; second byte => counter time
.KeyboardState:   db	0
			         db	KEY_START_REPEAT