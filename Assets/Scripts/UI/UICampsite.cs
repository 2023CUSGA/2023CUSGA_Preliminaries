using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UICampsite : MonoBehaviour
{
    public TextMeshProUGUI goldNum;

    private void Start()
    {
        SoundManager.Instance.PlayMusic(SoundDefine.Music_CampsiteScene);
        goldNum.text = ResourceDataContainer.GetResourceQuantity(ResourseNames.金币).ToString();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ButtonClickSound()
    {
        SoundManager.Instance.PlaySound(SoundDefine.Sound_UIClick);
    }





}
