
#include "HTTPRspCodeClassString.h"

HTTPRspCodeClassString::HTTPRspCodeClassString(void)
{
	m_CodeClassStrings[ 1 ] = "Informational - Request received, continuing process";
	m_CodeClassStrings[ 2 ] = "Success - The action was successfully received understood, and accepted";
	m_CodeClassStrings[ 3 ] = "Redirection - Further action must be taken in order to complete the request";
	m_CodeClassStrings[ 4 ] = "Client Error - The request contains bad syntax or cannot be fulfilled";
	m_CodeClassStrings[ 5 ] = "Server Error - The server failed to fulfill an apparently valid request";
}

std::string HTTPRspCodeClassString::getInfo( unsigned int p_Code ) {
	unsigned int codeClass = p_Code / 100;
	std::map<unsigned int,std::string>::iterator codeClassItr = m_CodeClassStrings.find( codeClass );
	if ( codeClassItr != m_CodeClassStrings.end() ) {
		return codeClassItr->second;
	}
	return "UNKNOWN CODE CLASS";
}
