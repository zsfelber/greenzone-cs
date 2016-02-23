// Smartlogger
// Author:	Adrian-Bogdan Andreias
// Email:	adiandreias _at_ softhome.net
// You may freely use this source code in any purpose, commercial or non-commercial

#include "StdAfx.h"
#include "SmartLog_ref.h"

using namespace System::IO;


LogSeverity LOG_SEVERITY = SEV_DEBUG_2;
const char* LogSeverity_Str[] = {
	"-----",
	"ERROR",
	"WARNING",
	"INFO",
	"DEBUG(1)",
	"DEBUG(2)",
	"DEBUG(3)",
	"DEBUG(4)",
	"DEBUG(5)"
};

CSmartLog &__smartlog() {
	return SmartLog::cs();
}

CSmartLog &__smartlog_cli() {
	return SmartLog::clis();
}

void _InitSmartLogC(sstring file) {
	SmartLog::InitC(file.toMString());
}

void _InitSmartLogCli(sstring file) {
	SmartLog::InitCli(file.toMString());
}

void InitSmartLogC(String ^file) {
	SmartLog::InitC(file);
}

void InitSmartLogCli(String ^file) {
	SmartLog::InitCli(file);
}


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

const char *CSmartLog::m_szSeparator = "; ";

CSmartLog::CSmartLog(void)
{
	m_bFirstLine = true;
}

CSmartLog::~CSmartLog(void)
{
}

void CSmartLog::SetFilePath(const char *p_pszFile)
{
	m_szFile = p_pszFile;
}

//void CSmartLog::SetFilePath(const wchar_t *p_pszFile)
//{
//	m_szFile = p_pszFile;
//}

void CSmartLog::SetFilePath(const sstring &p_pszFile)
{
	m_szFile = p_pszFile;
}


sstring &CSmartLog::GetFilePath()
{
	return m_szFile;
}

void CSmartLog::GetFlags(CLogFlags& p_Flags) const
{
	p_Flags = m_Flags;
}

void CSmartLog::SetFlags(const CLogFlags& p_Flags)
{
	m_Flags = p_Flags;
}


CSmartLog& CSmartLog::operator << (const bool value)
{
	if (value)
		WriteToFile("true");
	else
		WriteToFile("false");

	return *this;
}

CSmartLog& CSmartLog::operator << (const short value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const unsigned short value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const int value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const unsigned int value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const long value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const unsigned long value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const float value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const double value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const long double value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const char value)
{
	String ^s = "";
	s += value;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const char* value)
{
	WriteToFile(gcnew String(value));
	return *this;
}

CSmartLog& CSmartLog::operator << (const wchar_t* value)
{
	WriteToFile(gcnew String(value));
	return *this;
}

CSmartLog& CSmartLog::operator << (const void* value)
{
	int addr = (int) value;
	String ^s = "@";
	s += addr;
	WriteToFile(s);
	return *this;
}

CSmartLog& CSmartLog::operator << (const sstring &value)
{
	WriteToFile(value.toMString());
	return *this;
}

CSmartLog& CSmartLog::operator << (String^ value)
{
	WriteToFile(value);
	return *this;
}

CSmartLog& CSmartLog::operator << (Object^ value)
{
	WriteToFile(value->ToString());
	return *this;
}

CSmartLog& CSmartLog::operator << (const NativeObject& value)
{
	WriteToFile(value.ToString().toMString());
	return *this;
}

CSmartLog& CSmartLog::operator << (const NativeObject* value)
{
	WriteToFile(value->ToString().toMString());
	return *this;
}

CSmartLog& CSmartLog::WriteFlags(LPCSTR p_szFileName, size_t p_lLine, LPCSTR p_szFunction, LPCSTR p_szFunctionSign)
{
	return WriteFlags(gcnew String(p_szFileName), ""+p_lLine, gcnew String(p_szFunction), gcnew String(p_szFunctionSign));
}


CSmartLog& CSmartLog::WriteFlags(String ^p_szFileName, String ^p_lLine, String ^p_szFunction, String ^p_szFunctionSign)
{
	synchronized (SmartLog::mx()) {
		String ^szTmpBuffer = "";
		String ^separator = gcnew String(m_szSeparator);

		if (m_bFirstLine)
		{
			m_bFirstLine = false;
		}
		else
		{
			szTmpBuffer += "\r\n";
		}

		if (m_Flags.m_bTimestampFlag)
		{
			SYSTEMTIME time;
			::GetLocalTime(&time);
		
			szTmpBuffer +=	i2(time.wMonth)+"/"+i2(time.wDay)+"/"+time.wYear+" "+
							i2(time.wHour)+":"+i2(time.wMinute)+":"+i2(time.wSecond)+separator;
		}

		if (m_Flags.m_bFileFlag)
		{
			szTmpBuffer =	szTmpBuffer+"File:\""+p_szFileName+"\""+separator;
		}

		if (m_Flags.m_bLineFlag)
		{
			szTmpBuffer =	szTmpBuffer+"Line:"+p_lLine+separator;
		}

		if (m_Flags.m_bFunctionSignFlag)
		{
			szTmpBuffer =	szTmpBuffer+p_szFunctionSign+": ";
		}
		else
		{
			szTmpBuffer =	szTmpBuffer+p_szFunction+": ";
		}

		WriteToFile(szTmpBuffer);

		return *this;
	}
}

bool CSmartLog::WriteToFile(String ^p_szLog)
{
	synchronized (SmartLog::mx()) {
		if (m_Flags.m_bGlobalFlag)
		{
			String ^path;
			try {
				path = gcnew String(m_szFile);
				StreamWriter ^sw = gcnew StreamWriter(path,true);
				sw->WriteLine(p_szLog);
				sw->Flush();
				sw->Close();

				Console::WriteLine(p_szLog);
				return true;
			} catch ( Exception ^e ) {
				Console::WriteLine("CSmartLog::WriteToFile path:"+path+" p_szLog:"+p_szLog+" error: "+e);
				return false;
			} catch (const char* err) {
				Console::WriteLine("CSmartLog::WriteToFile path:"+path+" p_szLog:"+p_szLog+" error: "+gcnew String(err));
				return false;
			} catch (const int err) {
				Console::WriteLine("CSmartLog::WriteToFile path:"+path+" p_szLog:"+p_szLog+" error "+err);
				return false;
			}
		}
	}
}

Mutex &SmartLog::mx() {
	if (cSmartLog==NULL) {
		cSmartLog = new CSmartLog;
		cliSmartLog = new CSmartLog;
		MxLogFile = new Mutex;
		Console::WriteLine((sstring()+"CSmartLog::mx  MxLogFile=new Mutex:"+MxLogFile+" cSmartLog=new CSmartLog:"+cSmartLog).toMString());
	}
	return *MxLogFile;
}

CSmartLog &SmartLog::cs() {
	mx();
	return *cSmartLog;
}

CSmartLog &SmartLog::clis() {
	mx();
	return *cliSmartLog;
}

void SmartLog::InitLog(CSmartLog *log, String ^file) {
	synchronized (mx()) {
		if (log==NULL || !Utils::Equal(log->GetFilePath().toMString(),file)) {
			File::Delete(file);
			CLogFlags flags;
			log->SetFilePath(file);
			log->GetFlags(flags);
			flags.m_bLineFlag = true;
			flags.m_bFileFlag = true;
			log->SetFlags(flags);

			smartlog(SEV_PRINT) << "SmartLog()   log file : "+file;
		} else {
			EALogger::Log("SmartLog::Init  re-init  file has not changed:"+file, SEV_PRINT, __FILE__, ""+__LINE__, __FUNCTION__, __FUNCSIG__);
		}
	}
}

void SmartLog::InitC(String ^file) {
	mx();
	InitLog(cSmartLog,file);
}

void SmartLog::InitCli(String ^file) {
	synchronized (mx()) {
		if (Logger != nullptr) {
			EALogger::Log("This is the last test message to Obsolete Logger object", SEV_PRINT, __FILE__, ""+__LINE__, __FUNCTION__, __FUNCSIG__);
		}
		InitLog(cliSmartLog,file);
		if (Logger != nullptr) {
			EALogger::DLog -= Logger;
			smartlog(SEV_PRINT) << "SmartLog()  Obsolete Logger object is removed...";
		}
		Logger = gcnew EALogger::DEALogger(Log);
		EALogger::DLog += Logger;
		smartlog(SEV_PRINT) << "SmartLog()  Logger .net bridge initialized. Testing...";
		EALogger::Log("This is a test message from .NET Logger (EALogger)", SEV_PRINT, __FILE__, ""+__LINE__, __FUNCTION__, __FUNCSIG__);
	}
}

void SmartLog::Log(String ^s, int severity, String ^filePath, String ^lineNumber, String ^function, String ^functionSign) {
	smartlog2(severity,filePath,lineNumber,function,functionSign) << s;
}
