using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectWindow : MonoBehaviour
{
    public TextMeshProUGUI ButtonText_1;
    public TextMeshProUGUI ButtonText_2;
    public TextMeshProUGUI ButtonText_3;

    public Button button_1;
    public Button button_2;
    public Button button_3;

    public UnityAction Action1;
    public UnityAction Action2;
    public UnityAction Action3;

    public void InitWindow(string text1 = "", string text2 = "", string text3 = "")
    {
        ButtonText_1.text = text1;
        ButtonText_2.text = text2;
        ButtonText_3.text = text3;

        this.button_1.onClick.AddListener(OnClickButton_1);
        this.button_2.onClick.AddListener(OnClickButton_2);
        this.button_3.onClick.AddListener(OnClickButton_3);
        //button_1.Select();
    }

    /// <summary>
    /// 点击按钮1
    /// </summary>
    void OnClickButton_1()
    {
        Action1?.Invoke();
        Action1 = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击按钮2
    /// </summary>
    void OnClickButton_2()
    {
        Action2?.Invoke();
        Action2 = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击按钮3
    /// </summary>
    void OnClickButton_3()
    {
        Action3?.Invoke();
        Action3 = null;
        gameObject.SetActive(false);
    }






}
