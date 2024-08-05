/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using TMPro;
using UnityEngine;

public class UI_Assistant : MonoBehaviour
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
    [SerializeField] private float timePerCharacter = .02f;

    [Header("Messages")]
    [TextArea(3, 10)]
    [SerializeField] private string[] messages;

    private int messageIndex;
    
    void Awake()
    {
        textComponent.text = "";
        animator = npc.GetComponent<Animator>();
        Debug.Log(animator);
        Debug.Log(npc.name);

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
