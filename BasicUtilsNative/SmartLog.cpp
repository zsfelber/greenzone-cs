// Smartlogger
// Author:	Adrian-Bogdan Andreias
// Email:	adiandreias _at_ softhome.net
// You may freely use this source code in any purpose, commercial or non-commercial

#include "StdAfx.h"
#include "smartlog.h"
#include "NativeObject.h"

const char* LogSeverity_Str[] = {
	"ERROR",
	"WARNING",
	"INFO",
	"DEBUG(1)",
	"DEBUG(2)",
	"DEBUG(3)",
	"DEBUG(4)",
	"DEBUG(5)"
};

CLogFlags::CLogFlags()
{
	// default values
	m_bGlobalFlag		= true;
	m_bFileFlag			= false;
	m_bLineFlag			= false;
	m_bFunctionFlag		= true;
	m_bFunctionSignFlag = false;
	m_bTimestampFlag	= true;
}

const char CSmartLog::m_szSeparator[] = "; ";
CSmartLog	__smartlog;

CSmartLog::CSmartLog(void)
{
	m_bFirstLine = true;
}

CSmartLog::~CSmartLog(void)
{
}

void CSmartLog::SetFilePath(char *p_pszFile)
{
	strcpy(m_szFile, p_pszFile);
}

size_t CSmartLog::GetFilePath(char *p_pszFile, size_t m_lBufferSize) const
{
	if (m_lBufferSize >= strlen(m_szFile))
	{
		p_pszFile[0] = '\0';
	}
	else
	{
		strcpy(p_pszFile, m_szFile);
	}

	return strlen(m_szFile);
}

void CSmartLog::GetFlags(CLogFlags& p_Flags) const
{
	p_Flags = m_Flags;
}

void CSmartLog::SetFlags(const CLogFlags& p_Flags)
{
	m_Flags = p_Flags;
}


CSmartLog& CSmartLog::operator << (const bool p_bVal) 
{
	if (p_bVal)
		WriteToFile("true");
	else
		WriteToFile("false");

	return *this;
}

CSmartLog& CSmartLog::operator << (const short p_Val)
{
	char buffer[20];
	sprintf(buffer, "%d", (int)p_Val);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const unsigned short p_bVal)
{
	return *this;
}

CSmartLog& CSmartLog::operator << (const int p_nVal)
{
	char buffer[20];
	sprintf(buffer, "%d", p_nVal);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const unsigned int p_nVal)
{
	char buffer[20];
	sprintf(buffer, "%u", p_nVal);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const long p_lVal)
{
	char buffer[20];
	sprintf(buffer, "%d", p_lVal);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const unsigned long p_lVal)
{
	char buffer[20];
	sprintf(buffer, "%u", p_lVal);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const float p_fVal)
{
	char buffer[20];
	sprintf(buffer, "%f", p_fVal);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const double p_dVal)
{
	char buffer[20];
	sprintf(buffer, "%f", p_dVal);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const long double p_ldVal)
{
	char buffer[20];
	sprintf(buffer, "%f", p_ldVal);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const char p_chVal)
{
	char buffer[20];
	sprintf(buffer, "%c", p_chVal);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const char* p_szVal)
{
	WriteToFile(p_szVal);
	return *this;
}

CSmartLog& CSmartLog::operator << (const void* p_pvVal)
{
	char buffer[20];
	sprintf(buffer, "%p", p_pvVal);
	WriteToFile(buffer);
	return *this;
}

CSmartLog& CSmartLog::operator << (const std::string& value) {
	WriteToFile(value.c_str());
	return *this;
}


CSmartLog& CSmartLog::operator << (NativeObject& value)
{
	WriteToFile(value.ToString().c_str());
	return *this;
}

CSmartLog& CSmartLog::operator << (NativeObject* value)
{
	WriteToFile(value->ToString().c_str());
	return *this;
}

CSmartLog& CSmartLog::WriteFlags(LPCSTR p_szFileName, size_t p_lLine, LPCSTR p_szFunction, LPCSTR p_szFunctionSign)
{
	char szBuffer [10240];
	char szTmpBuffer [1024];
	szBuffer[0] = '\0';

	if (m_bFirstLine)
	{
		m_bFirstLine = false;
	}
	else
	{
		strcat(szBuffer, "\r\n");
	}

	if (m_Flags.m_bTimestampFlag)
	{
		SYSTEMTIME time;
		::GetLocalTime(&time);

		char szSecond[3], szMonth[3], szDay[3], szHour[3], szMinute[3];
		
		if (time.wSecond < 10)
		{
			szSecond[0] = '0';
			szSecond[1] = '\0';
		}
		else
			szSecond[0] = '\0';
		
		if (time.wMinute < 10)
		{
			szMinute[0] = '0';
			szMinute[1] = '\0';
		}
		else
			szMinute[0] = '\0';
		
		if (time.wHour < 10)
		{
			szHour[0] = '0';
			szHour[1] = '\0';
		}
		else
			szHour[0] = '\0';
		
		if (time.wDay < 10)
		{
			szDay[0] = '0';
			szDay[1] = '\0';
		}
		else
			szDay[0] = '\0';
		
		if (time.wMonth < 10)
		{
			szMonth[0] = '0';
			szMonth[1] = '\0';
		}
		else
			szMonth[0] = '\0';
		
		sprintf(szTmpBuffer, "%s%d/%s%d/%d %s%d:%s%d:%s%d ", 
			szMonth, time.wMonth, szDay, time.wDay, time.wYear, szHour, time.wHour, szMinute, time.wMinute, szSecond, time.wSecond);
		
		strcat(szBuffer, szTmpBuffer);
	}

	if (m_Flags.m_bFileFlag)
	{
		sprintf(szTmpBuffer, "File: \"%s\"%s", p_szFileName, m_szSeparator);
		strcat(szBuffer, szTmpBuffer);
	}

	if (m_Flags.m_bLineFlag)
	{
		sprintf(szTmpBuffer, "Line: %d%s", p_lLine, m_szSeparator);
		strcat(szBuffer, szTmpBuffer);
	}

	if (m_Flags.m_bFunctionSignFlag)
	{
		sprintf(szTmpBuffer, "%s%s", p_szFunctionSign, ": ");
		strcat(szBuffer, szTmpBuffer);
	}
	else
	{
		// this is ignored when m_bFunctionSignFlag is true
		sprintf(szTmpBuffer, "%s%s", p_szFunction, ": ");
		strcat(szBuffer, szTmpBuffer);
	}

	WriteToFile(szBuffer);

	return *this;
}

bool CSmartLog::WriteToFile(LPCSTR p_szLog) 
{
	if (m_Flags.m_bGlobalFlag)
	{
		DWORD dwBytesWritten = 0;
		HANDLE hFile = INVALID_HANDLE_VALUE;

		hFile = ::CreateFile (	m_szFile, GENERIC_WRITE, FILE_SHARE_READ, NULL,
								OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

		if (INVALID_HANDLE_VALUE == hFile)
			return false;

		// append to file
		SetFilePointer(hFile,0,0,FILE_END);
		if (0 == WriteFile(hFile, p_szLog, lstrlen(p_szLog), &dwBytesWritten, NULL))
		{
			CloseHandle(hFile);
			return false;
		}

		CloseHandle(hFile);
	}

	return true;
}
