#include <z80.h>
#include <string.h>
#include <intrinsic.h>
#include <im2.h>
#include <arch/zx.h>
#include <input.h>

#include "asm/lib/z80/io.h"
#include "asm/components/timer.h"
#include "asm/components/input_commands.h"

// unsigned char clock_changed = 0;
// unsigned char ticks = 0;
// unsigned char seconds = 0;
// unsigned char minutes = 0;
// unsigned char pause = 0;
// unsigned int abs_ticks = 0;
// unsigned int timer = 0;

/**
 * IM2 function, updated every 50 ms with vsync
 */
IM2_DEFINE_ISR(isr)
{
    update_timer();

    if (is_visible_input_commands())
    {
        print_buffer_keys();
    }
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

    // struct r_Rect8 s1;
    // s1.x = 5;
    // s1.width = 10;
    // s1.y = 5;
    // s1.height = 10;
    // unsigned char attr = 22;
    // zx_cls_wc(&s1, attr);

    // main app loop
    while (1)
    {
        // zx_cls_wc(&s1, attr);

        // show_input_commands();

        // read keyboard
        int key = get_key();
        if (key && is_visible_input_commands())
        {
            push_buffer_key(key);
        }
    }
}