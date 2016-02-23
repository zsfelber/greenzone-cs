#pragma once

#include "AbstractCodeInfo.h"
#include <string>
#include <map>

class HTTPRspCodeString: public AbstractCodeInfo
{
protected:
	std::map<unsigned int, std::string> m_StatusCodeStrings;
public:
	HTTPRspCodeString(void);
	virtual std::string getInfo( unsigned int pResponseCode );
};
