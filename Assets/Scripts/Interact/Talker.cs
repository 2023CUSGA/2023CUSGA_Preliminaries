using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Interact
{
    public class Talker : MonoBehaviour
    {
        public TextAsset talkTextFile;

        public void Start()
        {
            DialogueManager.Instance.ShowNormalDialogue(talkTextFile);
        }


    }
}
