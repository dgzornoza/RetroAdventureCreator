#include <arch/spectrum.h>
#include <stdlib.h>
#include <arch/zx.h>
#include "main.h"
#include "libs/graphics.h"

inline void setInk(uint8_t value)
{
    FontAttributes = value & 0b00000111;
}

inline void setPaper(uint8_t value)
{
    FontAttributes = value & 0b00111000;
}

int main()
{
    // Establecimiento de valores iniciales
    // FontCharset = charset1 - 256;
    FontCharset = (uint8_t *)DefaultFontCharset;
    FontAttributes = 13;
    FontCoordinates.X = 0;
    FontCoordinates.Y = 15;

    setInk(4);
    setPaper(4);

    printChar8x8('X');

    return 0;
}
