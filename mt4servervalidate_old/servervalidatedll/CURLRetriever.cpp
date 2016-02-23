//#include "stdafx.h"
#include "CURLRetriever.h"
#include <iostream>
#include <sstream>

using namespace std;

VOID CURLRetriever::InitializeCURL()
{
	curl_global_init(CURL_GLOBAL_ALL);
}

VOID CURLRetriever::UninitializeCURL()
{
	curl_global_cleanup();
}

CURLRetriever::CURLRetriever()
{
	Clear();
}

CURLRetriever::~CURLRetriever()
{
	Clear();
}

VOID CURLRetriever::Clear()
{
	m_sURL = "";
	m_hCURL = NULL;
}

HRESULT CURLRetriever::Initialize(IN LPCSTR lpszURL)
{
	HRESULT hResult = E_FAIL;

	Uninitialize();

	hResult = E_INVALIDARG;
	if (lpszURL != NULL)
	{
		m_sURL = lpszURL;

		hResult = E_FAIL;
		{
			m_hCURL = curl_easy_init();
			if (m_hCURL != NULL)
			{
				//curl_easy_setopt(m_hCURL, CURLOPT_CONNECTTIMEOUT_MS, (LONG)UR_DOWNLOAD_OPERATION_TIMEOUT);
				curl_easy_setopt(m_hCURL, CURLOPT_FOLLOWLOCATION, TRUE);
				curl_easy_setopt(m_hCURL, CURLOPT_FAILONERROR, TRUE);
				curl_easy_setopt(m_hCURL, CURLOPT_WRITEFUNCTION, &CURLRetriever::OnDataDownloaded);
				curl_easy_setopt(m_hCURL, CURLOPT_SSL_VERIFYPEER, FALSE);
				curl_easy_setopt(m_hCURL, CURLOPT_CONNECTTIMEOUT, 15);
				curl_easy_setopt(m_hCURL, CURLOPT_TIMEOUT, 15);



				hResult = S_OK;
			}
		}
	}

	if (hResult != S_OK)
	{
		Uninitialize();
	}

	return hResult;
}

HRESULT CURLRetriever::Uninitialize()
{
	HRESULT hResult = E_FAIL;

	m_sURL = "";
	curl_easy_cleanup(m_hCURL);
	m_hCURL = NULL;

	Clear();

	hResult = S_OK;

	return hResult;
}

HRESULT CURLRetriever::Retrieve(OUT LONG &lResponseCode, OUT std::string &sRetrievedContent, OUT std::string &sRetrievingErrorDescription)
{
	HRESULT hResult = E_FAIL;

	lResponseCode = 0;
	sRetrievedContent = "";
	sRetrievingErrorDescription = "";

	if (m_hCURL != NULL)
	{
		curl_easy_setopt(m_hCURL, CURLOPT_WRITEDATA, &sRetrievedContent);
		curl_easy_setopt(m_hCURL, CURLOPT_URL, m_sURL.c_str());

		CURLcode result = curl_easy_perform(m_hCURL);

		curl_easy_getinfo(m_hCURL, CURLINFO_RESPONSE_CODE, &lResponseCode);

		if (result == CURLE_OK)
		{
			hResult = S_OK;
		}
		else
		{
			sRetrievingErrorDescription = curl_easy_strerror(result);
		}
	}
	else
	{
		sRetrievingErrorDescription = "CURLRetriever::Initialize() should be called first.";
	}

	return hResult;
}

size_t CURLRetriever::OnDataDownloaded(LPVOID pData, size_t nDataBlockCount, size_t nDataBlocSize, LPVOID pStream)
{
	size_t nResult = 0;

	try
	{
		if (pData != NULL && pStream != NULL)
		{
			string *pRetrievedContent = (string*)pStream;
			if (pRetrievedContent != NULL)
			{
				size_t nDataSize = (DWORD)nDataBlockCount * nDataBlocSize;
				if (nDataSize > 0)
				{
					pRetrievedContent->append((LPCSTR)pData, nDataSize);
					nResult = nDataSize;
				}
			}
		}
	}
	catch (...)
	{
		nResult = 0;
	}

	return nResult;
}

