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
    FontAttributes = 56;
    FontStyle = bold;
    FontCoordinates.X = 0;
    FontCoordinates.Y = 15;

    // printChar8x8('X');

    char *c = "prueba de cadena super larga larga sdñflaksj dfñals fjasñld fasñdflaks fdasdf\0";
    while (*c)
    {
        printChar8x8(*c++);
        if (++FontCoordinates.X > 31)
        {
            if (FontCoordinates.Y != 23)
            {
                FontCoordinates.Y++;
            }
            FontCoordinates.X = 0;
        }
    }

    return 0;
}
