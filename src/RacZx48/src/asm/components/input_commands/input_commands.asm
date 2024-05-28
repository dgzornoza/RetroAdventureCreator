SECTION code_user

public _is_visible_input_commands   ; ex1port C decl "extern bool is_visible_input_commands(void) __z88dk_fastcall;"
public _show_input_commands         ; ex1port C decl "extern void enable_input_commands(void) __z88dk_fastcall;"
public _hide_input_commands         ; export C decl "extern void disable_input_commands(void) __z88dk_fastcall;"
PUBLIC _input_commands_update       ; export C decl "extern void input_commands_update(void) __z88dk_fastcall;"
PUBLIC _input_commands_render       ; export C decl "extern void input_commands_render(void) __z88dk_fastcall;"


EXTERN asm_zx_cls_wc
EXTERN asm_show_prompt
EXTERN asm_show_cursor
EXTERN asm_font_inc_x
EXTERN asm_push_buffer_key
EXTERN asm_print_buffer_keys

;;; Constants
EXTERN _ROM_LAST_KEY
EXTERN _SYS_LOWRES_SCR_HEIGHT;
EXTERN _SYS_LOWRES_SCR_WIDTH;
EXTERN _ROM_CHARSET
EXTERN _DEFAULT_FONT_ATTRIBUTES

;;; Font vars
EXTERN _GLOBAL_FONT_CHARSET
EXTERN _GLOBAL_FONT_X         
EXTERN _GLOBAL_FONT_ATTRIBUTES
EXTERN _GLOBAL_FONT_STYLE    

;-------------------------------------------------------------------------------
;  Name:		public _is_visible_input_commands
;  Description:	routine for get input commands component visibility
;  Input:		--
;  Output: 	   L = 0x00 => not visible, 0x01 => visible
;-------------------------------------------------------------------------------
_is_visible_input_commands:
   ld hl, (State)   
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
   
   ld a, (State)
   cp 0x01                                ; show only if not is visible.
   jr z, exit_show

   call asm_clear_input_commands_region         ; clear region

   call asm_set_input_command_font_properties   ; set font properties

   call asm_show_prompt                         ; show shell prompt

   call asm_font_inc_x                          ; set cursor position 
   call asm_show_cursor                         ; show cursor

   ld a, 0x01                             ; set as visible
   ld (State), a

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
   
   ld a, (State)
   cp 0x00                                ; hide only if is visible.
   jr z, exit_hide
   call asm_clear_input_commands_region   ; clear region

   ld a, 0x00                             ; set as not visible
   ld (State), a

.exit_hide
   pop de         
   pop bc     
   ret

;-------------------------------------------------------------------------------
;  Name:		public _input_commands_update
;  Description: routine for manage input commands component update events, should be called in main update loop
;  Input:		--
;  Output: 	   --
;-------------------------------------------------------------------------------
_input_commands_update:

   ld a, (State)
   cp 0x00                                
   jr z, exit_update             ; execute only if is visible.

   ; ld a, (CursorIndex)
   ; cp INPUT_MAX_LENGTH           ; compare with max length
   ; jr nc, exit_update            ; if index > max length, exit routine
   ; or a
   ; jr z, exit_update             ; if index == 0, exit routine

   ld a, (_ROM_LAST_KEY)
   call asm_push_buffer_key      ; push last key to input buffer

.exit_update
   ret


;-------------------------------------------------------------------------------
;  Name:		public _input_commands_render
;  Description: routine for manage input commands component render events, should be called in main render loop
;  Input:		--
;  Output: 	   --
;-------------------------------------------------------------------------------
_input_commands_render:

   ld a, (State)
   cp 0x00                                
   jr z, exit_render             ; execute only if is visible.

   call asm_print_buffer_keys    ; print buffer keys

.exit_render
   ret
   
;-------------------------------------------------------------------------------
;  Name:		private _clear_input_commands_region
;  Description:	routine for clear input commands region
;  Input:		--
;  Output: 	   --
;-------------------------------------------------------------------------------
asm_clear_input_commands_region:

   push iy              ; preserve iy register

   ld iy, Area                ; copy Rect data to stack
   ld hl, (AreaAttributes)    ; set attributes

   call asm_zx_cls_wc   ; call z88dk function for fill rect
   
   pop iy               ; restore iy register
   ret

;-------------------------------------------------------------------------------
;  Name:		private asm_set_input_command_font_properties
;  Description:	routine for set character font properties for input commands
;  Input:		--
;  Output: 	   --
;-------------------------------------------------------------------------------
asm_set_input_command_font_properties:

   ld hl, _ROM_CHARSET                
   ld (_GLOBAL_FONT_CHARSET), hl

   ld a, _DEFAULT_FONT_ATTRIBUTES                    
   ld (_GLOBAL_FONT_ATTRIBUTES), a

   ld a, 0
   ld (_GLOBAL_FONT_STYLE), a

   ld c, 0
   ld b, _SYS_LOWRES_SCR_HEIGHT - 1
   ld (_GLOBAL_FONT_X), bc

   ret

;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
; VARIABLES 
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;

;.INPUT_MAX_LENGTH    equ _SYS_LOWRES_SCR_WIDTH - 2     ; all width except prompt and cursor
;.CursorIndex         db 0

.State   db 0

.Area:   db	0, 32, 23, 1      ; Input command outer area: X, Width, Y, Height
.AreaAttributes:  db 0x78     ; Attributes (8 flash, 7 bright, 6-4 paper, 3-1 ink)