#include <arch/spectrum.h>
#include <stdlib.h>
#include <arch/zx.h>
#include "main.h"
#include "libs/graphics.h"
#include "libs/screen.h"

int main()
{
    zx_open_screen_chain(2);
    test();

    // char *c = "prueba de cadena super larga larga sdtflaksj dfrals fjassld faswdflaks fdasdf\0";
    //  FontAttributes = 56;
    //  // FontStyle = bold;
    //  FontCoordinates.X = 10;
    //  FontCoordinates.Y = 21;
    // zx_print_string(c);

    // FontStyle = bold;
    // printString8x8(c);
    // FontStyle = underscore;
    // printString8x8(c);
    // FontStyle = italic;
    // printString8x8(c);
    return 0;
}
