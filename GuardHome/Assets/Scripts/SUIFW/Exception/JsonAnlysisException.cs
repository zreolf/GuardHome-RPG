/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  Description:Json解析异常
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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SUIFW
{
    public class JsonAnlysisException : Exception
    {
        public JsonAnlysisException() : base() { }
        public JsonAnlysisException(string exceptionMessage) : base(exceptionMessage) { }
    }
}
