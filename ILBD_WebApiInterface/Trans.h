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

#define ENUM_MATCH(cpp_enum_type, cs_enum_type,cpp_funcname,cs_funcname) \
    inline cs_enum_type To##cs_funcname(cpp_enum_type cppValue) \
    { \
        return static_cast<cs_enum_type>(cppValue); \
    } \
    inline cpp_enum_type To##cpp_funcname(cs_enum_type csValue) \
    { \
        return static_cast<cpp_enum_type>(csValue); \
    }

//����������ܲ���ֱ����������QAXCLASS_BEGIN���֣����˰���û�뵽��ôʵ�����ֹ��ܣ��������ˣ�����ô����

//��������
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
//// ת��C++ö��ֵΪC#ö��ֵ
//MyEnum cppValue = MyEnum::Value1;
//MyCSharpEnum csValue = ToMyCSharpEnum(cppValue);
//
//// ת��C#ö��ֵΪC++ö��ֵ
//MyCSharpEnum csValue2 = MyCSharpEnum::Value2;
//MyEnum cppValue2 = ToMyEnum(csValue2);