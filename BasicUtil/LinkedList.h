#pragma once
#include <map>

template <typename T> class ZsfLinkedList;

template <typename T> class ZsfLinkedListItm : NativeObject {
	friend class ZsfLinkedList<T>;

	ZsfLinkedListItm<T> *next;

	T *value;

	void Subst(ZsfLinkedListItm<T> *item);

public:
	
	ZsfLinkedListItm(T *value);

	~ZsfLinkedListItm();

	const ZsfLinkedListItm<T> *Next() const;

	T *Value() const;

};

template <typename T> class ZsfLinkedList : NativeObject {

	int size;

	ZsfLinkedListItm<T> *first;

	ZsfLinkedListItm<T> *last;

public:

	ZsfLinkedList();
	~ZsfLinkedList();

	int Size() const;

	const ZsfLinkedListItm<T> *First() const;

	const ZsfLinkedListItm<T> *Last() const;

	const ZsfLinkedListItm<T> *Node(int index) const;

	const ZsfLinkedListItm<T> *AddFirst(T *value);

	const ZsfLinkedListItm<T> *InsertAfter(const ZsfLinkedListItm<T> *item, T *value);

	const ZsfLinkedListItm<T> *Add(int index, T *value);

	const ZsfLinkedListItm<T> *Remove(int index);

	void Subst(const ZsfLinkedListItm<T> *item1, const ZsfLinkedListItm<T> *item2);

	T *Get(int index);
};

