SECTION code_user

PUBLIC _get_key      ; export C decl "extern unsigned int get_key(void) __z88dk_fastcall;"

EXTERN _in_inkey


_in_KeyDebounce:	   db	1
_in_KeyStartRepeat:	db	250
_in_KeyRepeatPeriod:	db	250
_in_KbdState:		   db	1
			            db	0

;-------------------------------------------------------------------------------
;	Name:		    private get_key
;	Description:    Scans the keyboard and returns an ascii code representing a single keypress.  
;       Operates as a state machine.  First it debounces a key by ignoring it until a minimum time
;       "_in_KeyDebounce" (byte) has passed.  The key will be registered and then it will wait until 
;       the key has been pressed for a period "_in_KeyStartRepeat" (byte).  
;       The key will again be registered and then repeated thereafter with period "_in_KeyRepeatPeriod" (byte).
;       If more than one key is pressed, no key is registered and the state machine returns to the debounce state.  
;       Time intervals depend on how often GetKey is called.
;	Input:		   --
;	Output: 	      carry = no key registered (and HL=0), else HL = ascii code of key pressed
;  Clobbers:      AF,BC,DE,HL
;-------------------------------------------------------------------------------
_get_key:

   call _in_inkey              ; hl = ascii code & carry if no key
   jp c, key_reset

   ld a, (_in_KbdState)
   dec a   
   jr nz, nokey

   ld a, (_in_KbdState + 1)
   dec a
   jp m, debounce
   jp z, startrepeat

.repeat
   ld a, (_in_KeyRepeatPeriod)
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


