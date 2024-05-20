cmake_minimum_required(VERSION 3.21)

project(sample)

add_executable(${PROJECT_NAME}  
    src/main.cpp
    src/test.cpp
)

message(CMAKE_SYSTEM_NAME => ${CMAKE_SYSTEM_NAME})

#file(COPY assets DESTINATION ${CMAKE_CURRENT_BINARY_DIR}/Debug)
#file(COPY assets DESTINATION ${CMAKE_CURRENT_BINARY_DIR}/Release)

if (CMAKE_SYSTEM_NAME STREQUAL Emscripten)
    set(CMAKE_EXECUTABLE_SUFFIX .html)
    #target_link_options(${PROJECT_NAME} PUBLIC --use-port=sdl2 --use-port=sdl2_ttf --use-port=sdl2_image:formats=bmp,png,xpm,jpg --preload-file ../assets --shell-file=../assets/shell.html)
    target_link_options(${PROJECT_NAME} PUBLIC --use-port=sdl2 --use-port=sdl2_ttf --use-port=sdl2_image --preload-file ../assets --shell-file=../assets/shell.html)
endif()