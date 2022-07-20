using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MyDebug : MonoBehaviour
{
    public TextMeshProUGUI t;
    private string c = "";
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            t.text = "hoge" + c.ToString();
        
    }

    public void Hoge(string tt)
    {
        c = tt;
    }}
