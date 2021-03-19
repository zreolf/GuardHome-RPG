using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    void Awake()
    {
        StartCoroutine(InitData());
    }

    IEnumerator InitData()
    {
        yield return null;
    }
}
