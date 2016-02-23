// Smartlogger
// Author:	Adrian-Bogdan Andreias
// Email:	adiandreias _at_ softhome.net
// You may freely use this source code in any purpose, commercial or non-commercial

#ifndef __Lib_SmartLog__
#define __Lib_SmartLog__

#pragma once
#include <windows.h>
#include <string>

enum LogSeverity {
	SEV_ERROR,
	SEV_WARNING,
	SEV_INFO,
	SEV_DEBUG_1,
	SEV_DEBUG_2,
	SEV_DEBUG_3,
	SEV_DEBUG_4,
	SEV_DEBUG_5
};

extern LogSeverity LOG_SEVERITY;

extern const char* LogSeverity_Str[];

class NativeObject;

class CLogFlags
{
public:
	CLogFlags();

	// global flag for logs
	// when set to false, no logs are made
	bool	m_bGlobalFlag;

	// when true, logs the name of the file from where the logging is made
	bool	m_bFileFlag;

	// when true, logs the line number from where the logging is made
	bool	m_bLineFlag;

	// when true, logs the name of the function name
	bool	m_bFunctionFlag;

	// when true, logs the function, including name and its signature
	// when true, m_bFunctionFlag is ignored
	bool	m_bFunctionSignFlag;

	// when true, logs the time stamp
	bool	m_bTimestampFlag;
};

class CSmartLog
{
public:
	CSmartLog();
	~CSmartLog();

	// sets the file to which the logs are written
	void SetFilePath(char *p_pszFile);
	
	// gets the file to which the logs are written
	// [OUT] p_pszFile will contain the file path
	// if the file path is not longer then m_dwBufferSize
	// returns length of the path string
	size_t GetFilePath(char *p_pszFile, size_t m_lBufferSize) const;

	void GetFlags(CLogFlags& p_Flags) const;
	void SetFlags(const CLogFlags& p_Flags);

	CSmartLog& operator << (const bool p_bVal);

	CSmartLog& operator << (const short p_bVal);

	CSmartLog& operator << (const unsigned short p_bVal);

	CSmartLog& operator << (const int p_bVal);

	CSmartLog& operator << (const unsigned int p_bVal);

	CSmartLog& operator << (const long p_bVal);

	CSmartLog& operator << (const unsigned long p_bVal);

	CSmartLog& operator << (const float p_bVal);

	CSmartLog& operator << (const double p_bVal);

	CSmartLog& operator << (const long double p_bVal);

	CSmartLog& operator << (const char p_bVal);

	CSmartLog& operator << (const char* p_bVal);

	CSmartLog& operator << (const void* p_bVal);

	CSmartLog& operator << (const std::string& value);

	CSmartLog& operator << (NativeObject& value);

	CSmartLog& operator << (NativeObject* value);
	
	CSmartLog& WriteFlags(LPCSTR p_szFileName,size_t p_lLine, LPCSTR p_szFunction, LPCSTR p_szFunctionSign);
protected:

	// Protected methods
	bool WriteToFile(LPCSTR p_szLog);

	// Data
	char m_szFile[MAX_PATH];

	bool m_bFirstLine;

	static const char m_szSeparator[];

	CLogFlags	m_Flags;
};

extern	CSmartLog	__smartlog;

#define smartlog(S)	 if (LOG_SEVERITY>=S) __smartlog.WriteFlags(__FILE__, __LINE__, __FUNCTION__, __FUNCSIG__) << LogSeverity_Str[S] << " "

#endif // __Lib_SmartLog__
