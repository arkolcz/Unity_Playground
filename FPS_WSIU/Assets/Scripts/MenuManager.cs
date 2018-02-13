/*
Made by RoXKhaar
*/

using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject settingsMenu;

    public void Play()
    {
        // TODO: Load Game Scene
    }

    public void SettingsTabManager()
    {
        if(settingsMenu.activeInHierarchy == true)
        {
            settingsMenu.SetActive(false);
        }
        else
        {
            settingsMenu.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
