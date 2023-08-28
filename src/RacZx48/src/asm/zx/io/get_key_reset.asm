SECTION code_user

PUBLIC _get_key_reset      ; export C decl "extern void get_key_reset(void) __z88dk_fastcall;"
EXTERN _in_KeyDebounce, _in_KbdState

_get_key_reset:
   ld a, (_in_KeyDebounce)
   ld e, a
   ld d, 0
   ld (_in_KbdState), de
   ret