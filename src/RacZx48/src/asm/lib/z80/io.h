#ifndef __DGZ_IO_H__
#define __DGZ_IO_H__

#include <stdlib.h>

/** INFRASTRUCTURE */
// -----------------------------------------------------------------------------

/** Font control codes, should be equal to control_chars -> 'FontControlCodeRoutines'
 * See control_chars.asm
 * use example in print string function: print_string(FONT_CONTROL_CRLF "Hello" FONT_CONTROL_CRLF);
 */
#define FONT_CONTROL_EOS "\x00"
#define FONT_CONTROL_SET_STYLE "\x01"
#define FONT_CONTROL_SET_X "\x02"
#define FONT_CONTROL_SET_Y "\x03"
#define FONT_CONTROL_SET_INK "\x04"
#define FONT_CONTROL_SET_PAPER "\x05"
#define FONT_CONTROL_SET_ATTRIB "\x06"
#define FONT_CONTROL_SET_BRIGHT "\x07"
#define FONT_CONTROL_SET_FLASH "\x08"
#define FONT_CONTROL_UNUSED "\x09"
#define FONT_CONTROL_LF "\x0A"
#define FONT_CONTROL_CRLF "\x0B"
#define FONT_CONTROL_BLANK "\x0C"
#define FONT_CONTROL_CR "\x0D"
#define FONT_CONTROL_BACKSPACE "\x0E"
#define FONT_CONTROL_TAB "\x0F"
#define FONT_CONTROL_INC_X "\x10"
/** From 17 to 31 free */

/** Enum with font styles */
enum FontStyleEnum
{
    NORMAL = 0,
    BOLD = 1,
    UNDERSCORE = 2,
    ITALIC = 3,
};

/** ROM Last key memory,
 * @remarks this app uses custom scan keyboard functions to optimize keyboard typing,
 * i'm test ROM routines and are slow in comparison.
 * Main loop should update this variable with last key pressed.
 **/
extern char ROM_LAST_KEY;

/** FUNCTIONS */
// -----------------------------------------------------------------------------

/**
 * Print string with format in screen
 * @param string string to print
 */
extern void print_string(char *string) __z88dk_fastcall;

/**
 * Scans the keyboard and returns an ascii code representing a single keypress.
 * Operates as a state machine. First it get a key.
 * The key will be registered and then it will wait until the key has been pressed for a period "_in_KeyStartRepeat" (byte).
 * The key will again be registered and then repeated thereafter with period "_in_KeyRepeatPeriod" (byte).
 * If more than one key is pressed, no key is registered and the state machine returns to default state.
 * If other key is pressed, return new key and state machine returns to default state.
 * Time intervals is sync with timer from _GLOBAL_TIMER_TICKS
 * @returns ascii code of key pressed, otherwise 0 and carry flag = false if not key pressed or multiple key pressed
 */
extern unsigned int get_key(void) __z88dk_fastcall;

#endif
