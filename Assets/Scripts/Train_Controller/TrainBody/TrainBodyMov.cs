using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainBodyMov : MonoBehaviour
{
    private GameObject train_head;  //火车头物体
    private int id; //车厢id
    [SerializeField]private Vector2 moveDir;  //当前节点移动方向
    private Rigidbody2D trainbody_RB;  //当前节点的刚体
    private GameObject childNode;  //子节点游戏物体
    private List<Route> routeList = new List<Route>();  //转向节点list
    private GameObject preNode;  //前一节车厢的物体
    private Route route;  //转向信息
    private float distance;  //与前一节车厢的距离

    public event Action onRouteSetted;  //发送路径信息时的事件

    void Start()
    {
        id = gameObject.GetComponent<TrainBody>().GetId();
        childNode = this.transform.GetChild(0).gameObject;  //获取子节点游戏物体
        train_head = GameObject.Find("train_head");  //获取火车物体

        if (this.id == 0)  //获取前一节点物体
            preNode = train_head;
        else
            preNode = GameObject.Find("train_body" + (id - 1));

        moveDir = train_head.GetComponent<TrainheadMov>().GetDir();  //获取当前的方向
        trainbody_RB = this.GetComponent<Rigidbody2D>();  //获取刚体

        if (this.id == 0)  //订阅设置路径事件
            preNode.GetComponent<TrainheadMov>().onRouteSetted += this.RenewRouteList;
        else
            preNode.GetComponent<TrainBodyMov>().onRouteSetted += this.RenewRouteList;

        this.distance = Vector2.Distance(preNode.transform.position, transform.position);  //计算与上一节点的距离
    }

    private void FixedUpdate()
    {
        RenewRoute();
    }

    void Update()
    {
        //RenewRoute();
    }

    void MoveFunc()  //移动函数
    {
        this.transform.position = Vector2.MoveTowards(transform.position,childNode.transform.position, train_head.GetComponent<TrainheadMov>().GetTrainSpeed()*Time.fixedDeltaTime);
    }

    void RenewRouteList()  //更新路径列表
    {
        Route tempRoute;
        if (this.id == 0)
            tempRoute = preNode.GetComponent<TrainheadMov>().GetRoute();
        else
            tempRoute = preNode.GetComponent<TrainBodyMov>().GetRoute();

        if (tempRoute.GetIsVaild())
        {
            if(this.id==0 && train_head.GetComponent<TrainheadMov>().GetTrainSpeed().Equals(0) && routeList.Count()>0)
            {
                routeList[routeList.Count() - 1] = tempRoute;
            }
            else routeList.Add(tempRoute);
        }
        return;
    }

    void RenewRoute()  //更新当前节点的路径
    {
        if (routeList.Count() > 0)
        {
            Vector2 tempPosition = routeList[0].GetCurrentPosition();
            Vector2 tempDir = routeList[0].GetCurrentDir();
            this.transform.position = Vector2.MoveTowards(transform.position, tempPosition, train_head.GetComponent<TrainheadMov>().GetTrainSpeed()*Time.fixedDeltaTime);
            if(Vector2.Distance(tempPosition, transform.position) < 0.02f)
            {
                this.moveDir = tempDir;
                ChangeRotation(moveDir);  //改变图片朝向
                this.transform.position = tempPosition;
                this.SetRoute(new Route(this.transform.position, this.moveDir));  //设置转向时的信息
                routeList.RemoveAt(0);
                if (Vector3.Distance(transform.position, preNode.transform.position) != distance && routeList.Count()<=0)
                {
                    //Debug.Log("调整位置");
                    transform.position = new Vector3(preNode.transform.position.x - distance * moveDir.x,
                                                     preNode.transform.position.y - distance * moveDir.y, 0);
                }
                //Time.timeScale = 0;
            }

        }
        else
            MoveFunc();
    }

    void ChangeRotation(Vector2 moveDir)  //改变朝向函数
    {
        if (moveDir.Equals(new Vector2(0, 1)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);  //调整图形角度
            return;
        }
        if (moveDir.Equals(new Vector2(-1, 0)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);  //调整图形角度
            return;
        }
        if (moveDir.Equals(new Vector2(0, -1)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);  //调整图形角度
            return;
        }
        if (moveDir.Equals(new Vector2(1, 0)))
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);  //调整图形角度
            return;
        }
    }

    public void SetRoute(Route new_route)  //route Set方法
    {
        this.route = new_route;
        if (this.id < TrainManager.GetInstance().GetTrainLength() - 2)
            this.onRouteSetted();
    }

    public Route GetRoute()  //route Get方法
    {
        return this.route;
    }
}
