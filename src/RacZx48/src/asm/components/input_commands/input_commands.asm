SECTION code_user

public _show_input_commands         ; export C decl "extern void enable_input_commands(void) __z88dk_fastcall;"
public _hide_input_commands         ; export C decl "extern void disable_input_commands(void) __z88dk_fastcall;"
PUBLIC _update_input_commands       ; export C decl "extern void update_input_commands(void) __z88dk_fastcall;"

; z88dk/libsrc/_DEVELOPMENT/arch/zx/misc/z80/asm_zx_cls_wc.asm
; params :  l = attr
;           iy = rect* (x=iy-4, y=iy-2, width=iy-3, height=iy-1)
EXTERN asm_zx_cls_wc

EXTERN asm_show_shell

;-------------------------------------------------------------------------------
;  Name:		public _show_input_commands
;  Description:	routine for show input commands component
;  Input:		--
;  Output: 	   --
;-------------------------------------------------------------------------------
_show_input_commands:

   push bc         
   push de
   
   ld a, (InputCommandState)
   cp 0x01                                ; activate only if not is active.
   jr z, exit_show

   call asm_clear_input_commands_region   ; clear region
   call asm_show_shell                    ; show shell

   ld a, 0x01                             ; set as activated
   ld (InputCommandState), a

.exit_show
   pop de         
   pop bc     
   ret

;-------------------------------------------------------------------------------
;  Name:		public _hide_input_commands
;  Description:	routine for hide input commands component
;  Input:		--
;  Output: 	   --
;-------------------------------------------------------------------------------
_hide_input_commands:

   push bc         
   push de
   
   ld a, (InputCommandState)
   cp 0x00                       ; activate only if not is active.
   jr z, exit_hide
   call asm_show_shell

   ld a, 0x00                    ; set as activated
   ld (InputCommandState), a

.exit_hide
   pop de         
   pop bc     
   ret

;-------------------------------------------------------------------------------
;  Name:		public _update_input_commands
;  Description:	routine for update input commands component
;  Input:		--
;  Output: 	   --
;-------------------------------------------------------------------------------
_update_input_commands:


   ret


;-------------------------------------------------------------------------------
;  Name:		private _clear_input_commands_region
;  Description:	routine for clear input commands region
;  Input:		--
;  Output: 	   --
;-------------------------------------------------------------------------------
asm_clear_input_commands_region:

   push iy              ; preserve ix register

   ld iy, 0             ; set ix = stack pointer
   add iy, sp   
   push af              ; reserve 4 bytes for Rect data and create rect
   push af
   ld (iy-4), 0x05      ; Rect X
   ld	(iy-2), 0x05      ; Rect Y
   ld	(iy-3), 0x0a      ; Rect Width
   ld	(iy-1), 0x0a      ; Rect Height
 
   ld l, 22             ; set attributes
   call asm_zx_cls_wc   ; call z88dk function for fill rect

   pop af
   ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

.InputCommandState   db 0;