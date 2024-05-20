#include <SDL2/SDL.h>
#include <emscripten.h>
#include "test.cpp"

SDL_Window *window;
SDL_Renderer *renderer;

int main(int argc, char *argv[])
{
    SDL_Init(SDL_INIT_VIDEO);
    SDL_CreateWindowAndRenderer(512, 512, 0, &window, &renderer);

    emscripten_set_main_loop(main_loop, 0, 1);
}
