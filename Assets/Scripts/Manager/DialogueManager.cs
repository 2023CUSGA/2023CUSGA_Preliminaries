using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    [Header("对话框组件")]
    public GameObject dialogueBox;  // 对话框
    public GameObject nameBar;  // 姓名条
    public Text conten;    // 对话内容
    public Text CharacterName;     // 角色名字
    public bool Active => dialogueBox.activeSelf;

    #region 文字效果
    int index;      // 当前行数
    List<string> textList = new List<string>();

    bool textFinish = true;    // 检查当前句子是否显示完全
    bool textAccelerate;    // 检查当前句子是否被加速看完

    [SerializeField]
    [Header("文字逐字出现的时间间隔")]
    private float textSpeed;
    WaitForSeconds waitForSeconds;
    #endregion

    #region 立绘显示
    [Header("立绘组件")]
    public Image imageLeft;    // 立绘左
    public Image imageRight;   // 立绘右

    [Header("立绘资源")]
    public Sprite avatar1;
    public Sprite avatar2;
    public Sprite avatar3;
    public Sprite avatar4;
    #endregion

    [HideInInspector]
    private System.Action endAction;
    public bool isTalking;

    private void Start()
    {
        waitForSeconds = new(textSpeed);    // 减少文字逐字显示时的GC
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (index >= textList.Count) // 关闭文本的条件更宽一点, 以免什么见鬼操作导致 index 直接超范围, 然后寄
            {
                dialogueBox.SetActive(false);
                isTalking = false;
                index = 0;
                var tempAction = endAction;
                endAction?.Invoke();
                // 说明在 Invoke 之后没有新增 endAction
                if (endAction == tempAction)
                    endAction = null;
                return;
            }

            if (textFinish && textList.Count != 0)  // 下一句话
            {
                StartCoroutine(SetTextUI());
            }
            else
            {
                textAccelerate = true;  // 对话没完成就再次按下对话键，则对话完全显示
            }
        }
    }

    /// <summary>
    /// UI整体的淡入效果
    /// </summary>
    IEnumerator FadeIn()
    {
        dialogueBox.GetComponent<CanvasGroup>().alpha = 0;
        while (dialogueBox.GetComponent<CanvasGroup>().alpha < 1)
        {
            dialogueBox.GetComponent<CanvasGroup>().alpha += 0.02f;
            yield return null;
        }
    }

    #region 对话框的各种打开方式
    /// <summary>
    /// 外部调用，带结束事件
    /// </summary>
    public void ShowInteractiveDialogue(string[] text, System.Action endAction = null)
    {
        this.endAction = endAction;

        ShowNormalDialogue(text);
    }

    /// <summary>
    /// 外部调用，不带结束事件
    /// </summary>
    public void ShowNormalDialogue(string[] text)
    {
        if (dialogueBox.activeSelf)
            return;
        if (text.Length == 0)
            return;


        GetTextFromFile(text);

        dialogueBox.SetActive(true);

        StartCoroutine(FadeIn());

        isTalking = true;

        StartCoroutine(SetTextUI());
    }

    #endregion


    /// <summary>
    /// 读取文本文件的内容
    /// </summary>
    public void GetTextFromFile(string[] text)
    {
        textList.Clear();
        index = 0;

        foreach (string textLine in text)
        {
            if (!string.IsNullOrEmpty(textLine)) 
                textList.Add(textLine.Trim());
        }
    }

    /// <summary>
    /// 文本框内容更新
    /// </summary>
    IEnumerator SetTextUI()
    {
        textFinish = false;
        textAccelerate = false;
        conten.text = "";

        #region 立绘切换
        switch (textList[index])
        {
            case "npc1：":
                CharacterName.text = "npc1";
                nameBar.SetActive(true);
                imageLeft.gameObject.SetActive(true);
                imageRight.gameObject.SetActive(false);
                imageLeft.sprite = avatar1;
                index++;
                break;
            case "npc2：":
                CharacterName.text = "npc2";
                nameBar.SetActive(true);
                imageRight.gameObject.SetActive(true);
                imageLeft.gameObject.SetActive(false);
                imageRight.sprite = avatar2;
                index++;
                break;
            case "n":
                CharacterName.text = "None";
                nameBar.SetActive(false);    // 旁白不显示名字条UI
                imageLeft.gameObject.SetActive(false);
                imageRight.gameObject.SetActive(false);
                index++;
                break;
        }
        #endregion

        #region 文字效果
        for (int i = 0; i < textList[index].Length; i++)
        {
            conten.text += textList[index][i];
            yield return waitForSeconds;

            if (textAccelerate == true)  // 如果中途再次按下对话键则显示全部内容
            {
                conten.text = textList[index];
                break;
            }
        }
        #endregion

        textFinish = true;
        index++;
    }

}

