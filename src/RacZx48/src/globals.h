#ifndef __GLOBALS_H__
#define __GLOBALS_H__

#include <stdlib.h>

struct Coordinates
{
    uint8_t X;
    uint8_t Y;
};

/** Used by printChar8x8 in graphics.asm */
extern struct Coordinates FontCoordinates;
extern uint8_t *FontCharset;
extern uint8_t FontAttributes;

#endif