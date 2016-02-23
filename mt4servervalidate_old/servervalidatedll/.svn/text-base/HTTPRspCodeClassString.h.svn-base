#pragma once

#include "AbstractCodeInfo.h"
#include <string>
#include <map>

class HTTPRspCodeClassString: public AbstractCodeInfo
{
protected:
	std::map<unsigned int,std::string> m_CodeClassStrings;

public:
	HTTPRspCodeClassString(void);
	virtual std::string getInfo( unsigned int p_Code );
};
