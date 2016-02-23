#pragma once

#include <string>

class AbstractCodeInfo
{
public:
	virtual std::string getInfo( unsigned int pResponseCode ) = 0;
	virtual ~AbstractCodeInfo(void);
};
