# RacWebAssembly README

RetroAdventureCreator  in webassembly with SDL2

Author: David González Zornoza

## Features

--

## Requirements

- installed visual studio code dev-container extension:
  [https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers](vscode-remote.remote-containers)

- Docker environment

## How to use

- Download repository
- Set the folder name to the desired project name
- Open folder with Visual Studio Code
- Press F1, then execute `Dev Containers: Reopen in container`
- Wait for install container and recomended extensions
- Execute build task Ctrl + Shift + B (Build) (**Remarks**: main.cpp should be commented first time for compile and download SDL2 port)
- Uncomment main.cpp (ctrl + k + u) and run again execute build task Ctrl + Shift + B (Build)
- Press F5 to run and debug the project
- All ready to develop your project

## Remarks

Container name is created based in folder project name, you can create other folders based on this repository with other names for more projects.
The first time it download docker hub image with the sjasmplus sources from 2023-009-05 and PASMO 0.5.5

## emscripten build commands

This commads are for information, the proyect is configured to use the build task in visual studio code with cmake.

```bash
#show available SDL ports
emcc --show-ports
```

```bash
# compile to wasm
emcc -c src/sdl_1_2_sample.cpp -o obj/sdl_1_2_sample.o
# link to html
emcc obj/sdl_1_2_sample.o -o public/sdl_1_2_sample.html

# compile and link in one step
emcc src/sdl_1_2_sample.cpp -o public/sdl_1_2_sample.html

# publish with debug symbols
EMCC_DEBUG=1 emcc src/sdl_1_2_sample.cpp -o public/sdl_1_2_sample.html
```

## Links of interest

- emscripten: <https://emscripten.org/index.html>

- My Github account: <https://github.com/dgzornoza>

## Releases

### 1.0.0

Initial release emscripten 3.1.59

**Enjoy!**
