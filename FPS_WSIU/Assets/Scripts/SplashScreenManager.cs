/*
Made by RoXKhaar
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreenManager : MonoBehaviour
{
    public Image splashImage;
    public string sceneToLoad;

    IEnumerator Start()
    {
        splashImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene(sceneToLoad);
    }

    private void FadeIn()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    private void FadeOut()
    {
        splashImage.CrossFadeAlpha(0.0f, 1.5f, false);
    }
}
