/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  
 *  Description:
 *         功能：定义所有UI窗体的父类
 *         定义四个生命周期：
 *         1.Display 显示状态
 *         2.Hiding 隐藏状态
 *         3.ReDisplay 在显示状态
 *         4.Freeze 冻结状态
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
    public class BaseUIForm : MonoBehaviour
    {

        /*字段*/
        private UIType _CurrentUIType = new UIType();

        /*属性*/
        public UIType CurrentUIType { get => _CurrentUIType; set => _CurrentUIType = value; }

        #region 窗体的四种状态
        /// <summary>
        /// 显示状态
        /// </summary>
        public virtual void Display()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用（必须是弹出窗体）
            if (_CurrentUIType.UIForms_Type==UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject,_CurrentUIType.UIForms_LucencyType);
            }
        }
        /// <summary>
        /// 隐藏状态
        /// </summary>
        public virtual void Hiding()
        {
            this.gameObject.SetActive(false);
            //取消模态窗体调用（必须是弹出窗体）
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().CancelMaskWindow();
            }
        }
        /// <summary>
        /// 重新显示状态
        /// </summary>
        public virtual void Redisplay()
        {
            this.gameObject.SetActive(true);
            //设置模态窗体调用（必须是弹出窗体）
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.GetInstance().SetMaskWindow(this.gameObject, _CurrentUIType.UIForms_LucencyType);
            }
        }
        /// <summary>
        /// 冻结状态
        /// </summary>
        public virtual void Freeze()
        {
            this.gameObject.SetActive(true);
        }
        #endregion

        #region 封装子类常用方法
        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="buttonName">按钮节点名称</param>
        /// <param name="delHandle">委托需要注册的方法</param>
        protected void RigisterButtonObjectEvent(string buttonName,EventTriggerListener.VoidDelegate delHandle)
        {
            //查找按钮
            GameObject goButton = UnityHelp.FindTheChildNode(this.gameObject, buttonName).gameObject;
            //给按钮注册事件方法
            if (goButton != null)
            {
                EventTriggerListener.Get(goButton).onClick = delHandle;
            }
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        protected void OpenUIForm(string uiFormName)
        {
            UIManager.GetInstance().ShowUIForms(uiFormName);
        }

        /// <summary>
        /// 关闭本UI窗体
        /// </summary>
        protected void CloseUIForm()
        {
            string strUIFromName = string.Empty;                       //处理后的UIFrom名称

            strUIFromName =gameObject.name;

            UIManager.GetInstance().CloseUIForms(strUIFromName);
        }

        /// <summary>
        /// 发送消息方法
        /// </summary>
        /// <param name="msgType">消息的类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="msgContent">消息内容</param>
        protected void SendMessage(string msgType,string msgName,object msgContent)
        {
            KeyValuesUpdate kvs = new KeyValuesUpdate(msgName, msgContent);
            MessageCenter.SendMessage(msgType, kvs);
        }

        /// <summary>
        /// 接受消息
        /// </summary>
        /// <param name="messagType">消息分类</param>
        /// <param name="handler">消息委托</param>
        protected void ReceiveMessage(string messagType,MessageCenter.DelMessageDelivery handler)
        {
            MessageCenter.AddMsgListener(messagType,handler);
        }
        #endregion
    }
}
