#ifndef __DGZ_TIMER_H__
#define __DGZ_TIMER_H__

/** DEFINITIONS */
// -----------------------------------------------------------------------------
/** Ticks per second used in ISR IM2 */
#define TicksPerSecond 50

/** INFRASTRUCTURE */
// -----------------------------------------------------------------------------

extern unsigned char GLOBAL_TIMER_TICKS;

/** FUNCTIONS */
// -----------------------------------------------------------------------------

/**
 * routine for update timer, should be called from ISR IM2 for update
 */
extern void update_timer(void) __z88dk_fastcall;

#endif