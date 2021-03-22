/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *                 
 *  Description:窗体类型
 *         功能：
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
    public class UIType
    {
        //是否清空“栈集合”
        public bool IsClearStack = false;
        //UI窗体（位置）类型
        public UIFormType UIForms_Type = UIFormType.Normal;
        //UI窗体显示类型
        public UIFormShoeMode UIForms_ShowMode = UIFormShoeMode.Normal;
        //UI窗体透明度类型
        public UIFormLucenyType UIForms_LucencyType = UIFormLucenyType.Lucency;
     
    }
}
