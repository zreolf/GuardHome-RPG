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
    public class LogonUIForm : BaseUIForm
    {

        public Button uiButton;
        void Start()
       {
           uiButton.onClick.AddListener(() =>
           {
               UIManager.GetInstance().ShowUIForms("MainCityUIForm");
           });
        }
    }
}
