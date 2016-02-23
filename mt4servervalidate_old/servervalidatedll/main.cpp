/*=============================================================================
ServerValidate
=============================================================================*/

// This dll handles the communication to the web interface
//
//


//#define CURL_STATICLIB
#define WIN32_LEAN_AND_MEAN		// Exclude rarely-used stuff from Windows headers
#include <stdio.h>
#include <tchar.h>

#define _AFXDLL
#include <afx.h>
#include <Windows.h>

#include "xmlParser.h"
#include "convert.h"
#include "CURLRetriever.h"
#include "HTTPRspCodeString.h"
#include "HTTPRspCodeClassString.h"
#include "HTTPRspCodeDescription.h"
#include "GenComputerString.h"
#include "ServerValidate.h"

int statuscode[] = {0};
int validated[] = {0};




//+------------------------------------------------------------------+
//|                                                                  |
//+------------------------------------------------------------------+
 
	
void  ValidateUser(const char *server, const char *username, const int metatraderid, const int debug, int validated[], int statusCode[], char* statusText)
{

    std::string aOpenRequest;
    std::string requestResult;
    //std::string macAddr = GetMACAddr();
    std::string compID = GetComputerString(25);

    CURLRetriever::InitializeCURL();

    aOpenRequest = server;
    aOpenRequest += "/checkStatus.php?username=";

    aOpenRequest += username;
    aOpenRequest += "&compid=";
    aOpenRequest += compID;
    //aOpenRequest += "&compid=12345";
    aOpenRequest += "&mt4acctid=";
    aOpenRequest += stringify(metatraderid);

    CURLRetriever urlRetriever;
	if (debug == 1)
		MessageBox(GetActiveWindow(),aOpenRequest.c_str(),"Request",MB_OK);

    HRESULT hResult = urlRetriever.Initialize(aOpenRequest.c_str());

    LONG lResponseCode = 0;
    std::string sRetrievedContent;
    std::string sRetrievingErrorDescription;
    unsigned int codeClass;
    int retries = 0;
    do
    {


        hResult = urlRetriever.Retrieve(lResponseCode, sRetrievedContent, sRetrievingErrorDescription);
        codeClass = lResponseCode / 100;
        retries++;
    }
    while (codeClass == 5 && retries < 10);

    if (codeClass > 2)
    {
        HTTPRspCodeDescription codeDescr;
        std::string codeStringText = "Communications problem with server.  Description: ";
        codeStringText += codeDescr.getInfo( lResponseCode );
        //strcpy(statusText,codeStringText.c_str());
    }



    if (hResult != S_OK)
    {
        std::string codeStringText = "Communications problem with server.  Description: ";
        codeStringText += sRetrievingErrorDescription.c_str();
        //strcpy(statusText,codeStringText.c_str());
    }

    urlRetriever.Uninitialize();
    CURLRetriever::UninitializeCURL();

	if (debug == 1)
		MessageBox(GetActiveWindow(),sRetrievedContent.c_str(),"Response",MB_OK);

    XMLNode xMainNode=XMLNode::parseString(sRetrievedContent.c_str(),"validateStatus");

    XMLNode xStatusCodeNode=xMainNode.getChildNode("status");

    if (xStatusCodeNode.getText() != NULL)
    {
        statusCode[0] = convertToInt(xStatusCodeNode.getText());
    }
    else
    {
        statusCode[0] = 0;
    }


    XMLNode xStatusTextNode=xMainNode.getChildNode("statustext");

    if (xStatusTextNode.getText() != NULL)
    {
        strcpy(statusText,xStatusTextNode.getText());
    }
    else
    {
        strcpy(statusText," ");
    }


    XMLNode xValidatedNode=xMainNode.getChildNode("validated");

    if (xValidatedNode.getText() != NULL)
    {
        validated[0] = convertToInt(xValidatedNode.getText());
    }
    else
    {
        validated[0] = 0;
    }

	aOpenRequest.resize(0);
    std::string().swap(aOpenRequest);

    sRetrievedContent.resize(0);
    std::string().swap(sRetrievedContent);

    sRetrievingErrorDescription.resize(0);
    std::string().swap(sRetrievingErrorDescription);
}

void InitializeRobot0(const char *username, const int metatraderid, const int debug, int _validated[], int _statuscode[], char* returnbuffer) {
	_statuscode[0] = 0;
	_validated[0] = 0;
#ifdef NO_VALIDATE_ZSF_ONLY
	_statuscode[0] = 1;
	_validated[0] = 1;
#else
	ValidateUser ("https://validation.openthinkingsystems.com", username, metatraderid, debug, _validated, _statuscode, returnbuffer);
#endif
}

void InitializeRobotImpl(const char *username, const int metatraderid, const int debug, int _validated[], int _statuscode[], char* returnbuffer) {
	InitializeRobot0(username, metatraderid, debug, validated, statuscode, returnbuffer);
	_statuscode[0] = statuscode[0];
	_validated[0] = validated[0];
}

bool ok() {
	return validated[0];
}
