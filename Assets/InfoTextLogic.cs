using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTextLogic : MonoBehaviour
{
    private int start = 0;
    public Text Info;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            start = 1;
        }
        if (start == 1)
        {
            Info.text = "";
        }
    }
}
