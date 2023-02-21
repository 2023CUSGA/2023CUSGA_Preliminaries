using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interact
{
    public class Talker : MonoBehaviour
    {
        [TextArea(1, 5), Tooltip("正常对话")] public string[] normalText;
        [TextArea(1, 4), Tooltip("特殊对话")] public string[] specialText;

        public UnityEvent beforeTalkAction;
        public UnityEvent afterTalkAction;


        public void OnInteract()
        {
            beforeTalkAction?.Invoke();
            DialogueManager.Instance.ShowNormalDialogue(normalText);
            DialogueManager.Instance.ShowInteractiveDialogue(normalText, () => afterTalkAction?.Invoke());
        }

    }
}
