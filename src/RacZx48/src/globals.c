#include "globals.h"
#include <stdlib.h>

/** ROM Default font charset */
extern const uint16_t DefaultFontCharset = 0x3D00;

/** Used by printChar8x8 in graphics.asm */
struct Coordinates FontCoordinates;
uint8_t *FontCharset;
uint8_t FontAttributes;