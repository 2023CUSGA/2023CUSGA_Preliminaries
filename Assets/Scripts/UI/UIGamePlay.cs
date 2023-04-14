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
    }

    void AddGold(int num)
    {
        goldNum.text = ResourceDataContainer.GetResourceQuantity(ResourseNames.金币).ToString();
        addGoldNum.text = " +" + num;
        addGoldNum.DOFade(1f, 1f).onComplete = () => addGoldNum.DOFade(0, 2f);
    }


    void GameWin()
    {
        PlayerPrefs.SetInt("levelNum", PlayerPrefs.GetInt("levelNum") + 1);
        killNum.text = EnvironmentManager.instance.EnemyKillNum.ToString();
        goldNum2.text = ResourceDataContainer.GetResourceQuantity(ResourseNames.金币).ToString();
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
    void GameLose()
    {
        TrainManager.GetInstance().onFailedAction -= GameLose;
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReStartThisLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToCampsite()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Campsite");
    }


}
