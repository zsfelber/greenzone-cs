#pragma once

#include <string>

class NativeObject
{
public:
	NativeObject(void);
	~NativeObject(void);

	virtual std::string ToString() const;
};

