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
//���ת������ַ����������ݣ���ת���ַ���
//��֪����ή�Ϳɶ��ԣ�����ү����
#define INS(value){\
s2s(CLR::Instance()->value)\
}
//����ԭʼ���ݣ�����int longlongʲô�ģ�����û��ת����Ҫ��ת�ַ������ͣ���Ϊ.net���ַ������й���framework�ϵ�
//����ϵͳԭ��������
#define RAW(value){\
CLR::Instance()->value\
}
