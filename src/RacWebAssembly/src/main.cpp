#include <SDL2/SDL.h>
#include <SDL2/SDL_ttf.h>
#include <SDL2/SDL_image.h>
#include <emscripten.h>
#include "test.h"

// SDL_Window *window;
// SDL_Renderer *renderer;

bool is_running = true;
// TTF_Font *SamanthaFBFont;

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
    is_running = !draw_next_frame();
}

// void initialize()
// {
//     SDL_Init(SDL_INIT_VIDEO);
//     SDL_CreateWindowAndRenderer(800, 600, 0, &window, &renderer);

//     TTF_Init();

//     SamanthaFBFont = TTF_OpenFont("assets/TheForeign.otf", 64);
//     if (SamanthaFBFont == NULL)
//     {
//         printf("Error : %s\n", SDL_GetError());
//         emscripten_cancel_main_loop();
//     }

//     // Activation de l'alpha (transparence)
//     // SDL_SetRenderDrawBlendMode(renderer, SDL_BLENDMODE_BLEND);

//     is_running = true;
// }

// void destroy()
// {
//     printf("Exit\n");
//     SDL_DestroyWindow(window);
//     SDL_DestroyRenderer(renderer);
//     TTF_CloseFont(SamanthaFBFont);
//     TTF_Quit();
//     IMG_Quit();
//     SDL_Quit();
// }

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
