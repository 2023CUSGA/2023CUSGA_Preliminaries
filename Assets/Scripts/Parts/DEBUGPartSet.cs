using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGPartSet : MonoBehaviour
{
    private List<PartNames> GetRandomParts()
    {
        List<PartNames> parts = new List<PartNames>();
        int num = Random.Range(0, 3 + 1);
        while (num > 0)
        {
            int type = Random.Range(1, 3 + 1);
            switch (type)
            {
                case 1:
                    {
                        if (!parts.Contains(PartNames.弓弩))
                        {
                            parts.Add(PartNames.弓弩);
                            num--;
                        }
                        break;
                    }
                case 2:
                    {
                        if (!parts.Contains(PartNames.石弹枪))
                        {
                            parts.Add(PartNames.石弹枪);
                            num--;
                        }
                        break;
                    }
                case 3:
                    {
                        if (!parts.Contains(PartNames.地刺))
                        {
                            parts.Add(PartNames.地刺);
                            num--;
                        }
                        break;
                    }
                default:break;
            }
        }
        return parts;
    }

    private void Awake()
    {
        for(int i = 0; i < 7; i++)
        {
            TrainPartDataContainer.AddTrainPart();
            TrainPartDataContainer.ModifyTrainPart(GetRandomParts(), i);
        }
        Destroy(gameObject);
    }
}
