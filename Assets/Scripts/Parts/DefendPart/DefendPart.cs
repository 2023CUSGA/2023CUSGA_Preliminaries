using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendPart : PartBase
{
    private PartNames part;
    public void SetPartType(PartNames type) { part = type; }

    //如何获取并修改hp?
    public void AddHP()
    {
        float hp;
        switch (part)
        {
            case PartNames.木制挡板:
                {
                    //hp=1.1f*hp;
                    //SetHP(hp);
                    break;
                }
            case PartNames.铁皮挡板:
                {
                    //hp = 1.25f * hp;
                    //SetHP(hp);
                    break;
                }
            case PartNames.合金挡板:
                {
                    //hp = 1.3f * hp;
                    //SetHP(hp);
                    break;
                }
            default:break;
        }
    }
}