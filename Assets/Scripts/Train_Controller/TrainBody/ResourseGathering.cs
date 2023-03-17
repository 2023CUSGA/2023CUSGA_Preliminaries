using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseGathering : TrainBody
{
    private float speed;  //列车是否在移动状态
    private bool isInResoursePoint = false; //是否在资源点内
    private GameObject trainHead;  //火车头物体
    
    // Start is called before the first frame update
    void Start()
    {
        trainHead = GameObject.Find("train_head");  //获取火车头物体
        speed = trainHead.GetComponent<TrainheadMovement>().GetTrainSpeed();  //是否移动
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

    private void CollectResourse()  //采集资源
    {
        if(speed.Equals(0) && isInResoursePoint)
        {
            Debug.Log(id + " " + "资源采集中");
        }
    }
}
