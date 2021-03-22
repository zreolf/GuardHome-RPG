/***
 * 
 *  Title:"SUIFW"UI框架项目项目
 *  
 *  Description:
 *         功能：
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

namespace Test
{
    public class TestUnityJson2 : MonoBehaviour
    {
        void Start()
       {
            TextAsset textAsset = Resources.Load<TextAsset>("People");
            PersonInfo personInfo = JsonUtility.FromJson<PersonInfo>(textAsset.text);

            



















          //  //提取文件，得到字符串数据
          //TextAsset TaObj = Resources.Load<TextAsset>("People");
          //  //反序列化 文件->对象
          //PersonInfo perInfo = JsonUtility.FromJson<PersonInfo>(TaObj.text);
          //  //显示对象中数据
          //  foreach (PeoPle per in perInfo.People)
          //  {
          //      Debug.Log(" ");
          //      Debug.Log(string.Format("name={0},Age={1}",per.Name,per.Age));
          //  }
        }
    }
}
