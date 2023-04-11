using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TrackTrigger : MonoBehaviour
{
    public GameObject targetTrain;
    private Collider2D trigger;
    private EnemyBase parent;
    public float timer;
    public float waitTimer = 5f;

    Stack<Vector3Int> trace = new Stack<Vector3Int>();

    private void OnEnable()
    {
        trigger = GetComponent<Collider2D>();
        trigger.enabled = true;

    }

    private void Start()
    {
        parent = GetComponentInParent<EnemyBase>();

    }


    void FixedUpdate()
    {
        if (targetTrain != null)
        {
            if (timer >= waitTimer)
            {
                timer = 0;
                parent.RePathFinding(targetTrain.transform);
            }
            else
                timer += Time.fixedDeltaTime;

            parent.Move(targetTrain.transform.position);    // 通知敌人追击列车
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TrainHead") || collision.CompareTag("TrainBody"))
        {
            targetTrain = collision.gameObject;
            trigger.enabled = false;
        }
    }

}
