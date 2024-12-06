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
    float nervSpeed = 0.1f;
    float cont = 0.0f;
    
    public void Start()
    {
        breathe = RuntimeManager.CreateInstance(breatheEvent);
        breathe.start();
    }

    // Update is called once per frame
    public void Update()
    {
        print(cont);
        if(cont < 25.0f) {
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Nervousness", cont / 25.0f);
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

        cont += nervSpeed * Time.deltaTime;
    }


}
