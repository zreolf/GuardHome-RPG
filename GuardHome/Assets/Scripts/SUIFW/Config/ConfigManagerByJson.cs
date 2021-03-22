/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  Description:基于Json配置文件的配置管理器
 *         功能：
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SUIFW
{
    public class ConfigManagerByJson : IConfigManager
    {
        //保存键值对集合
        private static Dictionary<string, string> _AppString;

        public Dictionary<string, string> AppString 
        {
            get { return _AppString; }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonPath">json配置文件路径</param>
        public ConfigManagerByJson(string jsonPath)
        {
            _AppString = new Dictionary<string, string>();
            //初始化解析Json数据，加载到集合中.
            InitAndAnalysisJson(jsonPath);
        }
        /// <summary>
        /// 得到AppSetting的最大值
        /// </summary>
        /// <returns></returns>
        public int GetAppSettingMaxNumber()
        {
            if (_AppString!=null&&_AppString.Count>=1)
            {
                return _AppString.Count;
            }
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// 初始化解析Json数据，加载到集合中
        /// </summary>
        /// <param name="jsonPath"></param>
        private void InitAndAnalysisJson(string jsonPath)
        {
            TextAsset configInfo = null;
            KetValuesInfo ketValuesInfo = null;
            try
            {
                configInfo = Resources.Load<TextAsset>(jsonPath);
                ketValuesInfo = JsonUtility.FromJson<KetValuesInfo>(configInfo.text);
            }
            catch (System.Exception)
            {

                throw new JsonAnlysisException(GetType()+ "/InitAndAnalysisJson()/jsonPath="+ jsonPath);
            }
            foreach (KeyValuesNode node in ketValuesInfo.ConfigInfo)
            {
                _AppString.Add(node.Key, node.Value);
            }
        }



    }
}
