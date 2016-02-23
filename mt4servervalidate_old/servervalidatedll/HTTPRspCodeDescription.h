#pragma once
#include "abstractcodeinfo.h"
#include <string>
#include <map>
class HTTPRspCodeDescription :
	public AbstractCodeInfo
{
protected:
	std::map<unsigned int, std::string> m_CodeDescriptionList;

public:
	HTTPRspCodeDescription(void);
	virtual std::string getInfo( unsigned int pResponseCode );
};
