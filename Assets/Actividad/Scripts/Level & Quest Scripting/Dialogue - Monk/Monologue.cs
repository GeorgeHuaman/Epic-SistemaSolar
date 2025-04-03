using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monologue : MonoBehaviour
{
    [System.Serializable]
    public struct Message
    {
        public string messageText;
        public float durationSeconds;
    }

    [Header("Monologue Parameters")]
    [SerializeField] public Message[] monologue;
    [SerializeField] private float timeBetweenMessages;
    [SerializeField] private GameObject interactable;

    [Header("States")]
    [SerializeField] private bool isTalking;
    [SerializeField] private int actualMessage;
    [SerializeField] private float messageTimer;

    private void Start()
    {
        actualMessage = 0;
        isTalking = false;
    }

    private void Update()
    {
        if (isTalking == false) return;

        if(messageTimer > 0) messageTimer -= Time.deltaTime;
        else
        {
            TalkNext();
        }
    }

    public void StartTalking()
    {
        if (isTalking) return;

        interactable.SetActive(false);

        isTalking = true;
        TalkNext();
    }

    private void TalkNext()
    {
        if (isTalking == false) return;

        if(actualMessage >= monologue.Length)
        {
            interactable.SetActive(true);

            actualMessage = 0;
            isTalking = false;
            return;
        }

        SpatialBridge.coreGUIService.DisplayToastMessage(monologue[actualMessage].messageText, monologue[actualMessage].durationSeconds);
        messageTimer = monologue[actualMessage].durationSeconds + timeBetweenMessages;
        actualMessage++;
    }
}
