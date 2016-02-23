using namespace System;
using namespace System::Collections::Generic;
using namespace System::Diagnostics;
using namespace System::IO;
using namespace System::IO::Pipes;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;
using namespace System::Runtime::Serialization::Formatters::Binary;
using namespace System::Threading;

struct MqlStr;

public ref class Session {
public:
	int sessionId;
	String ^strSessionId;
	static int cntSessionId = 0;

	String ^path;
	Process ^process;
	NamedPipeServerStream ^pipeServer;
	BinaryWriter ^writer;
	BinaryReader ^reader;
	BinaryFormatter ^bformatter;

	bool isTesting;

	int lastTime;

	Assembly ^assembly;
	Type ^invoker;
	array<MethodInfo^> ^methods;

	Session(MqlStr login[]) ;

	~Session() ;

	void initDllVars() ;

	void deinit() ;

	void runMethod(int method, int time, double arguments[], double variables[], MqlStr strings[]) ;

	void log(String ^message) ;
};

public ref class Globals {
	static Dictionary<int,Session^> ^sessions = gcnew Dictionary<int,Session^>();

public:
	static Int32 rnd_id = (Int32)((gcnew Random())->NextDouble()*Int32::MaxValue);

	static Session ^createSession(MqlStr login[]) ;

	static Session ^getSession(int sessionId) ;

	static void deinitSession(int sessionId) ;
};
