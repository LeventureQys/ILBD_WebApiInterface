#pragma once
//Author:Leventure
//Data:2023.3.6
//info:���ͨ�õ�ö������
namespace WebApi_Api{
	ENUM_MATCH(WebApi_Api::E_TeachProductName_cpp, LBD_WebApiInterface::Api::TeachInfoI::E_TeachProductName, E_TeachProductName_cpp, E_TeachProductName)
		ENUM_MATCH(WebApi_Api::E_Subject_cpp, LBD_WebApiInterface::Api::TeachInfoI::E_Subject, E_Subject_cpp, E_Subject)
#pragma region TeachInfo
	// ѧ��
	enum class E_Subject_cpp
	{
		None = 0,
		���� = 1,
		��ѧ = 2,
		Ӣ�� = 3,
		���� = 4,
		��ѧ = 5,
		���� = 6,
		���� = 7,
		��ʷ = 8,
		���� = 9,
		��ѧ = 10,
		���� = 11
	};

	//����ϵͳ���ƣ��ӱ�ϵͳ�ӿڻ�ȡ����������ƽ̨��
	enum class E_OtherSysName_cpp
	{
		None = 0,            //��ʾ��ָ������Ӧ�÷�������ϵͳ��ֵ
		���� = 1,
		CSetup = 2,
		�������� = 3,
		����ѧϰ = 4,
		������� = 5,
		��Դ�� = 6,
		����ѵ�� = 7,
		���ⱳ���� = 8,
		VOD = 9,
		֪ʶ�� = 10
	};

	/// <summary>
	/// ��ѧϵͳ��Ʒ����
	/// </summary>
	enum class E_TeachProductName_cpp
	{
		Default = 0,//Ĭ��ΪӢ����ý�ѧϵͳ5.2
		EnglishClassRoomTeachSystemV52 = 1,
		GeneralClassRoomTeachSystemV20 = 2,
		InternetStudySystemV10 = 3
	};
	/// <summary>
	/// ��ѧģ��
	/// </summary>
	enum class E_Teach_Module_cpp
	{
		���ý��� = 1,
		���ø��� = 2,
		�����ۺϲ��� = 3,
		������� = 4,
		��ҵ = 5,
		����ƽ̨ = 6,
		��ý���ѧ = 7
	};

	//���ڶ�ѧ�ƣ���ѧģʽ�б����������ѧ�Ƶ��ֲ���ֱ��׷���ں��棬��Ϊ����ͬ���ֵ�ģʽ
	/// <summary>
	/// ��ѧģʽ
	/// </summary>
	enum class E_TeachClass_Mode_cpp
	{
		���Ľ��� = 1,
		�������� = 2,
		�����ѧ = 3,
		�Ķ�ѵ�� = 4,
		������������ = 5,
		�龰�Ự = 6,
		Э��д�� = 7,
		���ø��� = 8,
		���ò��� = 9,
		��ϰ���� = 10,
		Э��������� = 11,
		֪ʶ���� = 12,
		�����ۺϲ��� = 13
	};

	/// <summary>
	/// ��������
	/// </summary>
	enum class E_Operation_cpp
	{
		�������� = 1,
		��д = 2,
		���� = 3,
		�������� = 4,
		�ֶ��ӷ� = 5,
		���Լӷ� = 6,
		���� = 7,
		�������� = 8,
		���� = 9,
		С������ = 10,

		�ʶ� = 11,
		��ͷ���� = 12,
		д�� = 13,
		Ӱ��ѵ�� = 14,
		���ڼ��� = 15,
		������� = 16,
		�������� = 17      //��������Ҳ�ǲ�������
	};

	/// <summary>
	/// ѧ����ȡ��������
	/// </summary>
	enum class E_DataType_cpp
	{
		�ֶ����üӷ� = 1,
		���ò��Լӷ� = 2,
		����ѵ�� = 3,
		�������� = 4
	};

	/// <summary>
	/// ��ʦ������
	/// </summary>
	enum class E_TeachSetItem
	{
		���ý̲����� = 1,
		�������� = 2,
		�����涨����ʾ = 3,//'���ڿ��������涨����ʾ�Ƿ���ʾ
		�����ղص�ַ = 4,//'��ʦ���ñ����ղص�ַ
		���Ͻ���ȫ�� = 5//xiezongwu20130711�������Ͻ����Ƿ�ȫ�����ã�Ĭ�ϲ�ȫ��
	};

	/// <summary>
	/// ������Դ���
	/// </summary>
	enum class E_OriResourType_cpp
	{
		ͼ�Ľ̲Ŀ� = 1,      //��ѧ�Ż�ʹ�ã���Ϊ����ԭlgzx_dir
		��ý��̳̿� = 2,
		����ý��� = 3,
		��ҵ�� = 4,
		�龰�Ự�� = 5,
		ˮƽ����� = 6,
		֪ʶ��μ��� = 7,
		���ⱳ���� = 8,
		���ص��� = 9,    //v5.0����
		����� = 10,     //���������
		������Դ�� = 11,
		���ֻ���Դ�� = 12,
		���绯�μ��� = 13,
		U�� = 14,
		ר�ý̲� = 15,
		��ǰԤϰ = 16,
		���ý̰� = 17,
		�κ���ϰ = 18,
		���ܻ��μ� = 19,
		��������Ծ� = 20,
		���ò��Ծ� = 21,
		��ѧ���� = 22,
		��ǰԤϰ���� = 23,
		���ý̰�V2 = 24,
		�κ���ҵ���� = 25,
		���Ľ���μ� = 26,
		���ѵ㽲��μ� = 27,
		�������Ͽμ��� = 28,
		���ܻ��μ���� = 29  //���ܻ��μ�����ģ��������������ԭ�ġ���ǰ����xiezongwu201710-13
	};

	/// <summary>
	/// ���ϵĽ������
	/// </summary>
	enum class E_ResourceResultType_cpp
	{
		������ = 1,
		ƽ���� = 2
	};
#pragma endregion

}