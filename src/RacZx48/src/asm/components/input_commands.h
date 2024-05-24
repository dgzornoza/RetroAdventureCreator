#ifndef __DGZ_INPUT_COMMANDS_H__
#define __DGZ_INPUT_COMMANDS_H__

/** DEFINITIONS */
// -----------------------------------------------------------------------------

/** INFRASTRUCTURE */
// -----------------------------------------------------------------------------

/** FUNCTIONS */
// -----------------------------------------------------------------------------

/**
 * Show input commands component
 */
extern unsigned char is_visible_input_commands(void) __z88dk_fastcall;

/**
 * Show input commands component
 */
extern void show_input_commands(void) __z88dk_fastcall;

/**
 * Hide input commands component
 */
extern void hide_input_commands(void) __z88dk_fastcall;

/**
 * Update function for input commands component, should be called in main update loop
 */
extern void input_commands_update(void) __z88dk_fastcall;

/**
 * Render function for input commands component, should be called in main render loop
 */
extern void input_commands_render(void) __z88dk_fastcall;

#endif