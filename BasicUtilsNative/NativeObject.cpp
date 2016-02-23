#include "StdAfx.h"
#include "NativeObject.h"

#include <sstream>

NativeObject::NativeObject(void)
{
}


NativeObject::~NativeObject(void)
{
}

std::string NativeObject::ToString() const {
	int addr = (int) (void*) this;
	std::stringstream ss;
	ss<<"@"<<addr;
	return ss.str();
}
