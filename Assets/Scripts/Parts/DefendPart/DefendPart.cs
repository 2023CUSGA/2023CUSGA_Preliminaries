using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendPart : PartBase
{
    private PartNames part;
    public void SetPartType(PartNames type) { part = type; }

    private TrainBody trainBodyData;

    public void AddHP()
    {
        float hp = trainBodyData.GetHealth();
        switch (part)
        {
            case PartNames.木制挡板:
                {
                    hp=1.1f*hp;
                    break;
                }
            case PartNames.铁皮挡板:
                {
                    hp = 1.25f * hp;
                    break;
                }
            case PartNames.合金挡板:
                {
                    hp = 1.3f * hp;
                    break;
                }
            default:break;
        }
        trainBodyData.SetHealth(hp);
    }

    private void Start()
    {
        trainBodyData = gameObject.GetComponent<TrainBody>();
        AddHP();
    }
}