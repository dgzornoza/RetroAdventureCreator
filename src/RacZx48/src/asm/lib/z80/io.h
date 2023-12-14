#ifndef __DGZ_IO_H__
#define __DGZ_IO_H__

#include <stdlib.h>

/** INFRASTRUCTURE */
// -----------------------------------------------------------------------------

/** Enum with font control codes, should be equal to control_chars -> 'FontControlCodeRoutines' */
enum FontControlCodeEnum
{
    /* End of String */
    EOS = 0,
    SET_STYLE = 1,
    SET_X = 2,
    SET_Y = 3,
    SET_INK = 4,
    SET_PAPER = 5,
    SET_ATTRIB = 6,
    SET_BRIGHT = 7,
    SET_FLASH = 8,
    /* currently unused */
    UNUSED = 9,
    LF = 10,
    CRLF = 11,
    BLANK = 12,
    CR = 13,
    BACKSPACE = 14,
    TAB = 15,
    INC_X = 16,
    /** From 17 to 31 free */
};

/** Enum with font styles */
enum FontStyleEnum
{
    NORMAL = 0,
    BOLD = 1,
    UNDERSCORE = 2,
    ITALIC = 3,
};

/** Last key presed stored in rom */
extern char ROM_LAST_KEY;

/** FUNCTIONS */
// -----------------------------------------------------------------------------

/**
 * insert key into input queue buffer (FIFO)
 */
extern void push_buffer_key(char key) __z88dk_fastcall;
/**
 * print string from buffer_keys.
 */
extern void print_buffer_keys(void);

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