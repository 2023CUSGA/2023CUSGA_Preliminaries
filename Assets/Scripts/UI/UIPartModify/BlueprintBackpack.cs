using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintBackpack : MonoBehaviour
{
    [SerializeField]
    private GameObject prefeb;

    private void Start()
    {

        //测试用
        //CHEAT();
        //测试用

        Dictionary<PartNames, int> blueprints = ResourceDataContainer.GetAllBlueprints();
        foreach (var blueprint in blueprints)
        {
            if (blueprint.Value > 0)
            {
                GameObject instance = Instantiate(prefeb, transform);
                instance.GetComponent<BlueprintSelect>().SetType(blueprint.Key);
                //TODO: 设置图纸图片
                //instance.GetComponent<Image>().sprite = null;
                instance.transform.GetChild(0).GetComponent<TMP_Text>().text = blueprint.Value.ToString();
            }
        }
    }

    //测试用
    //凭空生成图纸和材料
    private void CHEAT()
    {
        foreach(string name in System.Enum.GetNames(typeof(ResourseNames)))
        {
            ResourceDataContainer.IncreaseResourceQuantity((ResourseNames)System.Enum.Parse(typeof(ResourseNames), name), 999);
        }
        foreach (string name in System.Enum.GetNames(typeof(PartNames)))
        {
            ResourceDataContainer.IncreaseBlueprintCount((PartNames)System.Enum.Parse(typeof(PartNames), name), 9);
        }
    }
}
