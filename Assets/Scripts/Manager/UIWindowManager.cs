using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowManager : MonoSingleton<UIWindowManager>
{
    public GameObject selectWindow;

    /// <summary>
    /// 外部调用，用来打开选择框
    /// </summary>
    public UISelectWindow ShowSelectWindow(string text1 = "", string text2 = "", string text3 = "")
    {
        UISelectWindow window = selectWindow.GetComponent<UISelectWindow>();
        window.InitWindow(text1, text2, text3);
        selectWindow.SetActive(true);
        return window;
    }







}
