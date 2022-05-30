using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Button Data")]
    public GameObject startData;
    public GameObject tutorialData;
    public GameObject creditData;
    public GameObject quitData;

    [Header("Actual Button")]
    public GameObject startButton;
    public GameObject tutorialButton;
    public GameObject creditButton;
    public GameObject quitButton;

    [Header("Audio Source")]
    public AudioSource clickAudio;
    public AudioSource pressAudio;

    private SceneManagerScript sceneManager;

    enum ButtonState
    {
        Start,
        Tutorial,
        Credit,
        Quit
    }
    ButtonState buttonState;

    bool pressed;

    private void Start()
    {
        buttonState = ButtonState.Start;
        pressed = false;

        HighlightButton(true);
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !pressed)
        {
            HighlightButton(false);
            buttonState--;
            if (buttonState < 0)
            {
                buttonState = ButtonState.Quit;
            }
            HighlightButton(true);
            clickAudio.Play();
        }
        else if (Input.GetKeyDown(KeyCode.S) && !pressed)
        {
            HighlightButton(false);
            buttonState++;
            if (buttonState > ButtonState.Quit)
            {
                buttonState = ButtonState.Start;
            }
            HighlightButton(true);
            clickAudio.Play();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            PressedButton(true);
            pressed = true;
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            PressedButton(false);
            pressAudio.Play();
            ChangeScene();
        }
    }

    GameObject ButtonObject(bool data)
    {
        // TRUE = DATA, FALSE = BUTTON
        GameObject buttonObject = null;

        if (buttonState == ButtonState.Start)
        {
            if (data)
            {
                buttonObject = startData;
            }
            else
            {
                buttonObject = startButton;
            }            
        }
        else if (buttonState == ButtonState.Tutorial)
        {
            if (data)
            {
                buttonObject = tutorialData;
            }
            else
            {
                buttonObject = tutorialButton;
            }
        }
        else if (buttonState == ButtonState.Credit)
        {
            if (data)
            {
                buttonObject = creditData;
            }
            else
            {
                buttonObject = creditButton;
            }
        }
        else if (buttonState == ButtonState.Quit)
        {
            if (data)
            {
                buttonObject = quitData;
            }
            else
            {
                buttonObject = quitButton;
            }
        }

        return buttonObject;
    }

    void HighlightButton(bool flag)
    {
        GameObject buttonData = ButtonObject(true); // DATA
        GameObject buttonObject = ButtonObject(false); // BUTTON
        Button button = buttonData.GetComponent<Button>();
        Image image = buttonObject.GetComponent<Image>();

        if (!flag)
        {
            image.color = button.colors.normalColor;
            buttonObject.transform.GetChild(1).gameObject.SetActive(false);
            buttonObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (flag)
        {
            image.color = button.colors.highlightedColor;
            buttonObject.transform.GetChild(0).gameObject.SetActive(false);
            buttonObject.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    void PressedButton(bool flag)
    {
        GameObject buttonData = ButtonObject(true); // DATA
        GameObject buttonObject = ButtonObject(false); // BUTTON
        Button button = buttonData.GetComponent<Button>();
        Image image = buttonObject.GetComponent<Image>();

        if (flag)
        {
            image.color = button.colors.pressedColor;
        }
        else if (!flag)
        {
            image.color = button.colors.selectedColor;
        }
    }

    void ChangeScene()
    {
        if (buttonState != ButtonState.Tutorial)
        {
            if (TimerCoroutine != null)
            {
                StopCoroutine(TimerCoroutine);
            }
            TimerCoroutine = StartCoroutine(OnChangeScene());
        }
        else
        {
            sceneManager.LoadScene("TutorialScene");
        }
    }
    Coroutine TimerCoroutine;
    IEnumerator OnChangeScene()
    {
        yield return new WaitForSeconds(1f);

        if (buttonState == ButtonState.Start)
        {
            sceneManager.LoadScene("NewGameScene");
        }
        else if (buttonState == ButtonState.Credit)
        {
            sceneManager.LoadScene("CreditsScene");
        }
        else if (buttonState == ButtonState.Quit)
        {
            Application.Quit();

        }
    }
}
