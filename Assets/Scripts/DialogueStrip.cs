using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueStrip
{
    public string PersonTalking;

    [TextArea(4, 5)]
    public string DialogueQuote;

    public UnityEvent OnStripStartEvent;

    public UnityEvent OnStripEndEvent;
}
