/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  Description:
 *         功能：
 *         
 *  Author:周立军
 *  
 *  Data:2020-05-01
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
    public class StartProject : MonoBehaviour
    {
        void Start()
       {
            //加载登陆窗体
            UIManager.GetInstance().ShowUIForms("LogonUIForm");
        }
    }
}
