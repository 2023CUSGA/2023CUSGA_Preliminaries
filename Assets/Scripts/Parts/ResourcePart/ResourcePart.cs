using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePart : PartBase
{
    private PartNames part;
    public void SetPartType(PartNames type) { part = type; }

    private TrainBody trainBodyData;

    public void SetEffeciency()
    {
        float effeciency = trainBodyData.GetGatheringSpeed();
        switch (part)
        {
            case PartNames.采集架:
                {
                    effeciency *= 1.2f;
                    break;
                }
            case PartNames.采集强化器:
                {
                    effeciency *= 1.4f;
                    break;
                }
            case PartNames.超采集器:
                {
                    effeciency *= 1.6f;
                    break;
                }
            default:break;
        }
        trainBodyData.SetGatheringSpeed(effeciency);
    }

    private void Start()
    {
        trainBodyData = gameObject.GetComponent<TrainBody>();
        SetEffeciency();
    }
}
