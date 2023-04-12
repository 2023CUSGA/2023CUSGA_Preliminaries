using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BlueprintSelect : MonoBehaviour
{
    private PartNames type;
    public void SetType(PartNames type) { this.type = type; }

    private BlueprintCraft craft;
    private string[] title;
    private string[] data;

    private void Start()
    {
        craft = GameObject.Find("Canvas/Image_图纸").GetComponent<BlueprintCraft>();
        string path = Path.Combine(Application.dataPath, "Scripts", "Parts", "图纸.csv");
        string[] csv = File.ReadAllLines(path);
        title = csv[0].Split(',');
        foreach (string line in csv)
        {
            data = line.Split(',');
            if (data[0].Equals(type.ToString()))
                break;
        }
    }

    public void OnClick()
    {
        craft.ResetUI();
        List<KeyValuePair<ResourseNames, int>> requirement = new List<KeyValuePair<ResourseNames, int>>();
        for(int i = 1; i < data.Length-1; i++)
        {
            if (!data[i].Equals("0"))
            {
                KeyValuePair<ResourseNames, int> e = new KeyValuePair<ResourseNames, int>
                    ((ResourseNames)System.Enum.Parse(typeof(ResourseNames), title[i]),
                    int.Parse(data[i]));
                requirement.Add(e);
            }
        }
        craft.SetRequirement(requirement);
        craft.SetPart(type);
        craft.SetDiscription(data[^1]);
        craft.SetUI();
    }
}
