/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  Description:
 *         功能：UI遮罩管理器
 *         
 *  Author:周立军
 *  
 *  Data:2020-05-05
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
    public class UIMaskMgr : MonoBehaviour
    {
        /*字段*/
        //本脚本私有单例
        private static UIMaskMgr _Instance = null;
        //UI根节点对象
        private GameObject _GoCanvasRoot = null;
        //UI脚本节点对象
        private Transform _TraUIScriptsNode = null;
        //顶层面板
        private GameObject _GoTopPanel = null;
        //遮罩面板
        private GameObject _GoMaskPanel = null;
        //UI摄像机
        private Camera _UICamera;
        //UI摄像机“层深”
        private float _OriginalUICameralDepth;
        //得到实体
        public static UIMaskMgr GetInstance()
        {
            if (_Instance==null)
            {
                _Instance =new GameObject("_UIMaskMgr").AddComponent<UIMaskMgr>();
            }
            return _Instance;
        }

        private void Awake()
        {
            //得到UI节点对象、脚本节点对象
            _GoCanvasRoot = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS);
            _TraUIScriptsNode = UnityHelp.FindTheChildNode(_GoCanvasRoot,SysDefine.SYS_SCRIPTMANAGER_NODE);
            //把本脚本实例，作为“脚本节点对象”的子节点
            UnityHelp.AddChildNodeToParentNode(_TraUIScriptsNode,this.gameObject.transform);
            //得到“顶层面板”、“遮罩面板”
            _GoTopPanel = _GoCanvasRoot;
            _GoMaskPanel = UnityHelp.FindTheChildNode(_GoCanvasRoot, "_UIMaskPanel").gameObject;
            //得到UI摄像机原始的“层深”
            _UICamera = GameObject.FindGameObjectWithTag("_TagUICamera").GetComponent<Camera>();
            if (_UICamera!=null)
            {
                //得到UI摄像机原始“层深”
                _OriginalUICameralDepth = _UICamera.depth;
            }
            else
            {
                Debug.LogError(GetType()+ "/Awake()/_UICamera is Null!,Please Check");
            }
        }

        /// <summary>
        /// 设置遮罩状态
        /// </summary>
        /// <param name="goDisplarUIForms">需要显示的UI窗体</param>
        /// <param name="lucenyType">显示透明度属性</param>
        public void SetMaskWindow(GameObject goDisplarUIForms,UIFormLucenyType lucenyType=UIFormLucenyType.Lucency)
        {
            //顶层窗口下移
            _GoTopPanel.transform.SetAsLastSibling();
            //启用遮罩窗体以及设置透明度
            switch (lucenyType)
            {
                case UIFormLucenyType.Lucency:                                  //完全透明，不能穿透
                    _GoMaskPanel.SetActive(true);
                    Color newColor1 = new Color(255,255,255,0);
                    _GoMaskPanel.GetComponent<Image>().color = newColor1;
                    break;
                case UIFormLucenyType.Translucence:                              //半透明， 不能穿透
                    _GoMaskPanel.SetActive(true);
                    Color newColor2 = new Color(220, 220, 220, 50);
                    _GoMaskPanel.GetComponent<Image>().color = newColor2;
                    break;
                case UIFormLucenyType.ImPenetrable:                              //低透明，不能穿透
                    _GoMaskPanel.SetActive(true);
                    Color newColor3 = new Color(50, 50, 50, 200);
                    _GoMaskPanel.GetComponent<Image>().color = newColor3;
                    break;
                case UIFormLucenyType.Pentrate:                                  //可以穿透
                    if (_GoMaskPanel.activeInHierarchy)
                    {
                        _GoMaskPanel.SetActive(false);
                    }
                    break;
                default:
                    break;
            }
            //遮罩窗体下移
            _GoMaskPanel.transform.SetAsLastSibling();
            //显示窗体下移
            goDisplarUIForms.transform.SetAsLastSibling();
            //增加当前UI摄像机的层深（保证当前摄像机为最前显示）
            if (_UICamera!=null)
            {
                _UICamera.depth = _UICamera.depth + 100;  //增加层深
            }
        }

        /// <summary>
        /// 取消遮罩状态
        /// </summary>
        public void CancelMaskWindow()
        {
            //顶层窗口上移
            _GoTopPanel.transform.SetAsFirstSibling();
            //禁用遮罩窗体
            if (_GoMaskPanel.activeInHierarchy)
            {
                //隐藏
                _GoMaskPanel.SetActive(false);
            }
            //恢复当前UI摄像机的层深
            if (_UICamera != null)
            {
                _UICamera.depth = _OriginalUICameralDepth;  //恢复层深
            }
        }

    }
}
