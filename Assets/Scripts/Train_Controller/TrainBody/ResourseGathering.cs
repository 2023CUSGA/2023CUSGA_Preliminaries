using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourseGathering : TrainBody
{
    private bool isMoving;  //是否在运动状态
    private bool isDizzy;  //是否在晕眩状态
    private bool isInResoursePoint = false; //是否在资源点内
    private GameObject trainHead;  //火车头物体
    
    // Start is called before the first frame update
    void Start()
    {
        trainHead = GameObject.Find("train_head");  //获取火车头物体
        isMoving = trainHead.GetComponent<TrainheadMov>().GetIsMoving();  //是否移动
        isDizzy = trainHead.GetComponent<TrainheadMov>().GetIsDizzy();  //是否晕眩
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

    private void CollectResourse()  //采集资源
    {
        if(!isMoving && isInResoursePoint && !isDizzy)
        {
            Debug.Log(id + " " + "资源采集中");
        }
    }
}
