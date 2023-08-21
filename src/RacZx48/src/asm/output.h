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

/**
 * Print string with format in screen
 * @param string string to print
 */
extern void zxPrintString(char *string) __z88dk_fastcall;

/**
 * Set font charset to use in printing strings
 * @param charset pointer to charset
 */
extern void zxSetFontCharset(char *charset) __z88dk_fastcall;

/**
 * Set default ROM charset to use in printing strings
 */
extern void zxSetDefaultFontCharset() __z88dk_fastcall;

#endif