using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseGathering : TrainBody
{
    private float speed;  //�г��Ƿ����ƶ�״̬
    private bool isInResoursePoint = false; //�Ƿ�����Դ����
    private GameObject trainHead;  //��ͷ����
    
    // Start is called before the first frame update
    void Start()
    {
        trainHead = GameObject.Find("train_head");  //��ȡ��ͷ����
        speed = trainHead.GetComponent<TrainheadMovement>().GetTrainSpeed();  //�Ƿ��ƶ�
    }

    // Update is called once per frame
    void Update()
    {
        speed = trainHead.GetComponent<TrainheadMovement>().GetTrainSpeed();
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
        if(speed.Equals(0) && isInResoursePoint)
        {
            Debug.Log(id + " " + "��Դ�ɼ���");
        }
    }
}
