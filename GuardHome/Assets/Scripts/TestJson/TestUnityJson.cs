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
    public class TestUnityJson : MonoBehaviour
    {
        void Start()
       {
            Hero heroObj = new Hero();
            heroObj.Name = "郭靖";
            heroObj.MyLevel = new Level() { HeroLevel = 800 };

            //方法1：Json 序列化工作（对象-->文件）
            string strHeroInfo = JsonUtility.ToJson(heroObj);
            Debug.Log("测试1：得到的序列化后的字符串="+ strHeroInfo);
            //方法2：反序列化（Json文件-->对象）
            Hero heroInfo2 = JsonUtility.FromJson<Hero>(strHeroInfo);
            Debug.Log("测试2：得到反序列化对象数值,名称:"+heroInfo2.Name+" 等级:"+heroInfo2.MyLevel.HeroLevel);
            //方法3： 测试覆盖反序列化输出
            Hero hero = new Hero();
            hero.Name = "杨过";
            hero.MyLevel = new Level() { HeroLevel = 500 };

            //Json 序列化
            string heroInfo3 = JsonUtility.ToJson(hero);
            //测试覆盖反序列化
            JsonUtility.FromJsonOverwrite(heroInfo3, heroObj);
            Debug.Log("测试3：得到再次反序列化覆盖的对象信息，名称：" + heroObj.Name + " 等级：" + heroObj.MyLevel.HeroLevel); ;
        }
    }
}
