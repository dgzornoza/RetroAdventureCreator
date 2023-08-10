#ifndef __GRAPHICS_H__
#define __GRAPHICS_H__

#include <stdlib.h>

struct Coordinates
{
    uint8_t X;
    uint8_t Y;
};

/** ROM Default font charset */
extern const uint16_t DefaultFontCharset;

/***********************************************************/
/** Used in graphics.asm

/** Font Coordinates for printChar8x8 in graphics.asm */
extern struct Coordinates FontCoordinates;
/** Font Charset for printChar8x8 in graphics.asm (can be use default with '(uint8_t *)DefaultFontCharset;') */
extern uint8_t *FontCharset;
/** Font Attributes for printChar8x8 in graphics.asm */
extern uint8_t FontAttributes;
/** Font Style for printChar8x8 in graphics.asm */
extern uint8_t FontStyle;

/**
 * @param character print character from charset
 */
extern void printChar8x8(char character) __z88dk_fastcall;

// ;--- Variables de fuente --------------------
// FONT_CHARSET     DW    $3D00-256
// FONT_ATTRIB      DB    56       ; Negro sobre gris
// FONT_STYLE       DB    0
// FONT_X           DB    0
// FONT_Y           DB    0

// ;--- Constantes predefinidas ----------------
// FONT_NORMAL      EQU   0
// FONT_BOLD        EQU   1
// FONT_UNDERSC     EQU   2
// FONT_ITALIC      EQU   3

#endif