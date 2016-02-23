// This is the main DLL file.

#include "stdafx.h"

#include "ServerValidateDummy.h"

void InitializeRobotImpl(const char *username, const int metatraderid, const int debug, int _validated[], int _statuscode[], char* returnbuffer) {
	_validated[0] = 1;
}

bool ok() {
	return true;
}
