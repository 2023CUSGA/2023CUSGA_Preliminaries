using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourseNumber : MonoBehaviour
{
    [SerializeField]private string resourseName;  //��Դ����
    
    // Start is called before the first frame update
    void Start()
    {
        resourseName = this.name;  //��ȡ��Դ����
        InitFunc();  //��ʼ����Դ
    }

    void ReNewText()  //����UI
    {
        this.GetComponent<Text>().text = ": " + ResoursesManager.GetInstance().GetResoursesNumber(resourseName);
    }

    void InitFunc()  //��ʼ��
    {
        ResoursesManager.GetInstance().OnResourseNumberChanged += this.ReNewText;
        this.GetComponent<Text>().text = ": " + ResoursesManager.GetInstance().GetResoursesNumber(resourseName);
    }
}
