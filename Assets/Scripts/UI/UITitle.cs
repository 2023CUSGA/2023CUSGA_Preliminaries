using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitle : MonoBehaviour
{
    public GameObject settingPanel;

    private void Start()
    {
        SoundManager.Instance.Init();
        SoundManager.Instance.PlayMusic(SoundDefine.Music_Ttile);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
        SoundManager.Instance.PlaySound(SoundDefine.Sound_UIClick);
    }

    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
        SoundManager.Instance.PlaySound(SoundDefine.Sound_UIClick);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void Exit()
    {
        SoundManager.Instance.PlaySound(SoundDefine.Sound_UIClick);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
    }









}
