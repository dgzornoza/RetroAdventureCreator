# z88dk-dev-environment README

z88dk Developer Container Environment for Visual Studio Code.

## Features

Project to set up a development environment for zx spectrum with z88dk in a visual studio code dev-container

## Requirements

-   installed visual studio code dev-container extension:
    [https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers](vscode-remote.remote-containers)

-   Docker environment

## How to use

-   Download repository
-   Set the folder name to the desired project name
-   Open folder with Visual Studio Code
-   Press F1, then execute `Dev Containers: Reopen in container`
-   Wait for install container and recomended extensions
-   Execute build task Ctrl + Shift + B (Build)
-   Set break point in main.c.lis and press F5 to debug
-   All ready to develop your project

## Remarks

Container name is created based in folder project name, you can create other folders based on this repository with other names for more projects.
The first time it takes about 10 minutes to compile the image with the latest Z88DK sources.

## z88dk docs

https://github.com/z88dk/z88dk/blob/master/doc/ZXSpectrumZSDCCnewlib_01_GettingStarted.md
https://wiki.speccy.org/cursos/z88dk/contenidos

https://www.z88dk.org/forum/viewtopic.php?t=8908
https://www.z88dk.org/forum/viewtopic.php?f=2&t=11636

http://www.breakintoprogram.co.uk/hardware/computers/zx-spectrum/assembly-language/z80-tutorials/print-in-assembly-language#:~:text=CALL%20ROM_OPEN_CHANNEL%20A%20single%20character%20can%20be%20printed,so%20the%20character%20%E2%80%98A%E2%80%99%20is%20character%20code%2065.

https://espamatica.com/acerca-de/

https://wiki.speccy.org/cursos/ensamblador/indice

https://wiki.speccy.org/indice

## Releases

### 2.2.0

Initial release aligned with z88dk v2.2

**Enjoy!**
