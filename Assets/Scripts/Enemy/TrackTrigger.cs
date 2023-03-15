using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTrigger : MonoBehaviour
{
    public GameObject targetTrain;

    private EnemyBase parent;

    private void Start()
    {
        parent = GetComponentInParent<EnemyBase>();

    }

    void Update()
    {
        if (targetTrain != null)
        {
            parent.Move(targetTrain.transform.position);    // 通知敌人追击列车
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TrainHead"))
        {
            targetTrain = collision.gameObject;
        }
    }

    //public void ResumeTransform()
    //{
    //    this.transform.position = parent.transform.position;
    //    this.transform.rotation = parent.transform.rotation;
    //}
}
