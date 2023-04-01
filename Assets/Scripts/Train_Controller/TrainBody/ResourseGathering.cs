using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseGathering : TrainBody
{
    private bool isMoving;  //�Ƿ����˶�״̬
    private bool isDizzy;  //�Ƿ�����ѣ״̬
    private bool isInResoursePoint = false; //�Ƿ�����Դ����
    private GameObject trainHead;  //��ͷ����
    
    // Start is called before the first frame update
    void Start()
    {
        trainHead = GameObject.Find("train_head");  //��ȡ��ͷ����
        isMoving = trainHead.GetComponent<TrainheadMov>().GetIsMoving();  //�Ƿ��ƶ�
        isDizzy = trainHead.GetComponent<TrainheadMov>().GetIsDizzy();  //�Ƿ���ѣ
    }

    // Update is called once per frame
    void Update()
    {
        isMoving = trainHead.GetComponent<TrainheadMov>().GetIsMoving();
        CollectResourse();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ResoursePoint")
        {
            isInResoursePoint = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ResoursePoint")
        {
            isInResoursePoint = false;
        }
    }

    private void CollectResourse()  //�ɼ���Դ
    {
        if(!isMoving && isInResoursePoint && !isDizzy)
        {
            Debug.Log(id + " " + "��Դ�ɼ���");
        }
    }
}
