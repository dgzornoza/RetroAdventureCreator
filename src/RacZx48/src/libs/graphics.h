#ifndef __GRAPHICS_H__
#define __GRAPHICS_H__

#include <stdlib.h>

/** DEFINITIONS */
// -----------------------------------------------------------------------------
/** ROM Default font charset */
#define DefaultFontCharset 0x3C00

/** INFRASTRUCTURE */
// -----------------------------------------------------------------------------
struct Coordinates
{
    uint8_t X;
    uint8_t Y;
};

/** Enum with font control codes */
enum FontControlCodeEnum
{
    /* End of String */
    eos = 0,
    set_style = 1,
    set_x = 2,
    set_y = 3,
    set_ink = 4,
    set_paper = 5,
    set_attrib = 6,
    set_bright = 7,
    set_flash = 8,
    /* currently unused */
    unused = 9,
    lf = 10,
    crlf = 11,
    blank = 12,
    cr = 13,
    backspace = 14,
    tab = 15,
    inc_x = 16,
    /** From 17 to 31 free */
};

/** Enum with font styles */
enum FontStyleEnum
{
    normal = 0,
    bold = 1,
    underscore = 2,
    italic = 3,
};

/** VARIABLES */
// -----------------------------------------------------------------------------

/** Font Coordinates for printChar8x8 in graphics.asm */
extern struct Coordinates FontCoordinates;
/** Font Charset for printChar8x8 in graphics.asm (can be use default with '(uint8_t *)DefaultFontCharset;') */
extern uint8_t *FontCharset;
/** Font Attributes for printChar8x8 in graphics.asm */
extern uint8_t FontAttributes;
/** Font Style for printChar8x8 in graphics.asm */
extern enum FontStyleEnum FontStyle;

/** FUNCTIONS */
// -----------------------------------------------------------------------------

/**
 * @param character print character from charset
 */
extern void printChar8x8(char character) __z88dk_fastcall;

#endif