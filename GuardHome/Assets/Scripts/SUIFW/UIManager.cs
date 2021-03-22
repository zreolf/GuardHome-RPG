/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *           
 *  
 *  Description:UI管理器
 *         功能：是整个UI框架的核心，用户通过本脚本，来实现框架绝大多数的功能实现。
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
    public class UIManager : MonoBehaviour
    {
        /* 字段*/
        private static UIManager _Instance = null;
        //UI窗体预设路径(键:窗体预设名称， 值：表示窗体预设路径)
        private Dictionary<string, string> _DicFormsPaths;
        //缓存所有UI窗体
        private Dictionary<string, BaseUIForm> _DicALLUIForms;
        //当前显示的UI窗体
        private Dictionary<string, BaseUIForm> _DicCurrentShowUIForms;
        //定义“栈”集合,显示当前所有[反向切换]（具备“反向切换”属性窗体的管理）
        private Stack<BaseUIForm> _StaCurrentUIForms;
        //UI根节点
        private Transform _TraCanvasTransform =null;
        //全屏幕显示节点
        private Transform _TraNormal = null;
        //固定显示的节点
        private Transform _TraFixed = null;
        //弹出节点
        private Transform _TraPopUp = null;
        //UI管理脚本的节点
        private Transform _TraUIScripts = null;

        /// <summary>
        /// 得到实例
        /// </summary>
        /// <returns></returns>
        public static UIManager GetInstance()
        {
            if (_Instance==null)
            {
                _Instance = new GameObject("_UIManager").AddComponent<UIManager>();
            }
            return _Instance;
        }
        //初始化核心数据，加载“UI窗体路径”到集合中
        private void Awake()
        {
            //字段初始化
            _DicALLUIForms = new Dictionary<string, BaseUIForm>();
            _DicCurrentShowUIForms = new Dictionary<string, BaseUIForm>();
            _DicFormsPaths = new Dictionary<string, string>();
            _StaCurrentUIForms = new Stack<BaseUIForm>();
            //初始化加载（根ui窗体）Canvas预设
            InitRootCanvasLoading();
            //得到UI根节点、全屏节点、固定节点、弹出节点
            _TraCanvasTransform = GameObject.FindGameObjectWithTag(SysDefine.SYS_TAG_CANVAS).transform;

            _TraNormal = UnityHelp.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_NORMAL_NODE);
            _TraFixed = UnityHelp.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_FIXED_NODE);
            _TraPopUp = UnityHelp.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_POPUP_NODE);
            _TraUIScripts = UnityHelp.FindTheChildNode(_TraCanvasTransform.gameObject, SysDefine.SYS_SCRIPTMANAGER_NODE);

            //把本脚本作为脚本UI窗体的子节点
            this.gameObject.transform.SetParent(_TraUIScripts, false);
            //“根UI窗体”在场景转换的时候，不允许销毁
            DontDestroyOnLoad(_TraCanvasTransform);
            //初始化“UI窗体预设”路径数据
            if (_DicFormsPaths!=null)
            {
                InitUIFormsPathData();
            }
            
        }

        /// <summary>
        /// 显示（打开）UI窗体
        /// 功能：
        /// 1：根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// 2：根据不同的UI窗体的显示模式，分别做不同的加载处理
        /// </summary>
        /// <param name="uiFormName">UI窗体预设的名称</param>
        public void ShowUIForms(string uiFormName)
        {
            BaseUIForm baseUIForms=null;                                           //UI窗体基类
            //参数的检测
            if (string.IsNullOrEmpty(uiFormName))
            {
                return;
            }
         
            //根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
            baseUIForms = LoadFormsToAllUIFormsCatch(uiFormName);
            if (baseUIForms==null)
            {
                return;
            }

            //是否清空“栈集合”中的数据
            if (baseUIForms.CurrentUIType.IsClearStack)
            {
                if (!ClearStackArray())
                {
                    Debug.LogError("栈中数据没有清空！请检查。参数：uiFormName" + uiFormName);
                }
            }
            if(uiFormName == "MainCityUIForm")
            {
                baseUIForms.CurrentUIType.UIForms_ShowMode = UIFormShoeMode.HideOther;
            }
            //根据不同的UI窗体的显示模式，分别做不同的加载处理
            switch (baseUIForms.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormShoeMode.Normal:
                    //把当前窗体加载到“当前窗体”集合中。
                    LoadUIToCurrentCache(uiFormName);                          //"普通显示"窗体模式
                    break;
                case UIFormShoeMode.ReversChange:                              //需要“反向切换”窗体模式
                    PushUIFormToStack(uiFormName);
                    break;
                case UIFormShoeMode.HideOther:                                 //“隐藏其他”窗口模式
                    EnterUIFormAndHideOther(uiFormName);
                    break;
                default:
                    break;
            }

        }


        /// <summary>
        /// 关闭（返回上一个）窗体
        /// </summary>
        /// <param name="uiFormName"></param>
        public void CloseUIForms(string uiFormName)
        {
            BaseUIForm baseUiForm;
            //检查参数
            if (string.IsNullOrEmpty(uiFormName)) return;
            //“所有UI窗体”集合中，如果没有记录，则直接返回
            _DicALLUIForms.TryGetValue(uiFormName,out baseUiForm);
            if (baseUiForm == null) return;
            //根据窗体不同的显示类型，分别做不同的关闭处理
            switch (baseUiForm.CurrentUIType.UIForms_ShowMode)
            {
                case UIFormShoeMode.Normal:
                    //普通窗体的关闭
                    ExitUIFormToStack(uiFormName);
                    break;
                case UIFormShoeMode.ReversChange:
                    //反向切换窗体的关闭
                    PopUIForms();
                    break;
                case UIFormShoeMode.HideOther:
                    //隐藏其他窗体关闭
                    ExitUIFormAndHideOther(uiFormName);
                    break;
                default:
                    break;
            }
        }


        #region 显示“UI管理器”内部核心数据，测试使用

        /// <summary>
        /// 显示所有UI窗体的数量
        /// </summary>
        /// <returns></returns>
        public int ShowALLUIFormCount()
        {
            if (_DicALLUIForms!=null)
            {
                return _DicALLUIForms.Count;
            }
            return 0;
        }

        /// <summary>
        /// 显示“当前窗体"的数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentUIFormCount()
        {
            if (_DicCurrentShowUIForms != null)
            {
                return _DicCurrentShowUIForms.Count;
            }
            return 0;
        }

        /// <summary>
        /// 显示“当前栈"的数量
        /// </summary>
        /// <returns></returns>
        public int ShowCurrentStackUIFormCount()
        {
            if (_StaCurrentUIForms != null)
            {
                return _StaCurrentUIForms.Count;
            }
            return 0;
        }



        #endregion


        #region 私有方法
        /// <summary>
        /// 初始化加载（根ui窗体）Canvas预设
        /// </summary>
        private void InitRootCanvasLoading()
        {
            ResourcesMgr.GetInstance().LoadAsset(SysDefine.SYS_PATH_CANVAS, false);
        }

        /// <summary>
        /// 根据UI窗体的名称，加载到“所有UI窗体”缓存集合中
        /// 功能：判断“所有窗体”是否加载过了，没加载过在加载
        /// </summary>
        /// <param name="uiFormsName">UI窗体预设的名称</param>
        /// <returns></returns>
        private BaseUIForm LoadFormsToAllUIFormsCatch(string uiFormsName)
        {
            BaseUIForm baseUIResulr = null;                                      //加载的返回UI窗体基类

            _DicALLUIForms.TryGetValue(uiFormsName,out baseUIResulr);
            if (baseUIResulr==null)
            {
                //加载指定路径的“UI窗体”
                baseUIResulr = LoadUIForm(uiFormsName);
            }
            return baseUIResulr;
        }


        /// <summary>
        /// 加载指定名称的“UI窗体”
        /// 功能：
        ///     1：根据“UI窗体名称”，加载预设克隆体。
        ///     2：根据不同预设克隆体中带的脚本中不同的“位置信息”，加载到“根窗体”下不同节点。
        ///     3：隐藏刚创建的UI克隆体
        ///     4：把克隆体，加入到“所有UI窗体”（缓存）集合中。
        /// </summary>
        /// <param name="uiFormName">UI窗体名称</param>
        private BaseUIForm LoadUIForm(string uiFormName)
        {
            string strUIFormPaths = null;                                   //UI窗体路径
            GameObject goCloneUIPrefabs = null;                             //创建的UI克隆体预设
            BaseUIForm baseUiForm = null ;                                          //窗体基类
            //根据ui窗体名称，得到对应的加载路径
            _DicFormsPaths.TryGetValue(uiFormName,out strUIFormPaths);

            //根据“UI窗体名称”，加载预设克隆体。
            if (!string.IsNullOrEmpty(strUIFormPaths))
            {
                goCloneUIPrefabs = ResourcesMgr.GetInstance().LoadAsset(strUIFormPaths,false);
            }
            //设置“UI克隆体”的父节点（根据克隆体中带的脚本中不同的“位置信息”）
            if (_TraCanvasTransform!=null&&goCloneUIPrefabs!=null)
            {
                baseUiForm = goCloneUIPrefabs.GetComponent<BaseUIForm>();
                if (baseUiForm==null)
                {
                    Debug.Log("baseUiForm==null,请先确认窗体预设对象上是否加载了baseUIForm的子类脚本！参数 uiFormName="+ uiFormName);
                    return null;
                }
                switch (baseUiForm.CurrentUIType.UIForms_Type)
                {
                    case UIFormType.Normal:
                        goCloneUIPrefabs.transform.SetParent(_TraNormal,false);
                        break;
                    case UIFormType.Fixed:
                        goCloneUIPrefabs.transform.SetParent(_TraFixed, false);
                        break;
                    case UIFormType.PopUp:
                        goCloneUIPrefabs.transform.SetParent(_TraPopUp, false);
                        break;
                    default:
                        break;
                }
                //设置隐藏
                goCloneUIPrefabs.SetActive(false);
                //把克隆体，加入到“所有UI窗体”（缓存）集合中。
                _DicALLUIForms.Add(uiFormName,baseUiForm);
                return baseUiForm;
            }//if_end
            else
            {
                Debug.Log("_TraCanvasTransform==null or goCloneUIPrefabs==null!!");
            }
            Debug.Log("出现不可预估的错误，请检查");
            return null;
        }

        /// <summary>
        /// 把当前窗体加载到“当前窗体”集合中
        /// </summary>
        /// <param name="uiFormName">窗体预设名称</param>
        private void LoadUIToCurrentCache(string uiFormName)
        {
            BaseUIForm baseUiForm;                                                   //UI窗体基类
            BaseUIForm baseUIFormFromAllCache;                                       //从“所有窗体集合”中得到的窗体
            //如果“正在显示”的集合中，存在这个UI窗体，则直接返回
            _DicCurrentShowUIForms.TryGetValue(uiFormName, out baseUiForm);
            if (baseUiForm!=null)
            {
                return;
            }
            //把当前窗体，加载到“正在显示”集合中
            _DicALLUIForms.TryGetValue(uiFormName,out baseUIFormFromAllCache);
            if (baseUIFormFromAllCache != null)
            {
                _DicCurrentShowUIForms.Add(uiFormName,baseUIFormFromAllCache);
                baseUIFormFromAllCache.Display();//显示当前窗体
            }

        }

        /// <summary>
        /// UI窗体入栈
        /// </summary>
        /// <param name="uiFormName">窗体的名称</param>
        private void PushUIFormToStack(string uiFormName)
        {
            BaseUIForm baseUIForm;                                                     //UI窗体
            //判断“栈”集合中，是否有其他的窗体，有则“冻结”处理。
            if (_StaCurrentUIForms.Count>0)
            {
               BaseUIForm topUIForm =  _StaCurrentUIForms.Peek();
                //栈顶元素作冻结处理
                topUIForm.Freeze();
            }
            //判断“UI所有窗体”集合是否有指定的UI窗体，有则处理
            _DicALLUIForms.TryGetValue(uiFormName,out baseUIForm);
            if (baseUIForm!=null)
            {
                //当前窗口显示状态
                baseUIForm.Display();
                //把指定的UI窗体，入栈操作。
                _StaCurrentUIForms.Push(baseUIForm);
            }
            else
            {
                Debug.LogError("baseUIForm==null,请检查，参数uiFormName="+ uiFormName);
            }
        }

        /// <summary>
        /// 退出指定UI窗体
        /// </summary>
        /// <param name="strUIFormName"></param>
        private void ExitUIFormToStack(string strUIFormName)
        {
            BaseUIForm baseUIForm;
            //“正在显示集合”中如果没有记录，则直接返回
            _DicCurrentShowUIForms.TryGetValue(strUIFormName, out baseUIForm);
            if (baseUIForm == null) return;
            //指定窗体，标记为“隐藏状态”，并且从“正在显示的集合”中移除
            baseUIForm.Hiding();
            _DicCurrentShowUIForms.Remove(strUIFormName);
        }

        /// <summary>
        /// ("反向切换"属性)窗体的出栈逻辑
        /// </summary>
        private void PopUIForms()
        {

            if (_StaCurrentUIForms.Count>=2)
            {
                //出栈处理
                BaseUIForm topUIForms = _StaCurrentUIForms.Pop();
                //做隐藏处理
                topUIForms.Hiding();
                //出栈后，下一个窗体做“重新显示”处理
                BaseUIForm nextUIForms = _StaCurrentUIForms.Peek();
                nextUIForms.Redisplay();
            }
            else if (_StaCurrentUIForms.Count==1)
            {
                //出栈处理
                BaseUIForm topUIForms = _StaCurrentUIForms.Pop();
                //做隐藏处理
                topUIForms.Hiding();
            }
        }
        /// <summary>
        /// 打开窗体，并隐藏其他窗体
        /// </summary>
        /// <param name="strUIName">打开的指定窗体名称</param>
        private void EnterUIFormAndHideOther(string strUIName)
        {
            BaseUIForm baseUIForm;                                   //UI窗体基类
            BaseUIForm baseUIFormFromAll;                            //从集合中得到UI窗体基类                    
            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;
            _DicCurrentShowUIForms.TryGetValue(strUIName,out baseUIForm);
            if (baseUIForm != null) return;
            //把“正在显示集合”与“栈集合”中所有窗体都做隐藏处理
            foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
            {
                baseUI.Hiding();
            }
            foreach (BaseUIForm staUI in _StaCurrentUIForms)
            {
                staUI.Hiding();
            }
            //把当前窗体加入到“正在显示窗体”集合中，且做显示处理
            _DicCurrentShowUIForms.TryGetValue(strUIName,out baseUIFormFromAll);
            if (baseUIFormFromAll!=null)
            {
                _DicCurrentShowUIForms.Add(strUIName, baseUIFormFromAll);
                //窗体显示
                baseUIFormFromAll.Display();
            }

        }


        /// <summary>
        /// 关闭窗体，并显示其他窗体
        /// </summary>
        /// <param name="strUIName">关闭的指定窗体名称</param>
        private void ExitUIFormAndHideOther(string strUIName)
        {
            BaseUIForm baseUIForm;                                   //UI窗体基类               
            //参数检查
            if (string.IsNullOrEmpty(strUIName)) return;
            _DicCurrentShowUIForms.TryGetValue(strUIName, out baseUIForm);
            if (baseUIForm == null) return;

            //当前窗体隐藏状态，且“正在显示”集合中，移除本窗体
            baseUIForm.Hiding();
            _DicCurrentShowUIForms.Remove(strUIName);

            //把“正在显示集合”与“栈集合”中所有窗体都做显示处理
            foreach (BaseUIForm baseUI in _DicCurrentShowUIForms.Values)
            {
                baseUI.Redisplay();
            }
            foreach (BaseUIForm staUI in _StaCurrentUIForms)
            {
                staUI.Redisplay();
            }
            

        }


        /// <summary>
        /// 是否清空“栈集合”中的数据
        /// </summary>
        /// <returns></returns>
        private bool ClearStackArray()
        {
            if (_StaCurrentUIForms!=null&& _StaCurrentUIForms.Count>=1)
            {
                //清空栈集合
                _StaCurrentUIForms.Clear();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 读取配置文件中的数据
        /// </summary>
        private void InitUIFormsPathData()
        {
            _DicFormsPaths = new ConfigManagerByJson(SysDefine.SYS_PATH_UIFORMS_CONFFIG_INFO).AppString;
        }


        #endregion
    }
}
