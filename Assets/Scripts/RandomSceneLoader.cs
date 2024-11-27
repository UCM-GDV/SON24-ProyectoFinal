using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomSceneLoader : MonoBehaviour
{
    public float delayInSeconds = 10.0f;
    public List<int> sceneIndexes;
    public int finalSceneIndex;
    private List<int> shuffledScenes;

    public Canvas canvas;
    public Image fadeImage;
    public Image crosshair;
    public float fadeDuration = 1.0f;

    public GameObject myCamera;
    private StudioEventEmitter respiroSuspiro;

    void Awake()
    {
        myCamera.SetActive(false);
        DontDestroyOnLoad(gameObject);
        ShuffleScenes();
        respiroSuspiro = myCamera.GetComponent<StudioEventEmitter>();
        respiroSuspiro.enabled = false;
    }

    public void StartLoadingScenes()
    {
        canvas.sortingOrder = 2;
        myCamera.SetActive(true);
        StartCoroutine(LoadScenesRandomly());
    }

    IEnumerator LoadScenesRandomly()
    {
        foreach (int sceneIndex in shuffledScenes)
        {
            yield return StartCoroutine(FadeIn());
            respiroSuspiro.enabled = true;
            SceneManager.LoadScene(sceneIndex);
            yield return StartCoroutine(FadeOut());
            yield return new WaitForSeconds(delayInSeconds);
        }
        // Escena final
        yield return StartCoroutine(FadeIn());
        respiroSuspiro.enabled = false;
        SceneManager.LoadScene(finalSceneIndex);
        yield return StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        crosshair.color = new Color(crosshair.color.r, crosshair.color.g, crosshair.color.b, 0);
    }

    IEnumerator FadeOut()
    {
        crosshair.color = new Color(crosshair.color.r, crosshair.color.g, crosshair.color.b, 1);
        float elapsedTime = fadeDuration;
        while (elapsedTime > 0)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, elapsedTime / fadeDuration);
            elapsedTime -= Time.deltaTime;
            yield return null;
        }
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0);
    }

    private void ShuffleScenes()
    {
        shuffledScenes = new List<int>(sceneIndexes);
        int n = shuffledScenes.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int value = shuffledScenes[k];
            shuffledScenes[k] = shuffledScenes[n];
            shuffledScenes[n] = value;
        }
    }
}
