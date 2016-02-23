// Metatrader4.cpp : Defines the exported functions for the DLL application.
//

#include <windows.h>
#include <stdio.h>
#include <tchar.h>
#include "MainDllBase.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Diagnostics;
using namespace System::IO;
using namespace System::IO::Pipes;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;
using namespace System::Runtime::Serialization::Formatters::Binary;
using namespace System::Threading;

struct MqlStr {
	int	len;
	char* string;
	MqlStr() {
	}
	MqlStr(int len, char* string) : len(len), string(string) {
	}
};

typedef void (CALLBACK* InitType)(int);
typedef void (CALLBACK* FunType)(double*, double*, MqlStr*);


Session::Session(MqlStr login[]) {
	sessionId = ++cntSessionId;

	strSessionId = "fxsub."+Process::GetCurrentProcess()->Id+"."+Globals::rnd_id+"."+sessionId;
	pipeServer = gcnew NamedPipeServerStream(strSessionId);
	writer = gcnew BinaryWriter(pipeServer);
	reader = gcnew BinaryReader(pipeServer);
	bformatter = gcnew BinaryFormatter();

	path = AppDomain::CurrentDomain->BaseDirectory;
	if (!path->EndsWith("\\Debug\\") && !path->EndsWith("\\Release\\")) {
		path = path + "experts\\libraries\\";
	}
	process = gcnew Process;
#ifdef DEBUG
	process->StartInfo->FileName = path+"FxSubmarineTaskbarAppDbg.exe";
#else
	process->StartInfo->FileName = path+"FxSubmarineTaskbarApp.exe";
#endif
	process->StartInfo->Arguments = strSessionId;
	process->StartInfo->UseShellExecute = false;
	//process->StartInfo->CreateNoWindow = true;
	if (process->Start()) {
		pipeServer->WaitForConnection();
		for (int i=0; i<4; i++) {
			writer->Write(gcnew String(login[i].string));
		}
		isTesting = login[4].string[0]=='1';
		writer->Write(isTesting);

		array<unsigned char> ^dllBytes;

        // TODO Downloading from server side...
#if (DEBUG)
        dllBytes = File::ReadAllBytes("F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\SubmarineLicenseManager\\Debug\\MillionDollarFx.dll");
        Console::WriteLine("Robot bytes received.");
#else
        dllBytes = File::ReadAllBytes("F:\\workspaces\\general_web\\ForexRobots\\windows_dll\\SubmarineLicenseManager\\Release\\MillionDollarFx.dll");
#endif


		assembly = Assembly::Load(dllBytes);

		invoker = assembly->GetType("Invoker");
		methods = gcnew array<MethodInfo^>(100);

		lastTime = 0;

		// process must terminate itself or you can do it programmatically 
		// from this application using the Kill method.
#ifdef DEBUG
		log("Process started...  strSessionId:"+strSessionId+" dllLen:"+dllBytes->Length);
#endif

	} else {
		cntSessionId--;
		Console::WriteLine("Could not start Process  strSessionId:"+strSessionId);
		throw gcnew Exception("Process not started  strSessionId:"+strSessionId);
	}
}

Session::~Session() {
	File::Delete(path+strSessionId+".dll");
}

void Session::initDllVars() {
    MethodInfo ^m = invoker->GetMethod("init");

	m->Invoke(nullptr, gcnew array<Object^> { sessionId });
}

void Session::deinit() {
	writer->Write((Int32)-1);
	writer->Flush();
}

void Session::runMethod(int method, int time, double arguments[], double variables[], MqlStr strings[]) {
	if (isTesting) {
		if (lastTime!=time && time%600==0) {
			writer->Write((Int32)method);
			writer->Write((Int32)time);
			writer->Flush();
			lastTime = time;
		}
	} else {
		if (lastTime != time) {
			writer->Write((Int32)method);
			writer->Write((Int32)time);
			writer->Flush();
			lastTime = time;
		}
	}

	MethodInfo ^m = methods[method];
	if (m==nullptr) {
		m = invoker->GetMethod("fun" + method);
		methods[method] = m;
	}
	m->Invoke(nullptr, gcnew array<Object^> { (IntPtr)arguments, (IntPtr)variables, (IntPtr)strings });
}

void Session::log(String ^message) {
	writer->Write((Int32)-2);
	writer->Write(message);
	writer->Flush();
}



Session ^Globals::createSession(MqlStr login[]) {
	Monitor::Enter(sessions);
	try {
		Session ^session = gcnew Session(login);
		sessions[session->sessionId] = session;
		session->initDllVars();
		return session;
	} finally {
		Monitor::Exit(sessions);
	}
}

Session ^Globals::getSession(int sessionId) {
	Monitor::Enter(sessions);
	try {
		return sessions[sessionId];
	} finally {
		Monitor::Exit(sessions);
	}
}

void Globals::deinitSession(int sessionId) {
	Monitor::Enter(sessions);
	try {
		sessions[sessionId]->deinit();
		sessions->Remove(sessionId);
	} finally {
		Monitor::Exit(sessions);
	}
}


