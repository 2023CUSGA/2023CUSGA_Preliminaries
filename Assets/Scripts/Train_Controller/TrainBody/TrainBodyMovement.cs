using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainBodyMovement : TrainBody
{
    private GameObject train_head;  //��ͷ����
    private Vector2 moveDir;  //��ǰ�ڵ��ƶ�����
    private Rigidbody2D trainbody_RB;  //��ǰ�ڵ�ĸ���
    private List<Route> routeList = new List<Route>();  //ת��ڵ�list
    private float distace;  //��ǰһ�ڳ���ľ���
    private GameObject preNode;  //ǰһ�ڳ��������
    private Route route;  //ת����Ϣ

    public event Action onRouteSetted;  //����·����Ϣʱ���¼�

    // Start is called before the first frame update
    void Start()
    {
        train_head = GameObject.Find("train_head");  //��ȡ������

        if (this.id == 0)  //��ȡǰһ�ڵ�����
            preNode = train_head;
        else
            preNode = GameObject.Find("train_body" + (id - 1));

        moveDir = train_head.GetComponent<TrainheadMovement>().GetDir();  //��ȡ��ǰ�ķ���
        trainbody_RB = this.GetComponent<Rigidbody2D>();  //��ȡ����

        if (this.id == 0)  //��������·���¼�
            preNode.GetComponent<TrainheadMovement>().onRouteSetted += this.RenewRouteList;
        else
            preNode.GetComponent<TrainBodyMovement>().onRouteSetted += this.RenewRouteList;

        this.distace = Vector2.Distance(preNode.transform.position, transform.position);  //��������һ�ڵ�ľ���
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

    void MoveFunc()  //�ƶ�����
    {
        trainbody_RB.velocity = train_head.GetComponent<TrainheadMovement>().GetTrainSpeed() * moveDir;
    }

    void RenewRouteList()  //����·���б�
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

    void RenewRoute()  //���µ�ǰ�ڵ��·��
    {
        if (routeList.Count() > 0)
        {
            Vector2 tempPosition = routeList[0].GetCurrentPosition();
            Vector2 tempDir = routeList[0].GetCurrentDir();
            if (Vector2.Distance(tempPosition, transform.position) < 0.1f)
            {
                this.moveDir = tempDir;
                ChangeRotation(moveDir);  //�ı�ͼƬ����
                this.transform.position = tempPosition;
                routeList.RemoveAt(0);
                if (Vector2.Distance(preNode.transform.position, transform.position) != this.distace)  //ת�������
                    transform.position = preNode.transform.position - new Vector3(this.moveDir.x, this.moveDir.y, 0) * this.distace;
                this.SetRoute(new Route(this.transform.position, this.moveDir));  //����ת��ʱ����Ϣ
            }
        }
        /*
        if ((Vector2.Distance(preNode.transform.position, transform.position) - this.distace) > 0.05f)  //����
        {
            this.moveDir = train_head.GetComponent<TrainheadMovement>().GetDir();
            ChangeRotation(moveDir);  //�ı�ͼƬ����
            transform.position = preNode.transform.position - new Vector3(this.moveDir.x, this.moveDir.y, 0) * this.distace;
            //routeList.Clear();
        }
        */
    }

    void ChangeRotation(Vector2 moveDir)  //�ı䳯����
    {
        if (moveDir.Equals(new Vector2(0, 1)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);  //����ͼ�νǶ�
            return;
        }
        if (moveDir.Equals(new Vector2(-1, 0)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);  //����ͼ�νǶ�
            return;
        }
        if (moveDir.Equals(new Vector2(0, -1)))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);  //����ͼ�νǶ�
            return;
        }
        if (moveDir.Equals(new Vector2(1, 0)))
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);  //����ͼ�νǶ�
            return;
        }
    }

    public void SetRoute(Route new_route)  //route Set����
    {
        this.route = new_route;
        if(this.id<TrainManager.GetInstance().GetTrainLength()-2)
            this.onRouteSetted();
    }

    public Route GetRoute()  //route Get����
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
