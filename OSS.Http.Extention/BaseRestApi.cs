﻿#region Copyright (C) 2016 Kevin (OSS开源系列) 公众号：osscoder

/***************************************************************************
*　　	文件功能描述：OSS.Http.Extention - 通用App接口请求基类
*
*　　	创建人： Kevin
*       创建人Email：1985088337@qq.com
*       创建时间： 2017-5-25
*       
*****************************************************************************/

#endregion

using System;
using OSS.Common.ComModels;
using OSS.Common.Plugs;

namespace OSS.Http.Extention
{
    /// <summary>
    ///   请求基类
    /// </summary>
    /// <typeparam name="RestType"></typeparam>
    public class BaseRestApi<RestType> : BaseRestApi<RestType, AppConfig>
        where RestType : class, new()
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="config"></param>
        public BaseRestApi(AppConfig config = null) : base(config)
        {

        }
    }

    /// <summary>
    /// 通用App接口请求基类
    /// </summary>
    /// <typeparam name="RestType"></typeparam>
    /// <typeparam name="TConfigType"></typeparam>
    public class BaseRestApi<RestType, TConfigType>
        where RestType : class, new()
        where TConfigType : class, new()
    {

        #region  接口配置信息

        /// <summary>
        ///   默认配置信息，如果实例中的配置为空会使用当前配置信息
        /// </summary>
        public static TConfigType DefaultConfig { get; set; }

        private readonly TConfigType _config;

        /// <summary>
        /// 微信接口配置
        /// </summary>
        public TConfigType ApiConfig => _config ?? DefaultConfig;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public BaseRestApi(TConfigType config = null)
        {
            if (config == null && DefaultConfig == null)
                throw new ArgumentNullException(nameof(config),
                    "构造函数中的config 和 全局DefaultConfig 配置信息同时为空，请通过构造函数赋值，或者在程序入口处给 DefaultConfig 赋值！");
            _config = config;
        }

        #endregion

        #region  单例实体

        private static object _lockObj = new object();

        private static RestType _instance;

        /// <summary>
        ///   接口请求实例  
        ///  当 DefaultConfig 设值之后，可以直接通过当前对象调用
        /// </summary>
        public static RestType Instance
        {
            get
            {
                if (_instance != null) return _instance;

                lock (_lockObj)
                {
                    if (_instance == null)
                        _instance = new RestType();
                }
                return _instance;
            }

        }

        #endregion

        /// <summary>
        ///   当前模块名称
        ///     方便日志追踪
        /// </summary>
        protected static string ModuleName { get; set; } = ModuleNames.Default;
    }



}
