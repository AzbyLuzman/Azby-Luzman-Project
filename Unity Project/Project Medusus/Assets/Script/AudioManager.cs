using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    GameObject AudioObject;
    GameObject walkAudio;
    GameObject runAudio;
    GameObject heartbeat;

    private void Start()
    {
        AudioObject = GameObject.Find("AudioObject");
        walkAudio = AudioObject.transform.Find("Walk").gameObject;
        runAudio = AudioObject.transform.Find("Run").gameObject;
        heartbeat = AudioObject.transform.Find("Heartbeat").gameObject;
    }

    public void PlayWalkAudio(bool trigger)
    {
        walkAudio.SetActive(trigger);
    }

    public void PlayRunAudio(bool trigger)
    {
        runAudio.SetActive(trigger);
    }

    public void PlayHeartbeatAudio(bool trigger)
    {
        heartbeat.SetActive(trigger);
    }
}
