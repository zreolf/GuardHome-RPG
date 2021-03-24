using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using UnityEngine;
using Protobuf;
public class TestDemo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Person p = new Person();
        p.Name = "TestName";
        p.Age = 22;
        p.NameList.Add("Player1");
        p.NameList.Add("Player2");
        p.NameList.Add("Player3");

        byte[] databytes = p.ToByteArray();

        IMessage IMperson = new Person();
        Person p1 = new Person();
        p1 = (Person) IMperson.Descriptor.Parser.ParseFrom(databytes);

        Debug.Log(p1.Name);
        Debug.Log(p1.Age);
        for (int i = 0; i < p1.NameList.Count; i++)
        {
            Debug.Log(p1.NameList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
