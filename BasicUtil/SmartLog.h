// Smartlogger
// Author:	Adrian-Bogdan Andreias
// Email:	adiandreias _at_ softhome.net
// You may freely use this source code in any purpose, commercial or non-commercial

//#ifndef __Lib_SmartLog__
//#define __Lib_SmartLog__

#pragma once

#include "dll.h"
#include "Stypes.h"


DECLDIR_BU enum LogSeverity {
	SEV_PRINT,
	SEV_ERROR,
	SEV_WARNING,
	SEV_INFO,
	SEV_DEBUG_1,
	SEV_DEBUG_2,
	SEV_DEBUG_3,
	SEV_DEBUG_4,
	SEV_DEBUG_5
};

extern DECLDIR_BU LogSeverity LOG_SEVERITY;

extern DECLDIR_BU const char* LogSeverity_Str[];

using namespace System;

class NativeObject;

DECLDIR_BU class CLogFlags
{
public:
	DECLDIR_BU CLogFlags();

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

DECLDIR_BU class CSmartLog
{
public:
	DECLDIR_BU CSmartLog();
	DECLDIR_BU ~CSmartLog();

	// sets the file to which the logs are written
	DECLDIR_BU void SetFilePath(const char *p_pszFile);

	//void SetFilePath(const wchar_t *p_pszFile);

	DECLDIR_BU void SetFilePath(const sstring &p_pszFile);
	
	
	// gets the file to which the logs are written
	// [OUT] p_pszFile will contain the file path
	// if the file path is not longer then m_dwBufferSize
	// returns length of the path string
	DECLDIR_BU sstring &GetFilePath();

	DECLDIR_BU void GetFlags(CLogFlags& p_Flags) const;
	DECLDIR_BU void SetFlags(const CLogFlags& p_Flags);

	DECLDIR_BU CSmartLog& operator << (const bool value);

	DECLDIR_BU CSmartLog& operator << (const short value);

	DECLDIR_BU CSmartLog& operator << (const unsigned short value);

	DECLDIR_BU CSmartLog& operator << (const int value);

	DECLDIR_BU CSmartLog& operator << (const unsigned int value);

	DECLDIR_BU CSmartLog& operator << (const long value);

	DECLDIR_BU CSmartLog& operator << (const unsigned long value);

	DECLDIR_BU CSmartLog& operator << (const float value);

	DECLDIR_BU CSmartLog& operator << (const double value);

	DECLDIR_BU CSmartLog& operator << (const long double value);

	DECLDIR_BU CSmartLog& operator << (const char value);
	
	DECLDIR_BU CSmartLog& operator << (const char* value);

	DECLDIR_BU CSmartLog& operator << (const wchar_t* value);

	DECLDIR_BU CSmartLog& operator << (const void* value);

	DECLDIR_BU CSmartLog& operator << (const sstring &value);

	CSmartLog& operator << (String^ value);

	CSmartLog& operator << (Object^ value);

	DECLDIR_BU CSmartLog& operator << (const NativeObject& value);

	DECLDIR_BU CSmartLog& operator << (const NativeObject* value);

	DECLDIR_BU CSmartLog& WriteFlags(LPCSTR p_szFileName,size_t p_lLine, LPCSTR p_szFunction, LPCSTR p_szFunctionSign);

	CSmartLog& WriteFlags(String ^p_szFileName,String ^p_lLine, String ^p_szFunction, String ^p_szFunctionSign);
protected:

	// Protected methods
	bool WriteToFile(String ^p_szLog);

	// Data
	sstring m_szFile;

	bool m_bFirstLine;

	static const char* m_szSeparator;

	CLogFlags	m_Flags;
};

DECLDIR_BU CSmartLog & __smartlog();

DECLDIR_BU void InitSmartLogC(sstring file);

DECLDIR_BU void InitSmartLogCli(sstring file);


#define smartlog(S)	 if (LOG_SEVERITY>=S) __smartlog().WriteFlags(__FILE__, __LINE__, __FUNCTION__, __FUNCSIG__) << LogSeverity_Str[S]
#define smartlog2(S,F,L,FU,FUSI)	 if (LOG_SEVERITY>=S) __smartlog_cli().WriteFlags(F, L, FU, FUSI) << LogSeverity_Str[S]
#define logline		 sstring(__FILE__) + ":" + __FUNCTION__ + ":" + __LINE__
#define mlogline	 gcnew String(__FILE__) + ":" + __FUNCTION__ + ":" + __LINE__


//#endif // __Lib_SmartLog__
