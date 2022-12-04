using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public Image BlackScreen;

    public GameObject startButton;
    public GameObject exitButton;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() 
    {
        StartCoroutine(StartFadeIn());
    }

    void LoadGame() 
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator StartFadeIn()
    {
        startButton.SetActive(false);
        exitButton.SetActive(false);

        Color newColor = Color.black;
        float Alpha = 0;

        while (Alpha < 1)
        {
            Alpha += 0.25f * Time.deltaTime;
            newColor.a = Alpha;
            BlackScreen.color = newColor;

            yield return new WaitForEndOfFrame();
        }

        Alpha = 1;
        newColor.a = Alpha;
        BlackScreen.color = newColor;

        LoadGame();

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

        startButton.SetActive(true);
        exitButton.SetActive(true);


        yield return null;
    }
}
