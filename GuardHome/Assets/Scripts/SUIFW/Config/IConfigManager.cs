/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  Description:通用配置管理器接口
 *         功能：
 *             1：基于“键值对”配置文件通用解析
 *         
 *  Author:周立军
 *  
 *  Data:2020-05-08
 *  
 *  Modify:
 *  
 *  
 *  
 */
using System;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace SUIFW
{
    public interface IConfigManager
    {
        /// <summary>
        /// 只读属性：应用设置
        /// 功能：得到键值对集合数据
        /// </summary>
        Dictionary<string, string> AppString { get; }

        /// <summary>
        /// 得到配置文件（AppSeting）最大数量
        /// </summary>
        /// <returns></returns>
        int GetAppSettingMaxNumber();


    }
    [Serializable]
    internal  class KetValuesInfo
    {
        public List<KeyValuesNode> ConfigInfo = null;
    }
    [Serializable]
    internal class KeyValuesNode
    {
        public string Key = null;
        public string Value = null;
    }
}
