#include <arch/spectrum.h>
#include <stdlib.h>
#include "libs/screen.h"
#include "libs/graphics.h"
#include "globals.h"
#include "assets/charset1.c"

int main()
{
    // Establecimiento de valores iniciales
    FontCharset = charset1 - 256;
    // FontX = 5;
    FontAttributes = 8;
    // FontY = 15;
    FontCoordinates.X = 0;
    FontCoordinates.Y = 15;

    printChar8x8('A');

    return 0;
}
