using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueSystem/DialogueData", order = 1)]
public class DialogueData : ScriptableObject
{
    public string DialogueTitle;

    public DialogueStrip[] Quotes;

    public bool endsOnQuestion;
    public DialogueData[] Options;

    public DialogueData NextDialogue;

    public bool DialogueIsValid() 
    {
        return Quotes.Length > 0;
    }
}
