using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainBodyMovement : TrainBody
{
    private GameObject train_head;  //火车头物体
    private Vector2 moveDir;  //当前节点移动方向
    private Rigidbody2D trainbody_RB;  //当前节点的刚体
    private List<Route> routeList = new List<Route>();  //转向节点list
    private float distace;  //与前一节车厢的距离
    private GameObject preNode;  //前一节车厢的物体
    private Route route;  //转向信息

    public event Action onRouteSetted;  //发送路径信息时的事件

    // Start is called before the first frame update
    void Start()
    {
        train_head = GameObject.Find("train_head");  //获取火车物体

        if (this.id == 0)  //获取前一节点物体
            preNode = train_head;
        else
            preNode = GameObject.Find("train_body" + (id - 1));

        moveDir = train_head.GetComponent<TrainheadMovement>().GetDir();  //获取当前的方向
        trainbody_RB = this.GetComponent<Rigidbody2D>();  //获取刚体

        if (this.id == 0)  //订阅设置路径事件
            preNode.GetComponent<TrainheadMovement>().onRouteSetted += this.RenewRouteList;
        else
            preNode.GetComponent<TrainBodyMovement>().onRouteSetted += this.RenewRouteList;

        this.distace = Vector2.Distance(preNode.transform.position, transform.position);  //计算与上一节点的距离
    }

    private void FixedUpdate()
    {
        RenewRoute();
    }

    // Update is called once per frame
    void Update()
    {
        MoveFunc();
    }

    void MoveFunc()  //移动函数
    {
        trainbody_RB.velocity = train_head.GetComponent<TrainheadMovement>().GetTrainSpeed() * moveDir;
    }

    void RenewRouteList()  //更新路径列表
    {
        Route tempRoute;
        if(this.id==0)
            tempRoute = preNode.GetComponent<TrainheadMovement>().GetRoute();
        else
            tempRoute = preNode.GetComponent<TrainBodyMovement>().GetRoute();

        if (tempRoute.GetIsVaild())
        {
            if (routeList.Count <= 0)
            {
                routeList.Add(tempRoute);
                return;
            }
            else
            {
                Route lastRoute = routeList[routeList.Count() - 1];
                if (!lastRoute.GetCurrentDir().Equals(tempRoute.GetCurrentDir())
                    || !lastRoute.GetCurrentPosition().Equals(tempRoute.GetCurrentPosition()))
                {
                    routeList.Add(tempRoute);
                    return;
                }
            }
        }
        return;
    }

    void RenewRoute()  //更新当前节点的路径
    {
        if (routeList.Count() > 0)
        {
            Vector2 tempPosition = routeList[0].GetCurrentPosition();
            Vector2 tempDir = routeList[0].GetCurrentDir();
            if (Vector2.Distance(tempPosition, transform.position) < 0.1f)
            {
                this.moveDir = tempDir;
                ChangeRotation(moveDir);  //改变图片朝向
                this.transform.position = tempPosition;
                routeList.RemoveAt(0);
                if (Vector2.Distance(preNode.transform.position, transform.position) != this.distace)  //转向后修正
                    transform.position = preNode.transform.position - new Vector3(this.moveDir.x, this.moveDir.y, 0) * this.distace;
                this.SetRoute(new Route(this.transform.position, this.moveDir));  //设置转向时的信息
            }
        }
        /*
        if ((Vector2.Distance(preNode.transform.position, transform.position) - this.distace) > 0.05f)  //修正
        {
            this.moveDir = train_head.GetComponent<TrainheadMovement>().GetDir();
            ChangeRotation(moveDir);  //改变图片朝向
            transform.position = preNode.transform.position - new Vector3(this.moveDir.x, this.moveDir.y, 0) * this.distace;
            //routeList.Clear();
        }
        */
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
        if(this.id<TrainManager.GetInstance().GetTrainLength()-2)
            this.onRouteSetted();
    }

    public Route GetRoute()  //route Get方法
    {
        return this.route;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Vector2 GetDir()
    {
        return moveDir;
    }
}
