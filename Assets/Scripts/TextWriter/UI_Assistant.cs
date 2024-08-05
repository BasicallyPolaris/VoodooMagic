/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assistant : MonoBehaviour
{
    private TextWriter.TextWriterSingle textWriterSingle;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI textComponent;
    private Text text;

    [Header("Audio")]
    [SerializeField] private AudioSource talkingAudioSource;

    [Header("Messages")]
    [TextArea(3, 10)]
    [SerializeField] private string[] messages;

    private int messageIndex;

    private void Start()
    {
        messageIndex = 0;
        playNextLine();
    }

    public void playNextLine()
    {
        StartCoroutine(PlayNextLineCoroutine());
    }

    private IEnumerator PlayNextLineCoroutine()
    {
        if (textWriterSingle != null && textWriterSingle.IsActive())
        {
            // Currently active TextWriter
            textWriterSingle.WriteAllAndDestroy();
            yield break;
        }

        if (messageIndex < messages.Length)
        {
            if (messageIndex > 0 && messageIndex < messages.Length)
            {
                StopTalkingSound();
                yield return new WaitForSeconds(2);
            }

            StartTalkingSound();
            textWriterSingle = TextWriter.AddWriter_Static(textComponent, messages[messageIndex], .02f, false, false, playNextLine);
            messageIndex++;
        }
        else
        {
            StopTalkingSound();
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
