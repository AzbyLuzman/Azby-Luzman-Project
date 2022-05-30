using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROOFTOPBUGFIX : MonoBehaviour
{
    public float timer = 3f;
    private SceneManagerScript sceneManager;

    private void Start()
    {
        sceneManager = GetComponent<SceneManagerScript>();

        if (Countdown != null)
        {
            StopCoroutine(Countdown);
        }
        Countdown = StartCoroutine(TimerCountdown());
    }

    Coroutine Countdown;
    IEnumerator TimerCountdown()
    {
        yield return new WaitForSeconds(timer);

        sceneManager.LoadScene("ClimbCutscene");
    }
}
