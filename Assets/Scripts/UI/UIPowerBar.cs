using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPowerBar : MonoBehaviour
{
    private float trainhead_power = 0;
    
    void Start()
    {
        trainhead_power = TrainManager.GetInstance().GetEnergy() * 0.01f;
        this.transform.localScale = new Vector3(trainhead_power, 1, 1);    
    }

    
    void Update()
    {
        trainhead_power = TrainManager.GetInstance().GetEnergy() * 0.01f;
        this.transform.localScale = new Vector3(trainhead_power, 1, 1);
    }
}
