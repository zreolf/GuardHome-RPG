/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  Description: 消息（传递）中心
 *         功能：
 *              1.负责UI框架中，所有UI窗体中间的数据传值
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
    public class MessageCenter
    {
        //委托：消息传递
        public delegate void DelMessageDelivery(KeyValuesUpdate kv);
        //消息中心缓存集合  string :数据的大的分类 DelMessageDelivery 数据执行委托
        public static Dictionary<string, DelMessageDelivery> _dicMesage = new Dictionary<string, DelMessageDelivery>();


        /// <summary>
        /// 添加消息的监听。
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handler">消息委托</param>
        public static void AddMsgListener(string messageType, DelMessageDelivery handler)
        {
            if (!_dicMesage.ContainsKey(messageType))
            {
                _dicMesage.Add(messageType, handler);
            }
            _dicMesage[messageType] += handler;
        }

        /// <summary>
        /// 取消消息的监听。
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="handler">消息委托</param>
        public static void RemoveMsgListener(string messageType, DelMessageDelivery handler)
        {
            if (_dicMesage.ContainsKey(messageType))
            {
                _dicMesage[messageType] -= handler;
            }
        }

        /// <summary>
        /// 取消消息的监听。
        /// </summary>
        public static void ClearAllMsgListener()
        {
            if (_dicMesage != null)
            {
                _dicMesage.Clear();
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageType">消息分类</param>
        /// <param name="kv">键值对（对象）</param>
        public static void SendMessage(string messageType, KeyValuesUpdate kv)
        {
            DelMessageDelivery del;
            if (_dicMesage.TryGetValue(messageType,out del))
            {
                if (del!=null)
                {
                    del(kv);
                }
            }
        }

    }

    /// <summary>
    /// 委托简直对
    /// </summary>
    public class KeyValuesUpdate
    {
        private string _Key;
        private object _Value;

        public string Key { get => _Key;  }
        public object Value { get => _Value;  }

        public KeyValuesUpdate(string key,object valueObj)
        {
            _Key = key;
            _Value = valueObj;

        }
    }

}
