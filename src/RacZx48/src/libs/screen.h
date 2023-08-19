#ifndef __SCREEN_H__
#define __SCREEN_H__

/**
 * @param chain chain to set: 1 = command line, 2 = upper screen
 */
extern void zx_open_screen_chain(char chain) __z88dk_fastcall;

extern void test() __z88dk_fastcall;

#endif