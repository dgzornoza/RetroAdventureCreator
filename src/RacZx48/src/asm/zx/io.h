#ifndef __DGZ_OUTPUT_H__
#define __DGZ_OUTPUT_H__

#include <stdlib.h>

/** DEFINITIONS */
// -----------------------------------------------------------------------------
/** ROM Default font charset */
#define DefaultFontCharset 0x3C00

/** INFRASTRUCTURE */
// -----------------------------------------------------------------------------

/** Enum with font control codes */
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

/** FUNCTIONS */
// -----------------------------------------------------------------------------


extern void push_buffer_key(char key) __z88dk_fastcall;

/**
 * @param chain chain to set: 1 = command line, 2 = upper screen
 */
extern void open_screen_chain(char chain) __z88dk_fastcall;

extern void print_char(char *ascii) __z88dk_fastcall;

/**
 * Print string with format in screen
 * @param string string to print
 */
extern void print_string(char *string) __z88dk_fastcall;

extern char* read_string(uint8_t *buffer, uint8_t length);

#endif