#define MT4_IMPFUNC __declspec(dllimport)
#define export extern "C" __declspec( dllimport )

struct MqlStr {
	int	len;
	char* string;
	MqlStr() {
	}
	MqlStr(int len, char* string) : len(len), string(string) {
	}
};

#ifdef __cplusplus
extern "C"
{
#endif

	MT4_IMPFUNC void __stdcall Log(int sessionId, const char *message) ;

	MT4_IMPFUNC int __stdcall FxSubInit(MqlStr login[]) ;

	MT4_IMPFUNC bool __stdcall FxSubDeinit(int sessionId, MqlStr login[]) ;

	MT4_IMPFUNC bool __stdcall FxSubInvoke(int sessionId, int method, int time, double arguments[], double variables[], MqlStr strings[]) ;

#ifdef __cplusplus
}
#endif
