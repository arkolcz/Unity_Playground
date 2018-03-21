/*
Made by RoXKhaar
*/

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
=======
using UnityEngine.SceneManagement;
>>>>>>> Minor changes to SplashScreenManager and 01Menu

public class SplashScreenManager : MonoBehaviour
{
    public Image splashImage;
<<<<<<< HEAD
=======
    public string sceneToLoad;
>>>>>>> Minor changes to SplashScreenManager and 01Menu

    IEnumerator Start()
    {
        splashImage.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
<<<<<<< HEAD
        yield return new WaitForSeconds(2.5f);

=======
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneToLoad);
>>>>>>> Minor changes to SplashScreenManager and 01Menu
    }

    private void FadeIn()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    private void FadeOut()
    {
<<<<<<< HEAD
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
=======
        splashImage.CrossFadeAlpha(0.0f, 1.5f, false);
    }
}

>>>>>>> Minor changes to SplashScreenManager and 01Menu
