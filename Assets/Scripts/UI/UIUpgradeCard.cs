using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIUpgradeCard :MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]private int upgradeType;  //升级的类型 1.毁灭 2.弹力 3.迅捷

    void Awake()
    {
        this.GetComponent<Button>().onClick.AddListener(
            ()=>TrainManager.GetInstance().SetTrainHeadType(upgradeType));
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        this.transform.SetAsLastSibling();
        this.transform.localScale = new Vector3(1.1f,1.1f,1.1f);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        this.transform.SetAsFirstSibling();
        this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
