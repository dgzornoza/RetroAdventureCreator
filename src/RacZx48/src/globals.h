#ifndef __GLOBALS_H__
#define __GLOBALS_H__

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

#endif