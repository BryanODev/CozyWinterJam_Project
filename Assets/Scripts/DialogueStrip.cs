using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueStrip
{
    public string PersonTalking;

    [TextArea(4, 5)]
    public string DialogueQuote;
}
