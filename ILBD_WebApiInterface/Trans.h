#pragma once
//Author:Leventure
//date:2023.3.3
//info:一个转换宏，懒得写单例的具体对象了，然后反正很多类都需要这个转换方法，就放在这了
//这个宏 比较简单，只是替换几个关键字，反正都是我维护，就这么写了

#include <msclr/gcroot.h> //gcroot
#include <msclr\marshal_cppstd.h>
#include "Instance.h"
#include <string>
using namespace std;
using namespace System;
using namespace msclr::interop;
using namespace LBD_WebApiInterface;
//这个宏只是未来代替我写找到单例而已，其实没有很大含义,只是为了编译的时候替换一下


string s2s(String^ Insert) {
	return marshal_as<string>(Insert);
}

String^ s2s(string Insert) {
	return marshal_as<String^>(Insert);
}
//获得转换后的字符串类型数据，仅转换字符串
//我知道这会降低可读性，但是爷乐意
#define INS(value){\
s2s(CLR::Instance()->value)\
}
//返回原始数据，比如int longlong什么的，这种没得转，主要是转字符串类型，因为.net的字符串是托管在framework上的
//不是系统原生的类型
#define RAW(value){\
CLR::Instance()->value\
}
