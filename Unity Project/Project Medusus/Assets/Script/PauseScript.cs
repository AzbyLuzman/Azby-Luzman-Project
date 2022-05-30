using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public bool paused;
    bool escPressed;
    bool exit;
    GameObject pauseObject;
    CharacterMovement characterMovement;

    bool switchButton;

    [Header("Button Data")]
    public GameObject noData;
    public GameObject yesData;

    [Header("Actual Button")]
    public GameObject noButton;
    public GameObject yesButton;

    [Header("Audio Source")]
    public AudioSource clickAudio;
    public AudioSource pressAudio;

    private void Start()
    {
        paused = false;
        escPressed = false;
        switchButton = false;
        exit = false;

        pauseObject = transform.GetChild(0).gameObject;
        characterMovement = GameObject.Find("Player").GetComponent<CharacterMovement>();
        noButton = pauseObject.transform.Find("No").gameObject;
        yesButton = pauseObject.transform.Find("Yes").gameObject;

        pauseObject.SetActive(false);
        characterMovement.enabled = true;
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (paused)
        {
            if (!exit)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    switchButton = !switchButton;

                    if (switchButton)
                    {
                        HighlightButton(true);
                        clickAudio.Play();
                    }
                    else
                    {
                        HighlightButton(false);
                        clickAudio.Play();
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                exit = true;
                if (switchButton)
                {
                    PressedButton(true, false);
                    pressAudio.Play();
                }
                else
                {
                    PressedButton(false, false);
                    pressAudio.Play();
                }
            }
            if (Input.GetKeyUp(KeyCode.Return))
            {
                if (switchButton)
                {
                    PressedButton(true, true);
                    Time.timeScale = 1f;
                    SceneManagerScript sceneManagerScript = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
                    sceneManagerScript.LoadScene("MenuScene");
                }
                else
                {
                    PressedButton(false, true);
                    ResumeGame();
                    escPressed = false;
                    exit = false;
                }
            }
        }

        if (!paused && !escPressed && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (paused && !escPressed && Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            escPressed = false;
        }
    }

    void PauseGame()
    {
        pauseObject.SetActive(true);
        characterMovement.enabled = false;
        switchButton = false;
        paused = true;
        escPressed = true;

        HighlightButton(false);

        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        paused = false;
        escPressed = true;
        exit = false;
        pauseObject.SetActive(false);
        characterMovement.enabled = true;
        Time.timeScale = 1f;
    }

    void HighlightButton(bool flag)
    {
        if (!flag)
        {
            Button button = noData.GetComponent<Button>();
            Image image = noButton.GetComponent<Image>();
            image.color = button.colors.highlightedColor;

            button = yesData.GetComponent<Button>();
            image = yesButton.GetComponent<Image>();
            image.color = button.colors.normalColor;

        }
        else
        {
            Button button = yesData.GetComponent<Button>();
            Image image = yesButton.GetComponent<Image>();
            image.color = button.colors.highlightedColor;

            button = noData.GetComponent<Button>();
            image = noButton.GetComponent<Image>();
            image.color = button.colors.normalColor;
        }
    }

    void PressedButton(bool flag, bool pressed)
    {
        if (!flag && !pressed)
        {
            Button button = noData.GetComponent<Button>();
            Image image = noButton.GetComponent<Image>();
            image.color = button.colors.pressedColor;
        }
        else if (flag && !pressed)
        {
            Button button = yesData.GetComponent<Button>();
            Image image = yesButton.GetComponent<Image>();
            image.color = button.colors.pressedColor;
        }
        else if (pressed && !flag)
        {
            Button button = noData.GetComponent<Button>();
            Image image = noButton.GetComponent<Image>();
            image.color = button.colors.selectedColor;
        }
        else if (pressed && flag)
        {
            Button button = yesData.GetComponent<Button>();
            Image image = yesButton.GetComponent<Image>();
            image.color = button.colors.selectedColor;
        }
    }
}