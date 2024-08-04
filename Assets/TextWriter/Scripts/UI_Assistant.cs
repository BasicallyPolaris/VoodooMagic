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
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Assistant : MonoBehaviour
{
    private TextWriter.TextWriterSingle textWriterSingle;

    [Header("UI Components")]
    [SerializeField]private Text textComponent;
    [Header("Audio")]
    [SerializeField] private AudioSource talkingAudioSource;

    [Header("Messages")]
    [TextArea(3, 10)]
    [SerializeField] private string[] messages;
    private void Awake()
    {

        foreach (string message in messages)
        {
            if (textWriterSingle != null && textWriterSingle.IsActive())
            {
                // Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }
            else
            {

                StartTalkingSound();
                textWriterSingle = TextWriter.AddWriter_Static(textComponent, message, .02f, true, true, StopTalkingSound);
                new WaitForSeconds(message.Length * .02f);
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

    private void Start()
    {
        //TextWriter.AddWriter_Static(messageText, "This is the assistant speaking, hello and goodbye, see you next time!", .1f, true);
    }

}
