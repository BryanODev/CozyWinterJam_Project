using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject DialogueOptionPrefab;

    public GameObject DialoguePanel;
    public GameObject DialogueOptionsPanel;

    public DialogueOption[] OptionList = new DialogueOption[4];
    public TMP_Text DialogueText;
    public TMP_Text DialogueNameText;
    public DialogueData currentDialogue;

    public int currentDialogueIndex;
    public int currentDialogueMaxText;
    public bool waitForPlayerInteraction;

    float dialogueCurrentDrawSpeed = 0.05f;
    public float dialogueSlowDrawSpeed = 0.05f;
    public float dialogueFastDrawSpeed = 0.005f;


    private void Awake()
    {
        for (int i = 0; i < 4; i++) 
        {
            DialogueOption optionInstance = Instantiate(DialogueOptionPrefab, DialogueOptionsPanel.transform).GetComponent<DialogueOption>();
            optionInstance.InitializeOption(this);
            OptionList[i] = optionInstance;
        }
    }

    private void Start()
    {
        SetCurrentDialogue(currentDialogue);
    }

    public void ToggleDialogue(bool isActive)
    {
        DialoguePanel.SetActive(isActive);
    }

    public void SetCurrentDialogue(DialogueData newCurrentDialogue)
    {
        currentDialogue = newCurrentDialogue;
        currentDialogueMaxText = currentDialogue.Quotes.Length;
        currentDialogueIndex = 0;

        //Start the Dialogue when we set it

        if (currentDialogue.DialogueIsValid())
        {
            DialogueNameText.SetText(currentDialogue.Quotes[currentDialogueIndex].PersonTalking);
            StartCoroutine(DrawDialogueText(currentDialogue.Quotes[currentDialogueIndex].DialogueQuote));
        }
        else 
        {
            Debug.LogError("This dialogue is Invalid! Has no Quotes!");
        }
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
        else
        {
            if (Input.GetMouseButton(0))
            {
                dialogueCurrentDrawSpeed = dialogueFastDrawSpeed;
            }
            else
            {
                dialogueCurrentDrawSpeed = dialogueSlowDrawSpeed;
            }
        }
    }

    bool EndOfDialogue() 
    {
        return currentDialogueIndex == currentDialogueMaxText - 1;
    }

    void NextLine() 
    {
        DialogueNameText.SetText(currentDialogue.Quotes[currentDialogueIndex].PersonTalking);
        StartCoroutine(DrawDialogueText(currentDialogue.Quotes[currentDialogueIndex].DialogueQuote));
    }

    IEnumerator DrawDialogueText(string Text) 
    {
        EmptyDialogue();

        waitForPlayerInteraction = false;

        float textSize = Text.Length;

        for (int i = 0; i < textSize; i++) 
        {
            DialogueText.text += Text[i];

            yield return new WaitForSeconds(dialogueCurrentDrawSpeed);
        }

        OnDialogueTextFinish();

        yield return null;
    }

    void OnDialogueTextFinish() 
    {
        if (EndOfDialogue()) 
        {
            waitForPlayerInteraction = false;

            if (currentDialogue.endsOnQuestion) 
            {
                //We enable the selection box
                ToggleOptionPanel(true);

                ///The max of options will always be: 4
                ///if theres less that 4 options, we will disable the not in use
                ///

                float optionsCount = currentDialogue.Options.Length;

                Debug.Log(optionsCount);

                for (int i = 0; i < 4; i++)
                {
                    if (i < optionsCount)
                    {
                        OptionList[i].AssignDialogueOption(currentDialogue.Options[i]);
                    }
                    else 
                    {
                        OptionList[i].ToggleOption(false);
                    }
                }

                //for (int i = 0; i < currentDialogue.Options.Length; i++) 
                //{
                //    OptionList[i].SetOptionText(currentDialogue.Options[i].DialogueTitle);
                //}

            }
        }
        else 
        {
            waitForPlayerInteraction = true;
            dialogueCurrentDrawSpeed = dialogueSlowDrawSpeed;
            currentDialogueIndex++;
        }
    }

    void EmptyDialogue() 
    {
        DialogueText.SetText("");
    }

    public void ToggleOptionPanel(bool isActive) 
    {
        DialogueOptionsPanel.SetActive(isActive);
    }

}

