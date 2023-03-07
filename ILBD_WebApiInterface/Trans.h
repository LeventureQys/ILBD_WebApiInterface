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

#define ENUM_MATCH(cpp_enum_type, cs_enum_type,cpp_funcname,cs_funcname) \
    inline cs_enum_type To##cs_funcname(cpp_enum_type cppValue) \
    { \
        return static_cast<cs_enum_type>(cppValue); \
    } \
    inline cpp_enum_type To##cpp_funcname(cs_enum_type csValue) \
    { \
        return static_cast<cpp_enum_type>(csValue); \
    }

//本来想的是能不能直接做成类似QAXCLASS_BEGIN那种，看了半天没想到怎么实现那种功能，还是算了，先这么做吧

//参数交换
#define TRANCPP_S(cpp_cls_name,cs_cls_name,prop_type)\
{\
cpp_cls_name.prop_type = s2s(cs_cls_name->prop_type);\
}

#define TRANCPP(cpp_cls_name,cs_cls_name,prop_type){\
cpp_cls_name.prop_type = cs_cls_name->prop_type;\
}




//
//enum class MyEnum { Value1, Value2, Value3 };
//enum class MyCSharpEnum { Value1, Value2, Value3 };
//
//ENUM_MATCH(MyEnum, MyCSharpEnum)
//
//// 转换C++枚举值为C#枚举值
//MyEnum cppValue = MyEnum::Value1;
//MyCSharpEnum csValue = ToMyCSharpEnum(cppValue);
//
//// 转换C#枚举值为C++枚举值
//MyCSharpEnum csValue2 = MyCSharpEnum::Value2;
//MyEnum cppValue2 = ToMyEnum(csValue2);