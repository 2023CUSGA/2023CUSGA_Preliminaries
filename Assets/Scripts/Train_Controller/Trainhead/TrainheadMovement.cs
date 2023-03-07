using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainheadMovement : MonoBehaviour
{
    [Header("������˶��ٶ�")]
    [SerializeField] private float moveSpeed = 5.0f;  //�ƶ��ٶ�
    [Header("�𳵼��ٶ�")]
    [SerializeField] private float accelerateedSpeed = 1.0f;  //���ٶ� 
    [Header("�𳵸���")]
    [SerializeField] private Rigidbody2D trainhead_RB;  //�𳵸���

    protected Vector2 moveDir = new Vector2(0, 1);  //�����з���
    private bool isMoving = false;  //�Ƿ����˶���
    private float trainSpeed = 0;  //�𳵵�ǰ���ٶ�
    private int train_rotation = 0;  //�𳵵ĳ���
    private Route route;  //ת����Ϣ

    public event Action onRouteSetted;  //����·����Ϣʱ���¼�

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        MovingFunc();
        DirFunc();
    }

    // Update is called once per frame
    void Update()
    {
        StartAndStop();
    }

    private void StartAndStop()  //������ͣ������
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isMoving == true)
                isMoving = false;
            else
                isMoving = true;
        }
    }

    private void MovingFunc()  //���ƶ�����
    {
        if (isMoving)  //�����������״̬
        {
            //������������
            if (trainSpeed != moveSpeed)
            {
                if (trainSpeed < moveSpeed)  //�ٶȲ��������ٶ�
                    trainSpeed += accelerateedSpeed * Time.fixedDeltaTime;
                else  //�ٶȵ���򳬹�����ٶȣ����ٶȸ�ֵΪ����ٶ�
                    trainSpeed = moveSpeed;
                trainhead_RB.velocity = moveDir * trainSpeed;
            }
        }
        else
        {
            //�����ƶ�����
            if (trainSpeed != 0)
            {
                if (trainSpeed > 0)  //�����ٶȣ�����
                    trainSpeed -= accelerateedSpeed * Time.fixedDeltaTime;
                else
                    trainSpeed = 0;  //�ٶ�С�ڵ���0�����ٶȸ�ֵΪ0
                trainhead_RB.velocity = moveDir * trainSpeed;
            }
        }
    }

    void DirFunc()  //ת����
    {
        transform.rotation = Quaternion.Euler(0, 0, train_rotation);  //����ͼ�νǶ�
        if (Input.GetKey(KeyCode.W) && moveDir.y != (-1))  //������¡�w�����һ𳵷���������
        {
            moveDir = new Vector2(0, 1);  //����
            train_rotation = 0;  //ת��
            trainhead_RB.velocity = trainSpeed * moveDir;
            this.SetRoute(new Route(transform.position, moveDir));  //����ת��ʱ����Ϣ
        }
        if (Input.GetKey(KeyCode.A) && moveDir.x != 1)
        {
            moveDir = new Vector2(-1, 0);  //����
            train_rotation = 90;  //ת��
            trainhead_RB.velocity = trainSpeed * moveDir;
            this.SetRoute(new Route(transform.position, moveDir));  //����ת��ʱ����Ϣ
        }
        if (Input.GetKey(KeyCode.S) && moveDir.y != 1)
        {
            moveDir = new Vector2(0, -1);  //����
            train_rotation = 180;  //ת��
            trainhead_RB.velocity = trainSpeed * moveDir;
            this.SetRoute(new Route(transform.position, moveDir));  //����ת��ʱ����Ϣ
        }
        if (Input.GetKey(KeyCode.D) && moveDir.x != (-1))
        {
            moveDir = new Vector2(1, 0);  //���
            train_rotation = -90;  //ת��
            trainhead_RB.velocity = trainSpeed * moveDir;
            this.SetRoute(new Route(transform.position, moveDir));  //����ת��ʱ����Ϣ
        }
    }

    public Vector2 GetDir()  //��ȡ��ͷ����
    {
        return moveDir;
    }

    public float GetTrainSpeed()  //��ȡ�𳵵��ٶ�
    {
        return this.trainSpeed;
    }

    public void SetRoute(Route new_route)  //route Set����
    {
        this.route = new_route;
        this.onRouteSetted();
    }

    public Route GetRoute()  //route Get����
    {
        return this.route;
    }
}
