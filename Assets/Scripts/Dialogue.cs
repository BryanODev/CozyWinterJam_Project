using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public GameObject DialogueOptionPrefab;

    public GameObject DialoguePanel;
    public GameObject DialogueOptionsPanel;

    public DialogueOption[] OptionList = new DialogueOption[4];
    public TMP_Text DialogueText;
    public TMP_Text DialogueNameText;
    public DialogueData previousDialogue;
    public DialogueData currentDialogue;
    public Font Italic;

    public int currentDialogueIndex;
    public int currentDialogueMaxText;
    public bool waitForPlayerInteraction;

    float dialogueCurrentDrawSpeed = 0.05f;
    public float dialogueSlowDrawSpeed = 0.05f;
    public float dialogueFastDrawSpeed = 0.005f;

    public Image BlackScreen;
    public TMP_Text EndText;
    bool BlackScreenOn;

    [Header("Audio")]
    [SerializeField] private AudioSource voxAudioSource;
    [SerializeField] AudioClip[] narratorAudioClip;
    [SerializeField] AudioClip[] lilyAudioClip;
    [SerializeField] AudioClip[] miaAudioClip;
    [SerializeField] AudioClip CarDoorSlam;
    [SerializeField] AudioClip DoorClose;

    public Image Mia;
    public Image Lily;

    public Color silluhetColor;

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            DialogueOption optionInstance = Instantiate(DialogueOptionPrefab, DialogueOptionsPanel.transform).GetComponent<DialogueOption>();
            optionInstance.InitializeOption(this);
            OptionList[i] = optionInstance;
        }
    }

    private void Start()
    {
        StartCoroutine(StartFadeOut());
    }

    public void ToggleDialogue(bool isActive)
    {
        DialoguePanel.SetActive(isActive);
    }

    public void SetCurrentDialogue(DialogueData newCurrentDialogue)
    {
        ToggleDialogue(true);
        previousDialogue = currentDialogue;
        currentDialogue = newCurrentDialogue;
        currentDialogueMaxText = currentDialogue.Quotes.Length;
        currentDialogueIndex = 0;


        if (currentDialogue.Quotes[currentDialogueIndex].PersonTalking == "Mia")
        {
            Mia.color = Color.white;
            Lily.color = silluhetColor;
        }
        else if (currentDialogue.Quotes[currentDialogueIndex].PersonTalking == "Lily")
        {
            Lily.color = Color.white;
            Mia.color = silluhetColor;
        }
        else
        {
            Mia.color = silluhetColor;
            Lily.color = silluhetColor;
        }

        //Start the Dialogue when we set it

        if (currentDialogue.DialogueIsValid())
        {
            DialogueNameText.SetText(currentDialogue.Quotes[currentDialogueIndex].PersonTalking);

            if (DialogueNameText.text == "")
            {
                DialogueText.fontStyle = FontStyles.Italic;
            }
            else if (DialogueNameText.text.Length > 0)
            {
                DialogueText.fontStyle = FontStyles.Normal;
            }

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
            if (Input.GetMouseButtonDown(0))
            {
                dialogueCurrentDrawSpeed = dialogueFastDrawSpeed;
                voxAudioSource.pitch = 1.5f;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                dialogueCurrentDrawSpeed = dialogueSlowDrawSpeed;
                voxAudioSource.pitch = 1;
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

        if (currentDialogue.Quotes[currentDialogueIndex].PersonTalking == "Mia")
        {
            Mia.color = Color.white;
            Lily.color = silluhetColor;
        }
        else if (currentDialogue.Quotes[currentDialogueIndex].PersonTalking == "Lily")
        {
            Lily.color = Color.white;
            Mia.color = silluhetColor;
        }
        else 
        {
            Mia.color = silluhetColor;
            Lily.color = silluhetColor;
        }

        if (DialogueNameText.text == "")
        {
            DialogueText.fontStyle = FontStyles.Italic;
        }
        else if (DialogueNameText.text.Length > 0)
        {
            DialogueText.fontStyle = FontStyles.Normal;
        }

        //Call Stript Start Event 

        currentDialogue.Quotes[currentDialogueIndex].OnStripStartEvent.Invoke();

        StartCoroutine(DrawDialogueText(currentDialogue.Quotes[currentDialogueIndex].DialogueQuote));
    }

    IEnumerator DrawDialogueText(string Text)
    {
        EmptyDialogue();

        waitForPlayerInteraction = false;

        float textSize = Text.Length;
        DialogueText.text = Text;
        DialogueText.maxVisibleCharacters = 0;

        for (int i = 0; i < textSize; i++)
        {
            float pauseMod = 0.0f;

            DialogueText.maxVisibleCharacters += 1;

            RemoveR();
            switch (Text[i])
            {
                case '.':
                    pauseMod = 10f;
                    break;
                case ',':
                    pauseMod = 5f;
                    break;
                case ':':
                    pauseMod = 8f;
                    break;
                default:
                    pauseMod = 1.0f;
                    break;
            }

            if (dialogueCurrentDrawSpeed == 0.05f)
            {
                switch (currentDialogue.Quotes[currentDialogueIndex].PersonTalking)
                {
                    case "Mia":
                        voxAudioSource.panStereo = -0.1f;
                        voxAudioSource.PlayOneShot(miaAudioClip[Random.Range(0, miaAudioClip.Length)]);
                        break;
                    case "Lily":
                        voxAudioSource.panStereo = 0.1f;
                        voxAudioSource.PlayOneShot(lilyAudioClip[Random.Range(0, lilyAudioClip.Length)]);
                        break;
                    default:
                        voxAudioSource.PlayOneShot(narratorAudioClip[Random.Range(0, narratorAudioClip.Length)]);
                        break;
                }
            }
            else
            {
                voxAudioSource.Stop();
            }

            yield return new WaitForSeconds(dialogueCurrentDrawSpeed * pauseMod);
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

                for (int i = 0; i < 2; i++)
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
            else 
            {
                //We set the next dialogue

                if (currentDialogue.NextDialogue != null)
                {
                    SetCurrentDialogue(currentDialogue.NextDialogue);
                }
                else 
                {
                    EndGame();
                }
            }
        }
        else
        {
            //call quote on end strip event
            currentDialogue.Quotes[currentDialogueIndex].OnStripEndEvent.Invoke();

            waitForPlayerInteraction = true;
            dialogueCurrentDrawSpeed = dialogueSlowDrawSpeed;
            currentDialogueIndex++;
        }
    }

    public void RemoveR()
    {
        DialogueText.text = DialogueText.text.Replace("\r", "");
    }

    void EmptyDialogue()
    {
        DialogueText.SetText("");
    }

    public void ToggleOptionPanel(bool isActive)
    {
        DialogueOptionsPanel.SetActive(isActive);
    }

    void EndGame() 
    {
        Debug.Log("End game");

        StartCoroutine(StartFadeIn());

        //Go to a level, close or w/e action we want
    }

    public void PlayCarDoorSlam()
    {
        voxAudioSource.PlayOneShot(CarDoorSlam); 
    }

    public void PlayDoorClose()
    {
        voxAudioSource.PlayOneShot(DoorClose);
    }

    IEnumerator StartFadeIn() 
    {
        BlackScreen.gameObject.SetActive(true);
        Color newColor = Color.black;
        Color EndnewColor = EndText.color;
        float Alpha = 0;

        while (Alpha < 1) 
        {
            Alpha += 0.25f * Time.deltaTime;
            newColor.a = Alpha;
            EndnewColor.a = Alpha;
            BlackScreen.color = newColor;
            EndText.color = EndnewColor;

            yield return new WaitForEndOfFrame();
        }

        Alpha = 1;
        newColor.a = Alpha;
        EndnewColor.a = Alpha;
        BlackScreen.color = newColor;
        EndText.color = EndnewColor;

        //We end game and go to title
        Invoke("ReturnToTitleMenu", 3.0f);

        yield return null;
    }

    IEnumerator StartFadeOut() 
    {
        Color newColor = Color.black;
        float Alpha = 1;

        while (Alpha > 0)
        {
            Alpha -= 0.25f * Time.deltaTime;
            newColor.a = Alpha;
            BlackScreen.color = newColor;

            yield return new WaitForEndOfFrame();
        }

        Alpha = 0;
        newColor.a = Alpha;
        BlackScreen.color = newColor;

        //We start game
        BlackScreen.gameObject.SetActive(false);
        SetCurrentDialogue(currentDialogue);

        yield return null;
    }

    void ReturnToTitleMenu() 
    {
        SceneManager.LoadScene(0);
    }

}

