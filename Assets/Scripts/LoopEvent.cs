using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class LoopEvent : MonoBehaviour
{
    [SerializeField]
    FMODUnity.EventReference breatheEvent;
    FMOD.Studio.EventInstance breathe;
    [SerializeField]
    float nervSpeed = 1.0f;
    float cont = 0.0f;
    
    public void Start()
    {
        breathe = RuntimeManager.CreateInstance(breatheEvent);
        breathe.start();
    }

    // Update is called once per frame
    public void Update()
    {
        if(cont < 1.0f) {
            cont += nervSpeed * Time.deltaTime;
            Mathf.Clamp(cont, 0.0f, 1.0f);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Nervousness", cont);
        }
        if (breathe.isValid())
        {
            FMOD.Studio.PLAYBACK_STATE playbackState;
            breathe.getPlaybackState(out playbackState);
            if (playbackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
            {
                breathe.start();
            }
        }
    }


}
