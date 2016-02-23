#pragma once
#include <string>
//#define CURL_STATICLIB
#include <curl/curl.h>

///////////////////////////////////////////////////	///////////////////////
#define UR_DOWNLOAD_OPERATION_TIMEOUT (30000) // (in milliseconds)
//////////////////////////////////////////////////////////////////////////

class CURLRetriever
{
public:
	static VOID InitializeCURL();
	static VOID UninitializeCURL();

	CURLRetriever();
	virtual ~CURLRetriever();

protected:
	VOID Clear();

public:
	HRESULT Initialize(IN LPCSTR lpszURL);
	HRESULT Uninitialize();

	HRESULT Retrieve(OUT LONG &lResponseCode, OUT std::string &sRetrievedContent, OUT std::string &sRetrievingErrorDescription);

protected:
	static size_t OnDataDownloaded(LPVOID pData, size_t nDataBlockCount, size_t nDataBlocSize, LPVOID pStream);

protected:
	std::string m_sURL;

	CURL* m_hCURL;
};
