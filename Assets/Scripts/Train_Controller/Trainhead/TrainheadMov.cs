using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainheadMov : MonoBehaviour
{
    [Header("火车最大运动速度")]
    [SerializeField] private float moveSpeed = 3.0f;  //移动速度
    [Header("火车加速度")]
    [SerializeField] private float accelerateedSpeed = 1.0f;  //加速度 
    [Header("火车刚体")]
    [SerializeField] private Rigidbody2D trainhead_RB;  //火车刚体

    protected Vector2 moveDir = new Vector2(0, 1);  //火车运行方向
    private GameObject wayPoint; //火车头路径点
    private bool isMoving = false;  //是否在运动中
    [SerializeField]private bool isDizzy = false;  //是否在晕眩中
    [SerializeField]private bool canMove = true;  //火车能否启动
    private float trainSpeed = 0;  //火车当前的速度
    private int train_rotation = 0;  //火车的朝向
    private Route route;  //转向信息

    public event Action onRouteSetted;  //发送路径信息时的事件

    void Start()
    {
        trainhead_RB = this.gameObject.GetComponent<Rigidbody2D>();
        wayPoint = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        MovingFunc();
    }

    void Update()
    {
        StartAndStop();
        DirFunc();
    }

    private void StartAndStop()  //启动和停车函数
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (canMove && !isDizzy)  //如果可以运动
            {
                if (isMoving == true)
                    isMoving = false;
                else
                    isMoving = true;
            }
            else  //不能运动的情况：即仍然撞在障碍物上
            {
                isMoving = false;
                if(!isDizzy)
                    DizzyFunc();
            }
        }
    }

    private void MovingFunc()  //火车移动函数
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint.transform.position, trainSpeed * Time.fixedDeltaTime);
        if (isMoving)  //如果处于启动状态
        {
            //加速启动部分
            if (trainSpeed != moveSpeed)
            {
                if (trainSpeed < moveSpeed)  //速度不够，加速度
                    trainSpeed += accelerateedSpeed * Time.fixedDeltaTime;
                else  //速度到达或超过最大速度，将速度赋值为最大速度
                    trainSpeed = moveSpeed;
            }
        }
        else
        {
            //减速制动部分
            if (trainSpeed != 0)
            {
                if (trainSpeed > 0)  //还有速度，减速
                    trainSpeed -= accelerateedSpeed * Time.deltaTime;
                else
                    trainSpeed = 0;  //速度小于等于0，将速度赋值为0
            }
        }
    }

    void DirFunc()  //转向函数
    {
        if (
            ((Input.GetKeyDown(KeyCode.W) && !TrainManager.GetInstance().GetIsPerversion()) ||
            (Input.GetKeyDown(KeyCode.S) && TrainManager.GetInstance().GetIsPerversion())) 
            && moveDir.y != (-1))  //如果按下“w”键且火车方向不是向下
        {
            moveDir = new Vector2(0, 1);  //向上
            train_rotation = 0;  //转向
            this.SetRoute(new Route(transform.position, moveDir));  //设置转向时的信息
            transform.rotation = Quaternion.Euler(0, 0, train_rotation);  //调整图形角度
        }

        if (
            ((Input.GetKeyDown(KeyCode.A) && !TrainManager.GetInstance().GetIsPerversion()) ||
            (Input.GetKeyDown(KeyCode.D) && TrainManager.GetInstance().GetIsPerversion()))
            && moveDir.x != 1)
        {
            moveDir = new Vector2(-1, 0);  //向左
            train_rotation = 90;  //转向
            this.SetRoute(new Route(transform.position, moveDir));  //设置转向时的信息
            transform.rotation = Quaternion.Euler(0, 0, train_rotation);  //调整图形角度
        }

        if (
            ((Input.GetKeyDown(KeyCode.S) && !TrainManager.GetInstance().GetIsPerversion()) ||
            (Input.GetKeyDown(KeyCode.W) && TrainManager.GetInstance().GetIsPerversion()))
            && moveDir.y != 1)
        {
            moveDir = new Vector2(0, -1);  //向下
            train_rotation = 180;  //转向
            this.SetRoute(new Route(transform.position, moveDir));  //设置转向时的信息
            transform.rotation = Quaternion.Euler(0, 0, train_rotation);  //调整图形角度
        }

        if (
            ((Input.GetKeyDown(KeyCode.D) && !TrainManager.GetInstance().GetIsPerversion()) ||
            (Input.GetKeyDown(KeyCode.A) && TrainManager.GetInstance().GetIsPerversion()))
            && moveDir.x != (-1))
        {
            moveDir = new Vector2(1, 0);  //向后
            train_rotation = -90;  //转向
            this.SetRoute(new Route(transform.position, moveDir));  //设置转向时的信息
            transform.rotation = Quaternion.Euler(0, 0, train_rotation);  //调整图形角度
        }
    }

    public Vector2 GetDir()  //获取火车头朝向
    {
        return moveDir;
    }

    public float GetTrainSpeed()  //获取火车的速度
    {
        return this.trainSpeed;
    }

    public void SetRoute(Route new_route)  //route Set方法
    {
        this.route = new_route;
        Debug.Log("Route: " + new_route.GetCurrentPosition());
        this.onRouteSetted();
    }

    public Route GetRoute()  //route Get方法
    {
        return this.route;
    }

    public bool GetIsMoving()  //isMoving Get方法
    {
        return this.isMoving;
    }

    public bool GetIsDizzy()
    {
        return this.isDizzy;
    }

    private void OnCollisionEnter2D(Collision2D collision)  //碰撞检测函数
    {
        if (collision.gameObject.tag == "RoadBlock" || collision.gameObject.tag == "ResoursePoint" || collision.gameObject.tag == "TrianBody")
        {
            this.trainSpeed = 0;
            trainhead_RB.velocity = moveDir * trainSpeed;
            DizzyFunc();  //晕眩函数
        }
        if(collision.gameObject.tag=="enemy")
        {
            TrainManager.GetInstance().OnCrashEnermy();
            Debug.Log("撞击敌人");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RoadBlock" || collision.gameObject.tag == "ResoursePoint" || collision.gameObject.tag == "TrianBody")  //仍然在障碍物上
            canMove = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RoadBlock" || collision.gameObject.tag == "ResoursePoint" || collision.gameObject.tag == "TrianBody")  //离开障碍物检测
            canMove = true;  //火车允许启动
    }

    public void DizzyFunc()  //晕眩函数
    {
        canMove = false;
        isMoving = false;
        trainSpeed = 0;
        isDizzy = true;
        StartCoroutine(DizzyFor3Second());
    }

    IEnumerator DizzyFor3Second()
    {
        int seconds = 0;
        while(true)
        {
            Debug.Log("晕眩" + (seconds++) + "秒");
            if(seconds >= 3)
            {
                isDizzy = false;
                canMove = true;
                yield break;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
