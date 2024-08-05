using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SpeechBubbleAssistant : MonoBehaviour
{
    private TextWriter.TextWriterSingle textWriterSingle;
    [Header("Start On Awake")]
    [SerializeField] private bool startOnAwake = true;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI textComponent;

    [Header("Audio")]
    [SerializeField] private AudioSource talkingAudioSource;

    [Header("Character")]
    [SerializeField] private GameObject npc;
    private Animator animator;

    [Header("Timing")]
    [SerializeField] private float timeBetweenMessages = 2f;
    [SerializeField] private float timePerCharacter = 0.02f;

    [Header("Messages")]
    [TextArea(3, 10)]
    [SerializeField] private string[] messages;

    [Header("Actions")]
    [SerializeField] private UnityEvent[] endEvents;

    private int messageIndex;
    
    void Awake()
    {
        textComponent.text = "";
        animator = npc.GetComponent<Animator>();
        if (startOnAwake) {
            startPlaying();
        }
    }

    public void startPlaying() {
        messageIndex = 0;
        playNextLine();
    }

    private void playNextLine()
    {
        StartCoroutine(PlayNextLineCoroutine());
    }

    private IEnumerator PlayNextLineCoroutine()
    {
        if (textWriterSingle != null && textWriterSingle.IsActive())
        {
            textWriterSingle.WriteAllAndDestroy();
            yield break;
        }

        if (messageIndex < messages.Length)
        {
            if (messageIndex > 0 && messageIndex < messages.Length)
            {
                StopTalkingSound();
                animator.SetBool("Talking", false);
                yield return new WaitForSeconds(timeBetweenMessages);
            }

            animator.SetBool("Talking", true);
            StartTalkingSound();
            textWriterSingle = TextWriter.AddWriter_Static(textComponent, messages[messageIndex], timePerCharacter, false, false, playNextLine);
            messageIndex++;
        }
        else
        {
            StopTalkingSound();
            animator.SetBool("Talking", false);
            foreach (UnityEvent @event in endEvents)
            {
                @event.Invoke();
            }
        }
    }

    private void StartTalkingSound()
    {
        talkingAudioSource.Play();
    }

    private void StopTalkingSound()
    {
        talkingAudioSource.Stop();
    }
}
