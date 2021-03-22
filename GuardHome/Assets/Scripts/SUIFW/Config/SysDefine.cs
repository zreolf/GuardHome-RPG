/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  Description:
 *         功能：
 *         1.系统常量
 *         2.全局性方法
 *         3.系统枚举类型
 *         4.委托
 *         
 *  Author:周立军
 *  
 *  Data:2020-04-30
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
    #region  枚举类型
    /// <summary>
    /// ui窗体 （位置）类型
    /// </summary>
    public enum UIFormType
    {
        /// <summary>
        /// 普通窗体
        /// </summary>
        Normal,
        /// <summary>
        /// 固定窗体
        /// </summary>
        Fixed,
        /// <summary>
        /// 弹出窗体
        /// </summary>
        PopUp,
    }
    /// <summary>
    /// UI窗体的显示类型
    /// </summary>
    public enum UIFormShoeMode
    {
        /// <summary>
        /// 普通
        /// </summary>
        Normal,
        /// <summary>
        /// 反向切换
        /// </summary>
        ReversChange,
        /// <summary>
        /// 隐藏其他
        /// </summary>
        HideOther,
    }
    /// <summary>
    /// UI窗体透明度类型
    /// </summary>
    public enum UIFormLucenyType
    {
        /// <summary>
        /// 全透明，不能穿透
        /// </summary>
        Lucency,
        /// <summary>
        /// 半透明，不能穿透
        /// </summary>
        Translucence,
        /// <summary>
        /// 低透明，不能穿透
        /// </summary>
        ImPenetrable,
        /// <summary>
        /// 可以穿透
        /// </summary>
        Pentrate

    }
    #endregion
    public class SysDefine : MonoBehaviour
    {
        /*路径常量*/
        public const string SYS_PATH_CANVAS = "Canvas";
        public const string SYS_PATH_UIFORMS_CONFFIG_INFO = "ConfigInfos\\UIFormConfigInfo";

        /*标签常量*/
        public const string SYS_TAG_CANVAS = "_TagCanvas";//主画布标签

        /*节点常量*/
        public const string SYS_NORMAL_NODE = "Normal";
        public const string SYS_FIXED_NODE = "Fixed";
        public const string SYS_POPUP_NODE = "PopUp";
        public const string SYS_SCRIPTMANAGER_NODE = "_ScriprMgr";

        /*全局性方法*/

        /*委托的定义*/

    }
}
