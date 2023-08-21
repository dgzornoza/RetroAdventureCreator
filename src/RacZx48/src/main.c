#pragma output REGISTER_SP = 0xFF58
#pragma output CLIB_MALLOC_HEAP_SIZE = -0xFBFA

#include <z80.h>
#include <string.h>
#include <intrinsic.h>
#include <im2.h>
#include <arch/zx.h>
#include "asm/input.h"
#include "asm/output.h"
#include <input.h>

int numtimers = 10;
int timer[10];

int screen = 0x4000;
char keysBuffer[10];

char cadena1[] = {
    (char)SET_Y,
    0,
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
    (char)SET_INK,
    (char)2,
    (char)SET_STYLE,
    (char)BOLD,
    '>',
    (char)CR,
    (char)EOS,
};

int *sp;
#define push(sp, n) (*((sp)++) = (n))
#define pop(sp) (*--(sp))

IM2_DEFINE_ISR(isr)
{
    char c;

    if ((c = in_inkey()) != 0)
        push(sp, 10);

    // char byte = zxTestInput();
    // byte = byte == NULL ? 0x55 : byte;
    // zxPrintString(cadena1);

    // char cadena2[] = {
    //     (char)SET_Y,
    //     1,
    //     c,
    //     (char)CR,
    //     (char)EOS,
    // };
    // zxPrintString(cadena2);

    // *(unsigned char *)screen = byte ? 0x55 : byte;
    // screen += 8;
    // int i;

    // for (i = 0; i != numtimers; i++)
    //     if (timer[i])
    //         timer[i]--;
}

#define TABLE_HIGH_BYTE ((unsigned int)0xfc)
#define JUMP_POINT_HIGH_BYTE ((unsigned int)0xfb)

#define UI_256 ((unsigned int)256)

#define TABLE_ADDR ((void *)(TABLE_HIGH_BYTE * UI_256))
#define JUMP_POINT ((unsigned char *)((unsigned int)(JUMP_POINT_HIGH_BYTE * UI_256) + JUMP_POINT_HIGH_BYTE))

int main()
{
    memset(TABLE_ADDR, JUMP_POINT_HIGH_BYTE, 257);

    z80_bpoke(JUMP_POINT, 195);
    z80_wpoke(JUMP_POINT + 1, (unsigned int)isr);

    im2_init(TABLE_ADDR);

    intrinsic_ei();

    sp = keysBuffer;

    while (1)
        ;
}