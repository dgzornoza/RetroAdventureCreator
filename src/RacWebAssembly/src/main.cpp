#include <SDL2/SDL.h>
#include <emscripten.h>
#include "test.h"

extern bool is_running;

static void mainloop(void) /* this will run often, possibly at the monitor's refresh rate */
{
    if (!is_running)
    {
        destroy();
#ifdef __EMSCRIPTEN__
        emscripten_cancel_main_loop(); /* this should "kill" the app. */
#else
        exit(0);
#endif
    }

    // check_for_new_input();
    // think_about_stuff();
    draw_next_frame();
}

int main(int argc, char *argv[])
{
    initialize();
#ifdef __EMSCRIPTEN__
    emscripten_set_main_loop(mainloop, 0, 1);
#else
    while (1)
    {
        mainloop();
    }
#endif
}
