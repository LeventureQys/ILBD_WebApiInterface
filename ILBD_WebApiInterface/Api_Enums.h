#pragma once
//Author:Leventure
//Data:2023.3.6
//info:获得通用的枚举类型

#pragma region TeachInfo
	// 学科
	enum class E_Subject_cpp
	{
		None = 0,
		语文 = 1,
		数学 = 2,
		英语 = 3,
		物理 = 4,
		化学 = 5,
		生物 = 6,
		政治 = 7,
		历史 = 8,
		地理 = 9,
		科学 = 10,
		美术 = 11
	};

	//其它系统名称（从本系统接口获取，区别于云平台）
	enum class E_OtherSysName_cpp
	{
		None = 0,            //表示不指定，则应该返回所有系统的值
		基础 = 1,
		CSetup = 2,
		作文评分 = 3,
		自由学习 = 4,
		口语测试 = 5,
		资源库 = 6,
		技能训练 = 7,
		主题背景库 = 8,
		VOD = 9,
		知识点 = 10
	};

	/// <summary>
	/// 教学系统产品名称
	/// </summary>
	enum class E_TeachProductName_cpp
	{
		Default = 0,//默认为英语课堂教学系统5.2
		EnglishClassRoomTeachSystemV52 = 1,
		GeneralClassRoomTeachSystemV20 = 2,
		InternetStudySystemV10 = 3
	};
	/// <summary>
	/// 教学模块
	/// </summary>
	enum class E_Teach_Module_cpp
	{
		课堂讲解 = 1,
		课堂辅导 = 2,
		随堂综合测试 = 3,
		口语测试 = 4,
		作业 = 5,
		管理平台 = 6,
		多媒体教学 = 7
	};

	//现在多学科，教学模式有变更。但其它学科的又不能直接追加在后面，因为有相同名字的模式
	/// <summary>
	/// 教学模式
	/// </summary>
	enum class E_TeachClass_Mode_cpp
	{
		课文讲解 = 1,
		听力讲解 = 2,
		口语教学 = 3,
		阅读训练 = 4,
		随堂听力测试 = 5,
		情景会话 = 6,
		协作写作 = 7,
		课堂辅导 = 8,
		随堂测试 = 9,
		复习辅导 = 10,
		协作设计制作 = 11,
		知识竞答 = 12,
		随堂综合测试 = 13
	};

	/// <summary>
	/// 操作类型
	/// </summary>
	enum class E_Operation_cpp
	{
		随堂提问 = 1,
		听写 = 2,
		其他 = 3,
		资料搜索 = 4,
		手动加分 = 5,
		测试加分 = 6,
		跟读 = 7,
		听力测试 = 8,
		配音 = 9,
		小组讨论 = 10,

		朗读 = 11,
		口头表达 = 12,
		写作 = 13,
		影子训练 = 14,
		短期记忆 = 15,
		自主领读 = 16,
		自主复听 = 17      //自主复听也是操作类型
	};

	/// <summary>
	/// 学生获取分数类型
	/// </summary>
	enum class E_DataType_cpp
	{
		手动课堂加分 = 1,
		课堂测试加分 = 2,
		课堂训练 = 3,
		其他类型 = 4
	};

	/// <summary>
	/// 教师设置项
	/// </summary>
	enum class E_TeachSetItem
	{
		常用教材设置 = 1,
		更新体验 = 2,
		主界面定制提示 = 3,//'用于控制主界面定制提示是否显示
		本地收藏地址 = 4,//'教师设置本地收藏地址
		资料讲解全屏 = 5//xiezongwu20130711增加资料讲解是否全屏设置，默认不全屏
	};

	/// <summary>
	/// 资料来源类别
	/// </summary>
	enum class E_OriResourType_cpp
	{
		图文教材库 = 1,      //大学才会使用，因为兼容原lgzx_dir
		多媒体教程库 = 2,
		公共媒体库 = 3,
		作业库 = 4,
		情景会话库 = 5,
		水平试题库 = 6,
		知识点课件库 = 7,
		主题背景库 = 8,
		本地电脑 = 9,    //v5.0新增
		翻译库 = 10,     //新增翻译库
		电子资源库 = 11,
		数字化资源库 = 12,
		网络化课件库 = 13,
		U盘 = 14,
		专用教材 = 15,
		课前预习 = 16,
		课堂教案 = 17,
		课后练习 = 18,
		智能化课件 = 19,
		智能组卷试卷 = 20,
		随堂测试卷 = 21,
		教学方案 = 22,
		课前预习方案 = 23,
		课堂教案V2 = 24,
		课后作业方案 = 25,
		课文讲解课件 = 26,
		重难点讲解课件 = 27,
		本地资料课件集 = 28,
		智能化课件组成 = 29  //智能化课件的子模块类型例如主体原文、课前热身xiezongwu201710-13
	};

	/// <summary>
	/// 资料的结果类型
	/// </summary>
	enum class E_ResourceResultType_cpp
	{
		错误率 = 1,
		平均分 = 2
	};
#pragma endregion


