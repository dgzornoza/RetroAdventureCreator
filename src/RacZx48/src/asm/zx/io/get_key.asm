SECTION code_user

PUBLIC _get_key      ; export C decl "extern unsigned int get_key(void) __z88dk_fastcall;"

EXTERN _in_inkey


_in_KeyDebounce:	   db	1
_in_KeyStartRepeat:	db	250
_in_KeyRepeatPeriod:	db	250
_in_KbdState:		   db	1
			            db	0
lastkey
;-------------------------------------------------------------------------------
;	Name:		    private get_key
;	Description:    Scans the keyboard and returns an ascii code representing a single keypress.  
;        Operates as a state machine.  
;        First it debounces a key by ignoring it until a minimum time "_in_KeyDebounce" (byte) has passed. 
;        The key will be registered and then it will wait until the key has been pressed for a period "_in_KeyStartRepeat" (byte).  
;        The key will again be registered and then repeated thereafter with period "_in_KeyRepeatPeriod" (byte).
;        If more than one key is pressed, no key is registered and the state machine returns to the debounce state.  
;        Time intervals depend on how often GetKey is called.
;	Input:		   --
;	Output: 	      carry = no key registered (and HL=0), else HL = ascii code of key pressed
;  Clobbers:      AF,BC,DE,HL
;-------------------------------------------------------------------------------
_get_key:

   call _in_inkey                ; hl = ascii code, 0x00 if no key, carry if multiple key pressed
   jp c, key_reset               ; if multiple key pressed, reset key and exit

   ld a, (_in_KbdState)          ; decrement first byte kdbstate, if not zero, jump to nokey and return
   dec a   
   jr nz, nokey

   ld a, (_in_KbdState + 1)      ; decrement second byte kdbstate, 
   dec a
   jp m, debounce                ; if sign flag jump debounce
   jp z, startrepeat             ; if zero flag jump startrepeat

.repeat                    
   ld a, (_in_KeyRepeatPeriod)   ; set _in_KeyRepeatPeriod in kdbstate an return
   ld (_in_KbdState), a
   ret

.debounce
   ;;; set KdbState to _in_KeyStartRepeat value for decrement
   ld a, (_in_KeyStartRepeat)
   ld e, a
   ld d, 1
   ld (_in_KbdState), de
   ret

.startrepeat
   ;;; set KdbState to _in_KeyStartRepeat value for decrement
   ld a, (_in_KeyRepeatPeriod)
   ld e, a
   ld d, 2
   ld (_in_KbdState), de
   ret

.key_reset:
   ; no key presed, reset to start state
   ld a, (_in_KeyDebounce)
   ld e, a
   ld d, 0
   ld (_in_KbdState), de
   ret   

.nokey
   ; Not return key
   ld (_in_KbdState), a
   ld hl, 0
   scf
   ret






_get_key:

   call _in_inkey                ; hl = ascii code, 0x00 if no key, carry if multiple key pressed
   jr c, reset_state_and_exit    ; if multiple key pressed, reset state and exit

   ld a, (hl)                    ; if not exist key, reset state and exit.
   or a   
   jr z, reset_state_and_exit                

   ld b, (LastKey)               ; if last key is diferent, update last key, reset state and exit.
   cp b
   jr nz, new_key    

   ld a, (KeyboardState)         ; is initial state key?, jump start_repeat
   cp STATE_INITIAL  
   jp z, startrepeat             

   
.repeat_period
   ; start repeat period state
   ld a, (KEY_REPEAT_PERIOD)
   ld e, a
   ld d, 0
   ld (KeyboardState), de
   ret

.repeat_state_control
   
   ld a, (KeyState)
   dec a,
   jr z, start_repeat      ; same key pressed, start repeat counter control 

   ;;; start repeat counter finished, start repeat period control
   ld a, (Counter)         ; decrement counter, 
   dec a
   ret

.start_repeat
   ; start repeat state
   ld a, (KEY_START_REPEAT)
   ld e, a
   ld d, STATE_START_REPEAT
   ld (KeyboardState), de
   ret

.new_key
   ; new key, update last key, reset state and exit.
   ld (LastKey), (hl)            
   
.reset_state_and_exit
   ; reset keyboard state and exit
   ld e, 1
   ld d, 0
   ld (KeyboardState), de
   ret   

.nokey
   ; return no key
   xor a
   ld hl, a
   scf
   ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; CONSTANTS 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;                  
KEY_START_REPEAT     equ 20
KEY_REPEAT_PERIOD    equ 10

STATE_INITIAL        equ 1
STATE_START_REPEAT   equ 0
STATE_REPEATE_PERIOD equ 0
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
.LastKey          db 0

; keyboard state: 
; first byte => state: 
;     1 => initial state (no key), 
;     0 => first key pressed (KEY_START_REPEAT), 
;     FF => pushed same key (KEY_REPEAT_PERIOD)
; second byte => counter time
.KeyboardState:   db	1
			         db	0