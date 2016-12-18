﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ydtf.isoa
{
    /// <summary>
    /// 错误码
    /// </summary>
    public enum ec_errno
    {
        /// <summary>
        /// 重复初始化，则失败
        /// </summary>
        [member_text("重复初始化，则失败")]
        EC_REINIT = 1,

        /// <summary>
        /// 创建 SOCKET失败
        /// </summary>
        [member_text("创建 SOCKET失败")]
        EC_SOCK = 2,

        /// <summary>
        /// 连接失败
        /// </summary>
        [member_text("连接失败")]
        EC_CONNECT = 3,

        /// <summary>
        /// 超时
        /// </summary>
        [member_text("超时")]
        EC_TIMEOUT = 4,

        /// <summary>
        /// 发送数据失败
        /// </summary>
        [member_text("发送数据失败")]
        EC_SEND = 5,

        /// <summary>
        /// 接收数据失败
        /// </summary>
        [member_text("接收数据失败")]
        EC_RECV = 6,

        /// <summary>
        /// 设置 SOL 选项失败
        /// </summary>
        [member_text("设置 SOL 选项失败")]
        EC_SETSOL = 7,

        /// <summary>
        /// 组织请求数据包失败
        /// </summary>
        [member_text("组织请求数据包失败")]
        EC_PACK = 8,

        /// <summary>
        /// 返回数据解包失败
        /// </summary>
        [member_text("返回数据解包失败")]
        EC_UNPACK = 9,

        /// <summary>
        /// 动态申请内存资源失败
        /// </summary>
        [member_text("动态申请内存资源失败")]
        EC_ALLOC = 10,

        /// <summary>
        /// Url格式不合法
        /// </summary>
        [member_text("Url格式不合法")]
        EC_URL = 11,

        /// <summary>
        /// ip地址格式不合法
        /// </summary>
        [member_text("ip地址格式不合法")]
        EC_HOST = 12,

        /// <summary>
        /// 端口不合法 
        /// </summary>
        [member_text("端口不合法")]
        EC_PORT = 13,

        /// <summary>
        /// 传入的参数不合法
        /// </summary>
        [member_text("传入的参数不合法")]
        EC_PARAM = 14,

        /// <summary>
        /// 没有初始化过 
        /// </summary>
        [member_text("没有初始化过")]
        EC_NOINIT = 15,

        /// <summary>
        /// ISOA服务返回错误信息
        /// </summary>
        [member_text("ISOA服务返回错误信息")]
        EC_PEER = 16,
    }
}
