using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueSystem/DialogueData", order = 1)]
public class DialogueData : ScriptableObject
{
    public string DialogueTitle;

    [TextArea(4,5)]
    public string[] Texts;

    public bool endsOnQuestion;
    public DialogueData[] Options;

    public DialogueData NextDialogue;
}
