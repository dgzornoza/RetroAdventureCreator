#include <arch/spectrum.h>
#include <stdlib.h>
#include <arch/zx.h>
#include "main.h"

int main()
{
    // Establecimiento de valores iniciales
    // FontCharset = charset1 - 256;
    FontCharset = (uint8_t *)DefaultFontCharset;
    FontAttributes = 13;
    FontCoordinates.X = 0;
    FontCoordinates.Y = 15;

    printChar8x8('X');

    return 0;
}
