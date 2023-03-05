using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using helper = LG.IntelligentCourse.WebService.Common.Helper;
using LG.IntelligentCourse.WebService.Model;
using LG.IntelligentCourse.WebService.Contract.Basic;
using System.ServiceModel;

//添加类库以下类库的引用
//System.ServiceModel
//LG.IntelligentCourse.WebService.Model
//LG.IntelligentCourse.WebService.Common.Model
//LG.IntelligentCourse.WebService.Contract.Basic

namespace LBD_WebApiInterface.Other
{
    /// <summary>
    /// 智能化课件库服务代理
    /// </summary>
    public class CoursewareNoAuthBasicSvcProxy : ICoursewareNoAuthBasic
    {
        private static readonly CoursewareNoAuthBasicSvcProxy _instance = new CoursewareNoAuthBasicSvcProxy();
        /// <summary>
        /// 获取单例
        /// </summary>
        public static CoursewareNoAuthBasicSvcProxy Instance
        {
            get { return _instance; }
        }

        protected CoursewareNoAuthBasicSvcProxy() { }

        /// <summary>
        /// 加密掩码
        /// </summary>
        /// <param name="mask">掩码：系统ID+"|"+系统ID</param>
        /// <returns>加密后的掩码</returns>
        protected string EncryptMask(string mask)
        {
            //rsa加密使用的公钥
            const string rsaPublicKey = "<RSAKeyValue><Modulus>nmeNO1CJb433iiiT/vcvpRwY6cFCDL4vTKWVzErkqi9zfMf8piUpbeU+AUfDFjdLl5GsKUDuk6BaPDTuAU+SAw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
            return helper.RSACryption.RSAEncrypt(rsaPublicKey, mask);
        }

        //服务的地址
        private string _address;
        //设置服务地址
        public void SetAddress(string address)
        {
            _address = address;
        }

        private BasicHttpBinding _basicHttpBinding = new BasicHttpBinding();
        /// <summary>
        /// 创建通道工厂
        /// </summary>
        /// <returns></returns>
        protected virtual ChannelFactory<ICoursewareNoAuthBasic> CreateChannelFactory()
        {
            return new ChannelFactory<ICoursewareNoAuthBasic>(_basicHttpBinding, new EndpointAddress(_address));
        }

        #region ICoursewareNoAuthBasic 成员
        /// <summary>
        /// 批量删除课件
        /// </summary>
        /// <param name="mask">掩码：系统ID+"|"+系统ID</param>
        /// <param name="userId">登录用户的id</param>
        /// <param name="ids">待删除的课件Id数组</param>
        /// <returns>已成功被删除的课件Id数组</returns>
        public Guid[] DeleteCoursewares(string mask, string userId, Guid[] ids)
        {
            using (var factory = CreateChannelFactory())
            {
                try
                {
                    //创建通道
                    ICoursewareNoAuthBasic client = factory.CreateChannel();
                    return client.DeleteCoursewares(EncryptMask(mask), userId, ids);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    factory.Close();
                }
            }
        }

        /// <summary>
        /// 根据课件的Id获取对应的课件信息
        /// </summary>
        /// <param name="mask">掩码：系统ID+"|"+系统ID</param>
        /// <param name="teacherId">登录用户的id</param>
        /// <param name="id">课件的Id</param>
        /// <returns>课件信息,不存在或该教师没权限查看的返回null</returns>
        public lgdt_courseware GetCoursewareById(string mask, string userId, Guid id)
        {
            using (var factory = CreateChannelFactory())
            {
                try
                {
                    //创建通道
                    ICoursewareNoAuthBasic client = factory.CreateChannel();
                    return client.GetCoursewareById(EncryptMask(mask), userId, id);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    factory.Close();
                }
            }
        }

        /// <summary>
        /// 批量开启或关闭共享课件
        /// </summary>
        /// <param name="mask">掩码：系统ID+"|"+系统ID</param>
        /// <param name="teacherId">登录用户的id</param>
        /// <param name="ids">待操作的课件id数组</param>
        /// <param name="isOn">true:开启共享 false:关闭共享</param>
        /// <returns>true:代表已经成功开启共享 false:代表已经成功关闭共享</returns>
        public bool SetCoursewareShareFlag(string mask, string teacherId, Guid[] ids, bool isOn)
        {
            using (var factory = CreateChannelFactory())
            {
                try
                {
                    //创建通道
                    ICoursewareNoAuthBasic client = factory.CreateChannel();
                    return client.SetCoursewareShareFlag(EncryptMask(mask), teacherId, ids, isOn);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    factory.Close();
                }
            }
        }

        /// <summary>
        /// 根据条件查询课件
        /// </summary>
        /// <param name="mask">掩码用于验证是否为合法调用，调用系统Id+"|"+经过RSA加密后的调用系统Id</param>
        /// <param name="teacherId">创建课件的教师Id</param>
        /// <param name="coursewareType">课件类型</param>
        /// <param name="term">学期（可选）</param>
        /// <param name="keyword">搜索的关键字（课件的的关键词或课件的名称）（可选）</param>
        /// <param name="sortField">排序的字段"CreateTime"或"ModifiedTime"（可选,默认用"CreateTime"）</param>
        /// <param name="sortType">排序的规则，降序或升序（默认用降序） </param>
        /// <param name="pageIndex">查询的页码（从1开始）</param>
        /// <param name="pageSize">每页最多返回的记录数量（上限为50)</param>
        /// <param name="totalCount">库中符合条件总的记录数量</param>
        /// <returns>课件列表</returns>
        public IList<LG.IntelligentCourse.WebService.Model.lgdt_courseware> SearchCourseware(string mask, string teacherId, string coursewareType, string term, string keyword, string sortField, LG.IntelligentCourse.WebService.Common.Model.CommonEnum.SortType sortType, int pageIndex, int pageSize, out int totalCount)
        {
            using (var factory = CreateChannelFactory())
            {
                try
                {
                    //创建通道
                    ICoursewareNoAuthBasic client = factory.CreateChannel();
                    return client.SearchCourseware(EncryptMask(mask), teacherId, coursewareType, term, keyword, sortField, sortType, pageIndex, pageSize, out totalCount);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    factory.Close();
                }
            }
        }

        /// <summary>
        /// 获取系统配置信息
        /// </summary>
        /// <param name="mask">掩码：系统ID+"|"+系统ID</param>
        /// <returns>系统配置信息</returns>
        public IDictionary<string, string> GetSysConfigs(string mask)
        {
            using (var factory = CreateChannelFactory())
            {
                try
                {
                    //创建通道
                    ICoursewareNoAuthBasic client = factory.CreateChannel();
                    return client.GetSysConfigs(EncryptMask(mask));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    factory.Close();
                }
            }
        }

        #endregion
    }
}
