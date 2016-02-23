#pragma once

#if defined DLL_BASICUTILS_EXPORT
#define DECLDIR_BU __declspec(dllexport)
#else
#define DECLDIR_BU __declspec(dllimport)
#endif
