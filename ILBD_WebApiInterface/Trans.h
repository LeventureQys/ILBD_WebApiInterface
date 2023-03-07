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

#ifdef _MSC_VER
//这个宏的意思是：进行枚举类型的转换，因为枚举类型的具体类型可能会需要带上，调用示例：
//ENUM_MATCH(WebApi_Api::TeachInfo::E_TeachProductName_cpp, LBD_WebApiInterface::Api::TeachInfoI::E_TeachProductName, E_TeachProductName_cpp, E_TeachProductName)

#define ENUM_MATCH(cpp_enum_type, cs_enum_type,cpp_funcname,cs_funcname) \
    inline cs_enum_type To##cs_funcname(cpp_enum_type cppValue) \
    { \
        return static_cast<cs_enum_type>(cppValue); \
    } \
    inline cpp_enum_type To##cpp_funcname(cs_enum_type csValue) \
    { \
        return static_cast<cpp_enum_type>(csValue); \
    }
#else
#define ENUM_MATCH(cpp_enum_type, cs_enum_type) \
    inline cs_enum_type To##cs_enum_type(cpp_enum_type cppValue) \
    { \
        return static_cast<cs_enum_type>(cppValue); \
    } \
    inline cpp_enum_type To##cpp_enum_type(cs_enum_type csValue) \
    { \
        return static_cast<cpp_enum_type>(csValue); \
    }
#endif

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