#pragma once
//Author:Leventure
//date:2023.3.3
//info:һ��ת���꣬����д�����ľ�������ˣ�Ȼ�����ܶ��඼��Ҫ���ת���������ͷ�������
//����� �Ƚϼ򵥣�ֻ���滻�����ؼ��֣�����������ά��������ôд��

#include <msclr/gcroot.h> //gcroot
#include <msclr\marshal_cppstd.h>
#include "Instance.h"
#include <string>
using namespace std;
using namespace System;
using namespace msclr::interop;
using namespace LBD_WebApiInterface;
//�����ֻ��δ��������д�ҵ��������ѣ���ʵû�кܴ���,ֻ��Ϊ�˱����ʱ���滻һ��

string s2s(String^ Insert) {
	return marshal_as<string>(Insert);
}

String^ s2s(string Insert) {
	return marshal_as<String^>(Insert);
}

#ifdef _MSC_VER
//��������˼�ǣ�����ö�����͵�ת������Ϊö�����͵ľ������Ϳ��ܻ���Ҫ���ϣ�����ʾ����
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
//// ת��C++ö��ֵΪC#ö��ֵ
//MyEnum cppValue = MyEnum::Value1;
//MyCSharpEnum csValue = ToMyCSharpEnum(cppValue);
//
//// ת��C#ö��ֵΪC++ö��ֵ
//MyCSharpEnum csValue2 = MyCSharpEnum::Value2;
//MyEnum cppValue2 = ToMyEnum(csValue2);