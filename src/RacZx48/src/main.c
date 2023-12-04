#include <z80.h>
#include <string.h>
#include <intrinsic.h>
#include <im2.h>
#include <arch/zx.h>
#include <input.h>

#include "asm/zx/io.h"
#include "asm/zx/timer.h"

unsigned char clock_changed = 0;
unsigned char ticks = 0;
unsigned char seconds = 0;
unsigned char minutes = 0;
unsigned char pause = 0;
unsigned int abs_ticks = 0;
unsigned int timer = 0;

IM2_DEFINE_ISR(isr)
{
    update_timer();

    // if (0 == GLOBAL_TIMER_TICKS)
    // {
    //     push_buffer_key('.');
    // }

    // int key = get_key();

    // store input keys in keyboard queue buffer
    // (ensure don't repeat key without leave)
    // key = in_inkey();
    // if (key)
    // {
    //     push_buffer_key(key);
    // }

    // if (!key)
    // {
    //     ROM_LAST_KEY = ROM_LAST_KEY == '_' ? ' ' : '_';
    // }
    // else if (ROM_LAST_KEY != key)
    // {
    //     ROM_LAST_KEY = key;
    //     push_buffer_key(key);
    // }
    print_buffer_keys();
}

#define TABLE_HIGH_BYTE ((unsigned int)0xfc)
#define JUMP_POINT_HIGH_BYTE ((unsigned int)0xfb)

#define UI_256 ((unsigned int)256)

#define TABLE_ADDR ((void *)(TABLE_HIGH_BYTE * UI_256))
#define JUMP_POINT ((unsigned char *)((unsigned int)(JUMP_POINT_HIGH_BYTE * UI_256) + JUMP_POINT_HIGH_BYTE))

int main(void)
{
    // START Instalation routine ISR
    memset(TABLE_ADDR, JUMP_POINT_HIGH_BYTE, 257);

    z80_bpoke(JUMP_POINT, 195);
    z80_wpoke(JUMP_POINT + 1, (unsigned int)isr);

    im2_init(TABLE_ADDR);

    intrinsic_ei();
    // END Instalation routine ISR

    // main app loop
    while (1)
    {
        int key = get_key();
        if (key)
        {
            push_buffer_key(key);
        }

        // print_buffer_keys();
    }
}