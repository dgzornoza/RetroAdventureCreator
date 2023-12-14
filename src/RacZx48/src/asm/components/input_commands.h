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
extern void show_input_commands(void) __z88dk_fastcall;

/**
 * Hide input commands component
 */
extern void hide_input_commands(void) __z88dk_fastcall;

/**
 * Update input commands component, should be called in main loop
 */
extern void update_input_commands(void) __z88dk_fastcall;

#endif