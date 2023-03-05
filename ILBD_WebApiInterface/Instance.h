#pragma once
//~ some rats... or mice ~
//
//    _()______
//  /_'_/   /  `\            ,
//     /'---\___/~~~~~~~~~~~~   jgs
//    ~     ~~

//Author:Leventure
//Date:2023.3.3
//info:提供一个LBD_WebApiInterface的唯一实例
//见博客：https://www.cnblogs.com/Leventure/p/17175461.html

//用于和基础平台之间进行交互，向Html页面发送post请求获得回执，以此来获得锁控信息等,根据接口的
//注：
//info:今天是我工作一整年了哈哈~
//这个类是用来导出LBD_WebApiInterface的属性的，能用到的属性全导出来给cpp用
//
// 这个类只是提供调用接口，实际上的单例就保存在Instance.h内
//
//目前为止最好的方案是选择使用NativeAOT，也就是.net 7之后的方案，但是我暂时没能选用这个方案CLR这个方案在.NET7之后渐渐地就废弃掉了~
//
//目前未使用NativeAOT原因有2：
//1.这个新的方案需要使用.NET 7 且配合VS 2022使用，目前暂时没有这个条件
//2.因为还需要考虑到老库的兼容性问题，有很多老库也用到了LBD_WebAPiInterface这个类库，所以不可能大动它
//只能在外面套壳，以保证当前老库不受影响，这个更新可能会持续好几年
//
//
//Feature，之后如果需要改动，或者直接废弃掉.net，可能会考虑直接用NativeAOT进行转化,当然加不加随你

//单例的名称请保持命名规范，虽然在具体的接口上应该是用宏去覆盖，但还是建议保持一定的命名规范
using namespace System;
using namespace LBD_WebApiInterface;
ref class CLR
{
public:
	static LBD_WebApiInterface::Api::TeachInfoI^ TeachInfo_Instance() {
		if (m_Instance_TeacherInfo == nullptr) {
			m_Instance_TeacherInfo = gcnew LBD_WebApiInterface::Api::TeachInfoI();
		}
		return m_Instance_TeacherInfo;
	}
private:
	static LBD_WebApiInterface::Api::TeachInfoI^ m_Instance_TeacherInfo;
};