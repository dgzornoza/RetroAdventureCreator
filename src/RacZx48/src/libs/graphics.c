#include "graphics.h"
#include <stdlib.h>

/** ROM Default font charset (first char 'space' is in +32 bytes = 3D00)*/
// const uint16_t DefaultFontCharset = 0x3C00;

/** Used by printChar8x8 in graphics.asm */
struct Coordinates FontCoordinates;
uint8_t *FontCharset = DefaultFontCharset;
uint8_t FontAttributes;
enum FontStyleEnum FontStyle;