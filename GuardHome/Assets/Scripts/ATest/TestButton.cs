using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            Debug.Log("New Start");
            SceneManager.LoadSceneAsync("MainSence");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
