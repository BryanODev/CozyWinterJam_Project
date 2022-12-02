using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject DialoguePanel;
    public TMP_Text DialogueText;

    public DialogueData currentDialogue;
    int currentDialogueIndex;

    public bool waitForPlayerInteraction;


    public void OpenDialogue() 
    {
        DialoguePanel.SetActive(true);
    }

    public void SetCurrentDialogue(DialogueData newCurrentDialogue) 
    {
        currentDialogue = newCurrentDialogue;
    }

    private void Start()
    {
        DialogueText.SetText("");
        StartCoroutine(DrawDialogueText(currentDialogue.Texts[currentDialogueIndex], 0.05f));
    }

    private void Update()
    {
        if (waitForPlayerInteraction) 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                NextLine();
            }
        }
    }

    void NextLine() 
    {
        if (currentDialogue.Texts[currentDialogueIndex] != null)
        {
            waitForPlayerInteraction = true;

            StartCoroutine(DrawDialogueText(currentDialogue.Texts[currentDialogueIndex], 0.05f));
        }
    }

    IEnumerator DrawDialogueText(string Text, float Duration) 
    {
        float textSize = Text.Length;

        for (int i = 0; i < textSize; i++) 
        {
            DialogueText.text += Text[i];

            yield return new WaitForSeconds(Duration);
        }

        OnDialogueTextFinish();

        yield return null;
    }

    void OnDialogueTextFinish() 
    {
        if (currentDialogue.endsOnQuestion) 
        {
            //We enable the selection box
        }
        else 
        {
            waitForPlayerInteraction = true;
            currentDialogueIndex++;
        }
    }

    public void CloseDialogue() 
    {
        DialoguePanel.SetActive(false);
    }

}
