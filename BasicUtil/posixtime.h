#pragma once

#include <time.h>
#include <windows.h>

struct timeval : tm
{
    int tm_usec;     /* microsecs */
};
struct timezone
{
	int  tz_minuteswest; /* minutes W of Greenwich */
	int  tz_dsttime;     /* type of dst correction */
};

int gettimeofday(struct timeval *tv, struct timezone *tz);
