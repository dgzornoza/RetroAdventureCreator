#include <arch/spectrum.h>
#include <stdlib.h>
#include <arch/zx.h>
#include <input.h>
#include "main.h"
#include "asm/output.h"
#include "asm/input.h"
#include "asm/screen.h"

int main()
{
    // while (1)
    // {
    //     zx_border(INK_WHITE);

    //     in_wait_key();

    //     zx_border(INK_BLUE);

    //     in_wait_nokey();
    // }

    openScreenChain(1);

    char buffer[25];

    // cadena1 DB "Introduce un texto:", FONT_CRLF, FONT_CRLF
    //         DB FONT_SET_INK, 2, FONT_SET_STYLE, FONT_BOLD
    //         DB "> ", FONT_EOS
    char cadena1[] = {
        'I',
        'n',
        't',
        'r',
        'o',
        'd',
        'u',
        'c',
        'e',
        ' ',
        'u',
        'n',
        ' ',
        't',
        'e',
        'x',
        't',
        'o',
        ':',
        (char)CRLF,
        (char)CRLF,
        (char)SET_INK,
        (char)2,
        (char)SET_STYLE,
        (char)BOLD,
        '>',
        (char)EOS,
    };

    // cadena2 DB FONT_CRLF, FONT_CRLF, FONT_SET_STYLE, FONT_NORMAL
    //         DB FONT_SET_INK, 0, "Tu cadena es: ", FONT_CRLF, FONT_CRLF
    //         DB FONT_SET_INK, 2, FONT_SET_STYLE, FONT_BOLD, FONT_EOS
    // uint8_t cadena2[] = {
    //     CRLF,
    //     CRLF,
    //     SET_STYLE,
    //     NORMAL,
    //     SET_INK,
    //     0,
    //     "Tu cadena es: ",
    //     CRLF,
    //     CRLF,
    //     SET_INK,
    //     2,
    //     SET_STYLE,
    //     BOLD,
    //     EOS,
    // };

    zxPrintString(cadena1);

    char *string = zxInputstring(buffer, 20);
    zxPrintString(string);

    // zxPrintString(cadena2);

    return 0;
}
