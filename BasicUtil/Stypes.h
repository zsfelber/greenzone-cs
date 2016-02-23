//#ifndef __Lib_Stypes__
//#define __Lib_Stypes__

#pragma once

#include <string>
#include "dll.h"
#include "Lock.h"

using namespace System;

#define Max(a,b) (((a)>=(b))?(a):(b))
#define Min(a,b) (((a)<=(b))?(a):(b))

#define WHOLE_ARRAY 0// Used with array functions. Indicates that all array elements will be processed. 

class sstring;

DECLDIR_BU class NativeObject
{
public:
	DECLDIR_BU NativeObject(void);
	DECLDIR_BU virtual ~NativeObject(void);

	DECLDIR_BU virtual sstring ToString() const;
};

DECLDIR_BU class sstring : public std::string {
public:
	DECLDIR_BU sstring();
	DECLDIR_BU sstring(const char* buf);
	DECLDIR_BU sstring(const std::string& str);
	DECLDIR_BU sstring(const sstring& str);
	DECLDIR_BU sstring(double value, int scale);
	sstring(String ^str);

	DECLDIR_BU operator const char* () const;
	//operator String^() const;
	String^ toMString() const;
	DECLDIR_BU sstring operator+ (const sstring &value) const;
	DECLDIR_BU sstring operator+ (const char* value) const;
	DECLDIR_BU sstring operator+ (const void* value) const;
	DECLDIR_BU sstring operator+ (double value) const;
	DECLDIR_BU sstring operator+ (float value) const;
	DECLDIR_BU sstring operator+ (long value) const;
	DECLDIR_BU sstring operator+ (unsigned long value) const;
	DECLDIR_BU sstring operator+ (int value) const;
	DECLDIR_BU sstring operator+ (unsigned int value) const;
	DECLDIR_BU sstring operator+ (char value) const;
	DECLDIR_BU sstring operator+ (unsigned char value) const;
	DECLDIR_BU sstring operator+ (const NativeObject& value) const;
	DECLDIR_BU sstring operator+ (const NativeObject* value) const;
};



template <typename T>
class sarray {
	int size;
	const int dims;
	const int rowlen;
	T *buffer;
public:
	sarray();
	sarray(int size);
	sarray(int dims, int rowlen, int size);
	sarray(int size, const T* itms);
	sarray(int dims, int rowlen, int size, const T* itms);
	~sarray();

	T &item(int index1, int index2);
	const T item(int index1, int index2) const;
	T &operator[](int index);
	const T operator[](int index) const;
	void Resize(int size);
	void Copy(const sarray<T> &src, int pdest, int psrc, int count=WHOLE_ARRAY);
	int Size() const;
};

template DECLDIR_BU class sarray<double>;
template DECLDIR_BU class sarray<int>;
template DECLDIR_BU class sarray<sstring>;

String ^i2(int i) ;

String ^i3(int i) ;

String ^i4(int i) ;

String^ millisToTxt(int millis) ;

String ^d(double v, int digits, bool strict=false) ;


//#endif // __Lib_Stypes__
