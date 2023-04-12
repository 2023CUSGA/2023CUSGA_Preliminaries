using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintCraft : MonoBehaviour
{
    private List<KeyValuePair<ResourseNames, int>> requirement;
    public void SetRequirement(List<KeyValuePair<ResourseNames, int>> requirement) { this.requirement = requirement; }

    private PartNames part;
    public void SetPart(PartNames part) { this.part = part; }

    private string discription;
    public void SetDiscription(string discription) {  this.discription = discription; }

    [SerializeField]
    private GameObject prefeb;

    public void SetUI()
    {
        bool valid = true;

        int index;
        for (index = 0; index < requirement.Count-1; index++)
        {
            GameObject instance = Instantiate(prefeb, transform.GetChild(1).GetChild(0));
            //TODO: 设置材料图片
            //instance.GetComponent<Image>().sprite = null;
            instance.transform.GetChild(0).GetComponent<TMP_Text>().text
                = ResourceDataContainer.GetResourceQuantity(requirement[index].Key).ToString()
                + " / "
                + requirement[index].Value.ToString();
            if (ResourceDataContainer.GetResourceQuantity(requirement[index].Key) < requirement[index].Value)
                valid = false;
        }

        //TODO: 设置配件图片
        //transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = null;

        transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text
            = "消耗金币：" + requirement[index].Value.ToString();
        if (ResourceDataContainer.GetResourceQuantity(requirement[index].Key) < requirement[index].Value)
            valid = false;

        if (valid)
        {
            transform.GetChild(1).GetChild(3).GetComponent<Button>().interactable = true;
        }
        else
        {
            transform.GetChild(1).GetChild(3).GetComponent<Button>().interactable = false;
        }

        transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "配件名：" + part.ToString();

        transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = discription;
    }

    public void ResetUI()
    {
        Transform materials = transform.GetChild(1).GetChild(0);
        for (int i = 0; i < materials.childCount; i++)
        {
            Destroy(materials.GetChild(i).gameObject);
        }

        transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = null;

        transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = "消耗金币：0";

        transform.GetChild(1).GetChild(3).GetComponent<Button>().interactable = false;

        transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = "配件名：";

        transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>().text = "功能说明";
    }

    public void OnClick()
    {
        foreach (var req in requirement)
        {
            ResourceDataContainer.DecreaseResourceQuantity(req.Key, req.Value);
        }
        ResourceDataContainer.IncreasePartCount(part, 1);
        ResetUI();
    }
}
