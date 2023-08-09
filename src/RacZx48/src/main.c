#include <arch/spectrum.h>
#include <stdlib.h>
#include "./screen.h"

int main()
{
    openScrChannel(2);
    print("a");
    openScrChannel(1);
    print("b");

    return 0;
}