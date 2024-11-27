using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public EventReference birdsEvent;
    [Range(0.0f, 1.0f)]
    public float birdsAmbient = 0.5f;

    private EventInstance instance;
    private float currentBirdsAmbient;

    void Start()
    {
        instance = RuntimeManager.CreateInstance(birdsEvent);
        instance.start();
        currentBirdsAmbient = birdsAmbient;
        instance.setParameterByName("Birds", birdsAmbient);
    }

    void Update()
    {
        if (currentBirdsAmbient != birdsAmbient)
        {
            instance.setParameterByName("Birds", birdsAmbient);
            currentBirdsAmbient = birdsAmbient;
        }
    }

    void OnDestroy()
    {
        instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        instance.release();
    }
}
