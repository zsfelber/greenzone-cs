#include "StdAfx.h"
#include "LinkedList.h"


template <typename T> ZsfLinkedListItm<T>::ZsfLinkedListItm(T *_value) :
	next(NULL), value(_value)
{
}

template <typename T> ZsfLinkedListItm<T>::~ZsfLinkedListItm() {
}

template <typename T> const ZsfLinkedListItm<T> *ZsfLinkedListItm<T>::Next() const {
	return next;
}

template <typename T> T *ZsfLinkedListItm<T>::Value() const {
	return value;
}

template <typename T> void ZsfLinkedListItm<T>::Subst(ZsfLinkedListItm<T> *item) {
	T *v = value;
	value = item->value;
	item->value = v;
}

template <typename T> ZsfLinkedList<T>::ZsfLinkedList() :
	Size(0), First(NULL), Last(NULL)
{
}


template <typename T> ZsfLinkedList<T>::~ZsfLinkedList() {
}

template <typename T> int ZsfLinkedList<T>::Size() const {
	return size;
}

template <typename T> const ZsfLinkedListItm<T> *ZsfLinkedList<T>::First() const {
	return first;
}

template <typename T> const ZsfLinkedListItm<T> *ZsfLinkedList<T>::Last() const {
	return last;
}

template <typename T> const ZsfLinkedListItm<T> *ZsfLinkedList<T>::Node(int index) const {
	ZsfLinkedListItm<T> *itm = first;
	for (int i=0; i<index; i++) {
		itm = itm->next;
	}
	return itm;
}

template <typename T> const ZsfLinkedListItm<T> *ZsfLinkedList<T>::AddFirst(T *value) {
	ZsfLinkedListItm<T> *newitem = new ZsfLinkedListItm<T>(value);
	if (first) {
		newitem->next = first;
	} else {
		last = newitem;
	}
	first = newitem;
	size++;
	return newitem;
}

template <typename T> const ZsfLinkedListItm<T> *ZsfLinkedList<T>::InsertAfter(const ZsfLinkedListItm<T> *_item, T *value) {
	ZsfLinkedListItm<T> *item = _item;
	ZsfLinkedListItm<T> *newitem = new ZsfLinkedListItm<T>(value);
	if (item->next) {
		newitem->next = item->next;
	} else {
		last = newitem;
	}
	item->next = newitem;
	size++;
	return newitem;
}

template <typename T> const ZsfLinkedListItm<T> *ZsfLinkedList<T>::Add(int index, T *value) {
	if (index) {
		const ZsfLinkedListItm<T> *item = Node(index-1);
		return InsertAfter(item, value);
	} else {
		return AddFirst(value);
	}
}

template <typename T> const ZsfLinkedListItm<T> *ZsfLinkedList<T>::Remove(int index) {
	const ZsfLinkedListItm<T> *previtem;
	const ZsfLinkedListItm<T> *item;
	if (index) {
		previtem = Node(index-1);
		item = previtem->next;
		previtem->next = item->next;
	} else {
		item = first;
		first = first->next;
	}
	if (item == last) {
		last = NULL;
	}
	size--;
	return item;
}

template <typename T> void ZsfLinkedList<T>::Subst(const ZsfLinkedListItm<T> *_item1, const ZsfLinkedListItm<T> *_item2) {
	ZsfLinkedListItm<T> *item1 = _item1;
	ZsfLinkedListItm<T> *item2 = _item2;
	item1->Subst(item2);
}

template <typename T> T *ZsfLinkedList<T>::Get(int index) {
	const ZsfLinkedListItm<T> *item = Node(index);
	return item->value;
}
