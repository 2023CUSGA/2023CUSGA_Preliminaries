using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePart : PartBase
{
    private PartNames part;
    public void SetPartType(PartNames type) { part = type; }

    //如何获取并修改采集效率？
    public void SetEffeciency()
    {
        float effeciency;
        switch (part)
        {
            case PartNames.采集架:
                {
                    //effeciency *= 1.2f;
                    break;
                }
            case PartNames.采集强化器:
                {
                    //effeciency *= 1.4f;
                    break;
                }
            case PartNames.超采集器:
                {
                    //effeciency *= 1.6f;
                    break;
                }
            default:break;
        }
    }
}
