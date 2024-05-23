SECTION code_user

public _is_visible_input_commands   ; ex1port C decl "extern bool is_visible_input_commands(void) __z88dk_fastcall;"
public _show_input_commands         ; ex1port C decl "extern void enable_input_commands(void) __z88dk_fastcall;"
public _hide_input_commands         ; export C decl "extern void disable_input_commands(void) __z88dk_fastcall;"
PUBLIC _update_input_commands       ; export C decl "extern void update_input_commands(void) __z88dk_fastcall;"

EXTERN asm_zx_cls_wc

EXTERN asm_show_shell

;-------------------------------------------------------------------------------
;  Name:		public _is_visible_input_commands
;  Description:	routine for get input commands component visibility
;  Input:		--
;  Output: 	   L = 0x00 => not visible, 0x01 => visible
;-------------------------------------------------------------------------------
_is_visible_input_commands:
   ld hl, (InputCommandState)   
   ret

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
   cp 0x01                                ; show only if not is visible.
   jr z, exit_show

   call asm_clear_input_commands_region   ; clear region
   call asm_show_shell                    ; show shell

   ld a, 0x01                             ; set as visible
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
; TODO: dgzornoza, no probado, cuando haga falta, se tiene que implementar correctamente guardar los atributos en el show, y aqui volver a ponerlos.
_hide_input_commands:

   push bc         
   push de
   
   ld a, (InputCommandState)
   cp 0x00                                ; hide only if is visible.
   jr z, exit_hide
   call asm_clear_input_commands_region   ; clear region

   ld a, 0x00                             ; set as not visible
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

   push iy              ; preserve iy register

   ld iy, InputCommandArea                ; copy Rect data to stack
   ld hl, (InputCommandAreaAttributes)    ; set attributes

   call asm_zx_cls_wc   ; call z88dk function for fill rect
   
   pop iy         ; restore iy register
   ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

.InputCommandState   db 0

.InputCommandArea:   db	0, 32, 23, 1      ; Input command area: X, Width, Y, Height
.InputCommandAreaAttributes:  db 0x78     ; Attributes (8 flash, 7 bright, 6-4 paper, 3-1 ink)