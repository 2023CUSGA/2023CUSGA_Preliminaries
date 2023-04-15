using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGamePlay : MonoBehaviour
{

    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject guidePanel;
    public Text killNum;
    public Text goldNum;
    public TextMeshProUGUI goldNum2;
    public TextMeshProUGUI addGoldNum;

    private void Start()
    {
        EnvironmentManager.instance.gameWin = GameWin;
        EnvironmentManager.instance.addGold = AddGold;
        TrainManager.GetInstance().onFailedAction += GameLose;
        goldNum2.text = ResourceDataContainer.GetResourceQuantity(ResourseNames.金币).ToString();
        SoundManager.Instance.PlayMusic(SoundDefine.Music_MainScene);

        if (!PlayerPrefs.HasKey("levelNum") )
        {
            guidePanel.SetActive(true);
            //Time.timeScale = 0;
            return;
        }
        if (PlayerPrefs.HasKey("levelNum") && 1 == PlayerPrefs.GetInt("levelNum"))
        {
            guidePanel.SetActive(true);
            //Time.timeScale = 0;

        }

    }

    void AddGold(int num)
    {
        goldNum2.text = ResourceDataContainer.GetResourceQuantity(ResourseNames.金币).ToString();
        addGoldNum.text = " +" + num;
        //addGoldNum.DOFade(1f, 1f).onComplete = () => addGoldNum.DOFade(0, 2f);
    }


    void GameWin()
    {
        SoundManager.Instance.PlayMusic(SoundDefine.Music_Victory);

        PlayerPrefs.SetInt("levelNum", PlayerPrefs.GetInt("levelNum") + 1);
        winPanel.SetActive(true);
        killNum.text = EnvironmentManager.instance.EnemyKillNum.ToString();
        goldNum.text = ResourceDataContainer.GetResourceQuantity(ResourseNames.金币).ToString();
        Time.timeScale = 0;
    }
    void GameLose()
    {
        SoundManager.Instance.PlayMusic(SoundDefine.Music_Failure);

        TrainManager.GetInstance().onFailedAction -= GameLose;
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReStartThisLevel()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlaySound(SoundDefine.Sound_UIClick);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToCampsite()
    {
        Time.timeScale = 1;
        SoundManager.Instance.PlaySound(SoundDefine.Sound_UIClick);

        SceneManager.LoadScene("Campsite");
    }


}
