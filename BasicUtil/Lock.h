//#ifndef __Lib_Lock__
//#define __Lib_Lock__

#pragma once

#include "dll.h"

//mutex class

DECLDIR_BU class Mutex
{
public:
    //the default constructor

    DECLDIR_BU Mutex()
    {
        InitializeCriticalSection(&m_criticalSection);
    }

    //destructor

    DECLDIR_BU ~Mutex()
    {
        DeleteCriticalSection(&m_criticalSection);
    }

    //lock

    DECLDIR_BU void lock()
    {
        EnterCriticalSection(&m_criticalSection);
    }

    //unlock

    DECLDIR_BU void unlock()
    {
        LeaveCriticalSection(&m_criticalSection);
    }

private:
    CRITICAL_SECTION m_criticalSection;
};




//synchronization controller object

DECLDIR_BU class Lock
{
public:
    //the default constructor

    DECLDIR_BU Lock(Mutex &mutex) : m_mutex(&mutex), m_locked(true)
    {
        mutex.lock();
    }

    DECLDIR_BU Lock(Mutex *mutex) : m_mutex(mutex), m_locked(mutex!=NULL)
    {
		if (mutex!=NULL) {
			mutex->lock();
		}
    }

	//the destructor

    DECLDIR_BU ~Lock()
    {
		if (m_mutex!=NULL) {
	        m_mutex->unlock();
		}
    }

    //report the state of locking when used as a boolean

    DECLDIR_BU operator bool () const
    {
        return m_locked;
    }

    //unlock

    DECLDIR_BU void setUnlock()
    {
        m_locked = false;        
    }

private:
    Mutex *m_mutex;
    bool m_locked;
};



#define synchronized(M)  for(Lock $$lock(M); $$lock; $$lock.setUnlock())

//#endif // __Lib_Lock__


