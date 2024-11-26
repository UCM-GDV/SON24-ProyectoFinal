using UnityEngine;
using FMODUnity;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float interactionDistance = 5f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public EventReference lampEvent;
    public EventReference keyboardEvent;
    public EventReference clickEvent;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleMouseLook();
        HandleInteraction();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0f);
    }

    void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Lamp"))
                {
                    Transform rootObject = hit.collider.transform.root;

                    Transform spotLightTransform = rootObject.Find("Spot Light");
                    if (spotLightTransform != null)
                    {
                        Light light = spotLightTransform.GetComponent<Light>();
                        if (light != null)
                        {
                            light.enabled = !light.enabled;
                            PlayLightSound(light.enabled);
                        }
                    }
                } 
                else if (hit.collider.CompareTag("Keyboard"))
                {
                    PlayKeyboardSound();
                }
                else if (hit.collider.CompareTag("Mouse"))
                {
                    PlayClickSound();
                }
            }
        }
    }

    void PlayLightSound(bool isLightOn)
    {
        var instance = RuntimeManager.CreateInstance(lampEvent);
        instance.setParameterByName("SwitchLight", isLightOn ? 1 : 0);
        instance.start();
        instance.release();
    }

    void PlayKeyboardSound()
    {
        var instance = RuntimeManager.CreateInstance(keyboardEvent);
        instance.start();
        instance.release();
    }
    void PlayClickSound()
    {
        var instance = RuntimeManager.CreateInstance(clickEvent);
        instance.start();
        instance.release();
    }
}
