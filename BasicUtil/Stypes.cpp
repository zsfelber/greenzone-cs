#include "StdAfx.h"
#include "Stypes.h"
#include <sstream>

using namespace System::Runtime::InteropServices;

NativeObject::NativeObject(void)
{
}


NativeObject::~NativeObject(void)
{
}

sstring NativeObject::ToString() const {
	int addr = (int) (void*) this;
	sstring s="@";
	s = s + addr;
	return s;
}



sstring::sstring() {
}

sstring::sstring(const char* buf) : std::string(buf){
}

sstring::sstring(const std::string& str) : std::string(str) {
}

sstring::sstring(const sstring& str) : std::string(str) {
}

sstring::sstring(double value, int scale) {
	std::stringstream ss;
	ss.precision(scale);
	ss<<value;
	this->sstring::sstring(ss.str());
}

sstring::sstring(String ^str) {
    // Marshal the managed string to unmanaged memory.
    char* stringPointer = (char*) Marshal::StringToHGlobalAnsi(str).ToPointer();

	this->sstring::sstring(stringPointer);

	// Always free the unmanaged string.
    Marshal::FreeHGlobal(IntPtr(stringPointer));
}

sstring::operator const char* () const {
	return c_str();
}

//sstring::operator String^ () const {
//	return gcnew String(c_str());
//}
String^ sstring::toMString() const {
	return gcnew String(c_str());
}


sstring sstring::operator+ (const sstring &value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (const char* value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (const void* value) const {
	int addr = (int) value;
	std::stringstream ss;
	ss<<*this;
	ss<<"@";
	ss<<addr;
	return ss.str();
}

sstring sstring::operator+ (double value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (float value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (long value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (unsigned long value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (int value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (unsigned int value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (char value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (unsigned char value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value;
	return ss.str();
}

sstring sstring::operator+ (const NativeObject& value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value.ToString();
	return ss.str();
}

sstring sstring::operator+ (const NativeObject* value) const {
	std::stringstream ss;
	ss<<*this;
	ss<<value->ToString();
	return ss.str();
}



template <typename T>
sarray<T>::sarray() :
	dims(1), rowlen(1), size(10)
{
	buffer = new T[size*rowlen];
}

template <typename T>
sarray<T>::sarray(int _size) :
	dims(1), rowlen(1), size(_size)
{
	buffer = new T[size*rowlen];
}

template <typename T>
sarray<T>::sarray(int _dims, int _rowlen, int _size) :
	dims(_dims), rowlen(_rowlen), size(_size)
{
	buffer = new T[size*rowlen];
}

template <typename T>
sarray<T>::sarray(int _size, const T* itms) :
	dims(1), rowlen(1), size(_size)
{
	buffer = new T[size*rowlen];
	memcpy(buffer,itms,size*rowlen*sizeof(T));
}

template <typename T>
sarray<T>::sarray(int _dims, int _rowlen, int _size, const T* itms) :
	dims(_dims), rowlen(_rowlen), size(_size)
{
	buffer = new T[size*rowlen];
	memcpy(buffer,itms,size*rowlen*sizeof(T));
}


template <typename T>
sarray<T>::~sarray() {
	delete[] buffer;
}




template <typename T>
T &sarray<T>::item(int index1, int index2) {
	int index=index1*rowlen+index2;
	return (*this)[index];
}

template <typename T>
const T sarray<T>::item(int index1, int index2) const {
	int index=index1*rowlen+index2;
	return (*this)[index];
}

template <typename T>
T &sarray<T>::operator[](int index) {
	if (index>=0 && index<size*rowlen) {
		return buffer[index];
	} else {
		String ^s="";
		throw gcnew Exception(s+"Array index out of bounds. index:"+index+" size:"+size+" rowlen:"+rowlen);
	}
}

template <typename T>
const T sarray<T>::operator[](int index) const {
	if (index>=0 && index<size*rowlen) {
		return buffer[index];
	} else {
		String ^s="";
		throw gcnew Exception(s+"Array index out of bounds. index:"+index+" size:"+size+" rowlen:"+rowlen);
	}
}

template <typename T>
void sarray<T>::Resize(int _size) {
	if (_size<=size) {
		size = _size;
	} else {
		T *_buffer = new T[_size];
		memcpy(_buffer,buffer,size*rowlen*sizeof(T));
		delete[] buffer;
		size = _size;
		buffer = _buffer;
	}
}

template <typename T>
void sarray<T>::Copy(const sarray<T> &src, int pdest, int psrc, int count) {
	String ^s="";
	if (count<0) {
		throw gcnew Exception(s+"Invalid arg  count:"+count);
	}
	if (count==WHOLE_ARRAY) {
		count = src.size-psrc;
	}
	if (src.size*src.rowlen<psrc+count) {
		throw gcnew Exception(s+"Invalid args  psrc:"+psrc+" count:"+count+" src.size:"+src.size+" src.rowlen:"+src.rowlen+" (src.dims:"+src.dims+")");
	}
	if (psrc+count>size*rowlen) {
		Resize((int) ceil((0.0+psrc+count)/rowlen));
	}
	memcpy(buffer+pdest,src.buffer+psrc,count*sizeof(T));
}

template <typename T>
int sarray<T>::Size() const {
	return size;
}




String ^i2(int i) {
   String ^s="";
   if (i < 10) {
      return (s+"0"+i);
   } else {
      return (s+i);
   }
}

String ^i3(int i) {
   String ^s="";
   if (i < 10) {
      return (s+"00"+i);
   } else if (i < 100) {
      return (s+"0"+i);
   } else {
      return (s+i);
   }
}

String ^i4(int i) {
   String ^s="";
   if (i < 10) {
      return (s+"000"+i);
   } else if (i < 100) {
      return (s+"00"+i);
   } else if (i < 1000) {
      return (s+"0"+i);
   } else {
      return (s+i);
   }
}

String^ millisToTxt(int millis) {
   int _milli = millis%1000;
   millis /= 1000;
   int _sec = millis%60;
   millis /= 60;
   int _min = millis%60;
   millis /= 60;
   int _hour = millis%60;
   millis /= 60;
   int _day = millis%24;
   millis /= 24;
   String ^s="";
   if (_day != 0) {
      s = s+_day+"d "+i2(_hour)+":"+i2(_min);
   } else if (_hour != 0){
      s = s+i2(_hour)+":"+i2(_min);
	  if (_sec) {
		s = s+":"+i2(_sec);
	  }
   } else if (_min != 0){
      s = s+"00:"+i2(_min);
	  if (_sec||_milli) {
		  s = s+":"+i2(_sec);
	  }
	  if (_milli) {
		s = s+" "+i3(_milli)+"ms";
	  }
   } else if (_sec != 0) {
      s = s+_sec+"s "+_milli+"ms";
   } else {
      s = s+_milli+" millis";
   }
   return s;
}

String ^d(double v, int digits, bool strict) {
	String ^format = "{0:0.";
	if (strict) {
		for (int i=0; i<digits; i++) {
			format = format + "0";
		}
	} else {
		for (int i=0; i<digits; i++) {
			format = format + "#";
		}
	}
	format = format + "}";
	return String::Format(format, v);
}
