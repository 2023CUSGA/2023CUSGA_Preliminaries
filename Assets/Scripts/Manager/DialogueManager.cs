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
    public bool isSelecting;

    private void Start()
    {
        waitForSeconds = new(textSpeed);    // 减少文字逐字显示时的GC
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (index >= textList.Count || textList[index - 1].Contains("end")) // 对话结束
            {
                dialogueBox.SetActive(false);
                index = 0;
                var tempAction = endAction;
                endAction?.Invoke();
                // 说明在 Invoke 之后没有新增 endAction
                if (endAction == tempAction)
                    endAction = null;
                return;
            }

            if (textFinish && textList.Count != 0 && !isSelecting)  // 下一句话
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
    public void ShowInteractiveDialogue(TextAsset text, System.Action endAction = null)
    {
        this.endAction = endAction;

        ShowNormalDialogue(text);
    }

    /// <summary>
    /// 外部调用，不带结束事件
    /// </summary>
    public void ShowNormalDialogue(TextAsset textFile)
    {
        if (dialogueBox.activeSelf)
            return;
        //if (text.Length == 0)
        //    return;


        GetTextFromFile(textFile);

        dialogueBox.SetActive(true);

        StartCoroutine(FadeIn());

        StartCoroutine(SetTextUI());
    }

    #endregion


    /// <summary>
    /// 读取文本文件的内容
    /// </summary>
    public void GetTextFromFile(TextAsset textFile)
    {
        textList.Clear();
        index = 0;
        string[] lineData = textFile.text.Split('\n');

        foreach (string textLine in lineData)
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
            case "帕切拉：":
                CharacterName.text = "帕切拉";
                imageLeft.sprite = avatar1;
                index++;
                break;
            case "难民2：":
                CharacterName.text = "难民2";
                imageRight.sprite = avatar2;
                index++;
                break;
            case "n":
                CharacterName.text = "None";
                index++;
                break;
        }
        #endregion


        #region 文字效果
        for (int i = 0; i < textList[index].Length; i++)
        {
            if (textList[index].StartsWith("@"))
            {
                string text1 = textList[index].Substring(3);
                int text1Index = int.Parse(textList[index].Substring(1, 2));
                index++;

                string text2 = textList[index].Substring(3);
                int text2Index = int.Parse(textList[index].Substring(1, 2));
                index++;

                string text3 = textList[index].Substring(3);
                int text3Index = int.Parse(textList[index].Substring(1, 2));

                UISelectWindow selectWindow = UIWindowManager.Instance.ShowSelectWindow(text1, text2, text3);   // 打开选择框
                selectWindow.Action1 += () => { ContinueTalk(text1Index); };
                selectWindow.Action2 += () => { ContinueTalk(text2Index); };
                selectWindow.Action3 += () => { ContinueTalk(text3Index); };

                isSelecting = true;
                textFinish = true;

                yield return null;
            }
            else
            {
                conten.text += textList[index][i];
                yield return waitForSeconds;

                if (textAccelerate == true)  // 如果中途再次按下对话键则显示全部内容
                {
                    conten.text = textList[index];
                    break;
                }
            }
        }
        #endregion

        textFinish = true;
        index++;
    }

    void ContinueTalk(int textIndex)
    {
        StopCoroutine(SetTextUI());
        index = textIndex - 1;
        isSelecting = false;
        StartCoroutine(SetTextUI());
    }
}

