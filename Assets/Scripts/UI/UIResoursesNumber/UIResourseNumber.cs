using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourseNumber : MonoBehaviour
{
    [SerializeField]private string resourseName;  //资源名称
    
    // Start is called before the first frame update
    void Start()
    {
        resourseName = this.name;  //获取资源名称
        InitFunc();  //初始化资源
    }

    void ReNewText()  //更新UI
    {
        this.GetComponent<Text>().text = ": " + ResoursesManager.GetInstance().GetResoursesNumber(resourseName);
    }

    void InitFunc()  //初始化
    {
        ResoursesManager.GetInstance().OnResourseNumberChanged += this.ReNewText;
        this.GetComponent<Text>().text = ": " + ResoursesManager.GetInstance().GetResoursesNumber(resourseName);
    }
}
