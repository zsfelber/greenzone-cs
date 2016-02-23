#include "disphelper.h"
#include "base64.h"
#include "GenComputerString.h"
#include <wchar.h>
#include <string.h>


std::string __stdcall GetComputerString(int length)
{

 	DISPATCH_OBJ(wmiSvc);
	DISPATCH_OBJ(colMedia);
    DISPATCH_OBJ(colCPU);


    std::string uniqueIdentifier = "";
    CDispPtr ColProcesador;

	dhInitialize(TRUE);
	dhToggleExceptions(TRUE);




	dhGetObject(L"winmgmts:{impersonationLevel=impersonate}!\\\\.\\root\\cimv2", NULL, &wmiSvc);
	dhGetValue(L"%o", &colMedia, wmiSvc, L".ExecQuery(%S)",  L"SELECT * FROM Win32_PhysicalMedia");
    dhGetValue(L"%o", &colCPU, wmiSvc, L".ExecQuery(%S)",  L"SELECT * FROM Win32_Processor");



	FOR_EACH(wmiCPUItem, colCPU, NULL)
	{
        char *pszCPUCaption = NULL;



        dhGetValue(L"%s", &pszCPUCaption, wmiCPUItem, L".Caption");
        if (pszCPUCaption)
        {
            uniqueIdentifier = uniqueIdentifier + string(pszCPUCaption);
        }

        dhFreeString(pszCPUCaption);

	} NEXT(wmiCPUItem);

	FOR_EACH(wmiMediaItem, colMedia, NULL)
	{
		char *pszSerial = NULL;
		char *pszTag    = NULL;
        std::string szTag;

        dhGetValue(L"%s", &pszTag,    wmiMediaItem, L".Tag");
        szTag = string(pszTag);

        if (szTag.find("PHYSICAL")!=string::npos)
        {
            dhGetValue(L"%s", &pszSerial, wmiMediaItem, L".SerialNumber");
            if (pszSerial)
            {
                uniqueIdentifier = uniqueIdentifier + string(pszSerial);
            }
        }


		dhFreeString(pszTag);
		dhFreeString(pszSerial);

	} NEXT(wmiMediaItem);



	SAFE_RELEASE(colMedia);
	SAFE_RELEASE(colCPU);
	SAFE_RELEASE(wmiSvc);

    std::string encoded = base64_encode(reinterpret_cast<const unsigned char*>(uniqueIdentifier.c_str()), uniqueIdentifier.length());


	dhUninitialize(TRUE);
	return (encoded.substr(0, length));
}


HRESULT dhShowException(PDH_EXCEPTION pException)
{
	WCHAR szMessage[512];

	dhFormatExceptionW(pException, szMessage, ARRAYSIZE(szMessage), FALSE);

#ifdef ERROR_DIALOGS
	MessageBoxW(g_ExceptionOptions.hwnd, szMessage, g_ExceptionOptions.szAppName,
	            MB_ICONSTOP | MB_SETFOREGROUND);
	return NOERROR;
#else
	std::wstring *exception = new std::wstring(szMessage);
	throw exception;
#endif

}
