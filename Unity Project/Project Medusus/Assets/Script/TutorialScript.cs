using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    [Header("Button Data")]
    public GameObject NextData;
    public GameObject ESCData;

    [Header("Actual Button")]
    public GameObject NextButton;
    public GameObject ESCButton;

    [Header("UI Order")]
    public GameObject UI_1;
    public GameObject UI_2;

    private bool pressed;
    private SceneManagerScript sceneManager;

    [Header("Audio Source")]
    public AudioSource clickAudio;
    public AudioSource pressAudio;

    enum ButtonState
    {
        Next,
        Back,
        Escape        
    }
    ButtonState buttonState;

    private void Start()
    {
        buttonState = ButtonState.Next;
        pressed = false;

        UI_1.SetActive(true);
        UI_2.SetActive(false);

        HighlightButton(true);
        sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && !pressed)
        {
            HighlightButton(false);
            if (buttonState == ButtonState.Next || buttonState == ButtonState.Back)
            {
                buttonState = ButtonState.Escape;
            }
            else if (buttonState == ButtonState.Escape)
            {
                if (UI_1.activeInHierarchy)
                {
                    buttonState = ButtonState.Next;
                }
                else
                {
                    buttonState = ButtonState.Back;
                }
                
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

            if (buttonState == ButtonState.Next || buttonState == ButtonState.Back)
            {
                pressed = false;
                if (buttonState == ButtonState.Next)
                {
                    UI_1.SetActive(false);
                    UI_2.SetActive(true);
                    buttonState = ButtonState.Back;
                }
                else if (buttonState == ButtonState.Back)
                {
                    UI_2.SetActive(false);
                    UI_1.SetActive(true);
                    buttonState = ButtonState.Next;
                }
            }
            else
            {
                sceneManager.LoadScene("MenuScene");
            }
        }
    }

    GameObject ButtonObject(bool data)
    {
        // TRUE = DATA, FALSE = BUTTON
        GameObject buttonObject = null;

        if (buttonState == ButtonState.Next || buttonState == ButtonState.Back)
        {
            if (data)
            {
                buttonObject = NextData;
            }
            else
            {
                buttonObject = NextButton;
            }
        }
        else if (buttonState == ButtonState.Escape)
        {
            if (data)
            {
                buttonObject = ESCData;
            }
            else
            {
                buttonObject = ESCButton;
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
}
