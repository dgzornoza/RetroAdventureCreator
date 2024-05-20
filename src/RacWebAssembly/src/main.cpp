#include <SDL2/SDL.h>
#include <emscripten.h>
#include "test.h"

int main(int argc, char *argv[])
{
    initialize();
    emscripten_set_main_loop(mainLoop, 0, 1);

    return EXIT_SUCCESS;
}
