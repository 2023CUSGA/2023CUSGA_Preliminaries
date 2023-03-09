using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResoursesManager : MonoBehaviour
{
    private static ResoursesManager instance;  //����ģʽ
    public enum resourseNames:int  //��Դ���Ƽ����Ӧ�ı��
    {
        ľ�� = 0,
        ʯ�� = 1,
        ͭ�� = 2,
        ���� = 3,
        �Ͻ� = 4,
        ��� = 5
    }

    //��Դ����
    private List<int> resoursesList;  //��Դ����list
    public Action OnResourseNumberChanged;  //��Դ�����ı��¼�

    void Awake()
    {
        instance = this;
        if (resoursesList == null)
            resoursesList = new List<int>() { 0,0,0,0,0,0};
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadFromLocal();
        OnResourseNumberChanged();
    }

    public int GetResoursesNumber(string name)  //������Դ����
    {
        return resoursesList[(int)Enum.Parse(typeof(resourseNames), name)];
    }

    public void AddResourses(string name,int num)  //��Դ����������
    {
        resoursesList[(int)Enum.Parse(typeof(resourseNames), name)] += num;
        OnResourseNumberChanged();
    }

    private void LoadFromLocal()  //�ӱ��ش浵��ȡ����
    {
        //�ӱ��ش浵��ȡ����
    }

    public static ResoursesManager GetInstance()  //����ģʽ
    {
        return instance;
    }
}
