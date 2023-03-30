using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrainBodyMov : TrainBody
{
    private GameObject train_head;  //��ͷ����
    [SerializeField]private Vector2 moveDir;  //��ǰ�ڵ��ƶ�����
    private Rigidbody2D trainbody_RB;  //��ǰ�ڵ�ĸ���
    private GameObject childNode;  //�ӽڵ���Ϸ����
    private List<Route> routeList = new List<Route>();  //ת��ڵ�list
    private GameObject preNode;  //ǰһ�ڳ��������
    private Route route;  //ת����Ϣ
    private float distance;  //��ǰһ�ڳ���ľ���

    public event Action onRouteSetted;  //����·����Ϣʱ���¼�

    void Start()
    {
        childNode = this.transform.GetChild(0).gameObject;  //��ȡ�ӽڵ���Ϸ����
        train_head = GameObject.Find("train_head");  //��ȡ������

        if (this.id == 0)  //��ȡǰһ�ڵ�����
            preNode = train_head;
        else
            preNode = GameObject.Find("train_body" + (id - 1));

        moveDir = train_head.GetComponent<TrainheadMov>().GetDir();  //��ȡ��ǰ�ķ���
        trainbody_RB = this.GetComponent<Rigidbody2D>();  //��ȡ����

        if (this.id == 0)  //��������·���¼�
            preNode.GetComponent<TrainheadMov>().onRouteSetted += this.RenewRouteList;
        else
            preNode.GetComponent<TrainBodyMov>().onRouteSetted += this.RenewRouteList;

        this.distance = Vector2.Distance(preNode.transform.position, transform.position);  //��������һ�ڵ�ľ���
    }

    private void FixedUpdate()
    {
        RenewRoute();
    }

    void Update()
    {
        //RenewRoute();
    }

    void MoveFunc()  //�ƶ�����
    {
        this.transform.position = Vector2.MoveTowards(transform.position,childNode.transform.position, train_head.GetComponent<TrainheadMov>().GetTrainSpeed()*Time.fixedDeltaTime);
    }

    void RenewRouteList()  //����·���б�
    {
        Route tempRoute;
        if (this.id == 0)
            tempRoute = preNode.GetComponent<TrainheadMov>().GetRoute();
        else
            tempRoute = preNode.GetComponent<TrainBodyMov>().GetRoute();

        if (tempRoute.GetIsVaild())
        {
            routeList.Add(tempRoute);
        }
        return;
    }

    void RenewRoute()  //���µ�ǰ�ڵ��·��
    {
        if (routeList.Count() > 0)
        {
            Vector2 tempPosition = routeList[0].GetCurrentPosition();
            Vector2 tempDir = routeList[0].GetCurrentDir();
            this.transform.position = Vector2.MoveTowards(transform.position, tempPosition, train_head.GetComponent<TrainheadMov>().GetTrainSpeed()*Time.fixedDeltaTime);
            if(Vector2.Distance(tempPosition, transform.position) < 0.02f)
            {
                this.moveDir = tempDir;
                ChangeRotation(moveDir);  //�ı�ͼƬ����
                this.transform.position = tempPosition;
                this.SetRoute(new Route(this.transform.position, this.moveDir));  //����ת��ʱ����Ϣ
                routeList.RemoveAt(0);
                if (Vector3.Distance(transform.position, preNode.transform.position) != distance)
                {
                    Debug.Log("����λ��");
                    transform.position = new Vector3(preNode.transform.position.x - distance * moveDir.x,
                                                     preNode.transform.position.y - distance * moveDir.y, 0);
                }
                //Time.timeScale = 0;
            }

        }
        else
            MoveFunc();
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
        if (this.id < TrainManager.GetInstance().GetTrainLength() - 2)
            this.onRouteSetted();
    }

    public Route GetRoute()  //route Get����
    {
        return this.route;
    }
}
