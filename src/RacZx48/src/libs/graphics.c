#include "graphics.h"
#include <stdlib.h>

/** VARIABLES */
// -----------------------------------------------------------------------------

/** Font Coordinates for printChar8x8 in graphics.asm */
struct Coordinates FontCoordinates;
/** Font Charset for printChar8x8 in graphics.asm (can be use default with '(uint8_t *)DefaultFontCharset;') */
uint8_t *FontCharset = DefaultFontCharset;
/** Font Attributes for printChar8x8 in graphics.asm */
uint8_t FontAttributes;
/** Font Style for printChar8x8 in graphics.asm */
enum FontStyleEnum FontStyle;

/** FUNCTIONS */
// -----------------------------------------------------------------------------

inline void setInk(uint8_t value)
{
    FontAttributes = value & 0b00000111;
}

inline void setPaper(uint8_t value)
{
    FontAttributes = value & 0b00111000;
}

// void printString8x8(char *string)
// {
//     // Establecimiento de valores iniciales
//     // FontCharset = charset1 - 256;
//     FontCharset = (uint8_t *)DefaultFontCharset;
//     FontAttributes = 56;
//     FontStyle = bold;
//     FontCoordinates.X = 0;
//     FontCoordinates.Y = 15;

//     while (*string)
//     {
//         printChar8x8(*string++);
//         FontCoordinates.X++;
//         if (FontCoordinates.X == 32 && FontCoordinates.Y < 24)
//         {
//             FontCoordinates.Y++;
//             FontCoordinates.X = 0;
//         }
//     }
// }
