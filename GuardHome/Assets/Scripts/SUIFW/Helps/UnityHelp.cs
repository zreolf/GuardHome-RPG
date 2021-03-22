/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  Description:
 *         功能：提供程序用户一些常用的功能方法实现。
 *         
 *  Author:周立军
 *  
 *  Data:2020-05-04
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
    public class UnityHelp : MonoBehaviour
    {
        /// <summary>
        /// 查找子节点对象
        /// </summary>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">查找的子对象名称</param>
        /// <returns></returns>
        public static Transform FindTheChildNode(GameObject goParent,string childName)
        {
            Transform searchTrans =  null;
            searchTrans = goParent.transform.Find(childName);
            if (searchTrans ==null)
            {
               
                foreach (Transform trans in goParent.transform)
                {
                    searchTrans = FindTheChildNode(trans.gameObject, childName);
                    if (searchTrans!=null)
                    {
                        return searchTrans;
                    }
                }
            }
            return searchTrans;
        }

        /// <summary>
        /// 获得子节点（对象）脚本
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T GetTheChildNodeComponentScripts<T>(GameObject goParent,string childName) where T:Component
        {
            Transform searchTranformNode = null;                               //查找特定子节点
            searchTranformNode = FindTheChildNode(goParent, childName);
            if (searchTranformNode!=null)
            {
                return searchTranformNode.gameObject.GetComponent<T>();
            }
            return null;
        }
        /// <summary>
        /// 给子节点添加脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="goParent">父对象</param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T AddChildNodeComponent<T>(GameObject goParent,string childName) where T:Component
        {
            Transform searchTranform = null;                                            // 查找特定节点结果

            //查找特定子节点
            searchTranform = FindTheChildNode(goParent, childName);
            //如果查找成功，则考虑如果已经有相同的脚本，则先删除，否则直接添加
            if (searchTranform!=null)
            {
                //如果已经有相同的脚本，则先删除
               T[] componentScriptsArray = searchTranform.GetComponents<T>();
                for (int i = 0; i < componentScriptsArray.Length; i++)
                {
                    if (componentScriptsArray[i]!=null)
                    {
                        Destroy(componentScriptsArray[i]);
                    }
                }
                return searchTranform.gameObject.AddComponent<T>();
            }
            else
            {
                return null;
            }
            //如果查找不成功，返回null
        }
        /// <summary>
        /// 给子节点添加父对象
        /// </summary>
        /// <param name="parents">父对象</param>
        /// <param name="child">子对象</param>
        public static void AddChildNodeToParentNode(Transform parents,Transform child)
        {
            child.SetParent(parents,false);
            child.localPosition = Vector3.zero;
            child.localScale = Vector3.one;
            child.localEulerAngles = Vector3.zero;
        }

    }
}
