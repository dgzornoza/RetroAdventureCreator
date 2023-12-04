// https://github.com/z88dk/z88dk/blob/master/doc/ZXSpectrumZSDCCnewlib_06_SomeDetails.md
// add pragma directives ...

// IM2 Interrupts https://github.com/z88dk/z88dk/blob/master/doc/ZXSpectrumZSDCCnewlib_08_Interrupts.md
#pragma output REGISTER_SP = 0xFF58
#pragma output CLIB_MALLOC_HEAP_SIZE = -0xFBFA

// https://github.com/z88dk/z88dk/wiki/WritingOptimalCode
#pragma output CLIB_EXIT_STACK_SIZE     = 0         // never exit, remove atexit stack
#pragma output CRT_INITIALIZE_BSS       = 0         // don't use dynamic memory 

#pragma output CRT_ORG_CODE             = 24500     // org of compile this should be higher to allocate graphics and allow compatibility 16Kb


