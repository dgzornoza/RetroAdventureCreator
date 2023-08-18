#include <arch/spectrum.h>
#include <stdlib.h>
#include <arch/zx.h>
#include "main.h"
#include "libs/graphics.h"

int main()
{
    char *c = "prueba de cadena super larga larga sdtflaksj dfrals fjassld faswdflaks fdasdf\0";
    // FontAttributes = 56;
    // // FontStyle = bold;
    // FontCoordinates.X = 10;
    // FontCoordinates.Y = 21;
    printString(c);

    // FontStyle = bold;
    // printString8x8(c);
    // FontStyle = underscore;
    // printString8x8(c);
    // FontStyle = italic;
    // printString8x8(c);
    return 0;
}
