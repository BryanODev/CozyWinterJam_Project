using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueOption : MonoBehaviour
{
    Dialogue dialogueParent;
    TMP_Text optionText;
    public DialogueData dialogueOption;

    Button buttonComponent;

    private float fontSize;

    private void Awake()
    {
        optionText = GetComponentInChildren<TMP_Text>();
        buttonComponent = GetComponent<Button>();
    }

    private void Start()
    {
        fontSize = optionText.fontSize;
    }

    public void InitializeOption(Dialogue _dialogueParent) 
    {
        dialogueParent = _dialogueParent;
    }

    public void AssignDialogueOption(DialogueData newOption) 
    {
        dialogueOption = newOption;
        SetOptionText(dialogueOption.DialogueTitle);

        buttonComponent.onClick.RemoveAllListeners();
        buttonComponent.onClick.AddListener(OnOptionClick);
    }

    void OnOptionClick() 
    {
        if (dialogueOption.DialogueIsValid())
        {
            dialogueParent.SetCurrentDialogue(dialogueOption);
            dialogueParent.ToggleOptionPanel(false);
        }
        else 
        {
            Debug.LogError("This dialogue option is Invalid! Has no Quotes!");
        }
    }

    void OnDialogueOptionClick() 
    {
        dialogueParent.SetCurrentDialogue(dialogueOption);
    }

    public void SetOptionText(string newOptionText) 
    {
        optionText.text = newOptionText;
    }

    public void ToggleOption(bool newOptionActive) 
    {
        gameObject.SetActive(newOptionActive);
    }

    public void OnPointerEnter() 
    {
        optionText.fontSize = fontSize + 15.0f;
    }

    public void OnPointerExit()
    {
        optionText.fontSize = fontSize;
    }

    private void OnDisable()
    {
        optionText.fontSize = fontSize;
    }

}
