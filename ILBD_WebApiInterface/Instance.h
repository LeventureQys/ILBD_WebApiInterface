#pragma once
//~ some rats... or mice ~
//
//    _()______
//  /_'_/   /  `\            ,
//     /'---\___/~~~~~~~~~~~~   jgs
//    ~     ~~

//Author:Leventure
//Date:2023.3.3
//info:�ṩһ��LBD_WebApiInterface��Ψһʵ��
//�����ͣ�https://www.cnblogs.com/Leventure/p/17175461.html

//���ںͻ���ƽ̨֮����н�������Htmlҳ�淢��post�����û�ִ���Դ������������Ϣ��,���ݽӿڵ�
//ע��
//info:�������ҹ���һ�����˹���~
//���������������LBD_WebApiInterface�����Եģ����õ�������ȫ��������cpp��
//
// �����ֻ���ṩ���ýӿڣ�ʵ���ϵĵ����ͱ�����Instance.h��
// 
//ĿǰΪֹ��õķ�����ѡ��ʹ��NativeAOT��Ҳ����.net 7֮��ķ�������������ʱû��ѡ���������CLR���������.NET7֮�󽥽��ؾͷ�������~
// 
//Ŀǰδʹ��NativeAOTԭ����2��
//1.����µķ�����Ҫʹ��.NET 7 �����VS 2022ʹ�ã�Ŀǰ��ʱû���������
//2.��Ϊ����Ҫ���ǵ��Ͽ�ļ��������⣬�кܶ��Ͽ�Ҳ�õ���LBD_WebAPiInterface�����⣬���Բ����ܴ���
//ֻ���������׿ǣ��Ա�֤��ǰ�Ͽⲻ��Ӱ�죬������¿��ܻ�����ü���
//
// 
//������Ϊһ��Feature��֮�������Ҫ�Ķ�������ֱ�ӷ�����.net�����ܻῼ��ֱ����NativeAOT����ת��,��Ȼ�Ӳ�������

using namespace System;
using namespace LBD_WebApiInterface;
ref class CLR
{
public:
	static LBD_WebApiInterface::Api::TeachInfoI^ Instance() {
		if (m_Instance_TeacherInfo == nullptr) {
			m_Instance_TeacherInfo = gcnew LBD_WebApiInterface::Api::TeachInfoI();
		}
		return m_Instance_TeacherInfo;
	}
private:
	static LBD_WebApiInterface::Api::TeachInfoI^ m_Instance_TeacherInfo;


};