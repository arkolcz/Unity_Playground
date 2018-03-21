/*
Made by RoXKhaar
*/

using UnityEngine;

public class MenuManager : MonoBehaviour {

    public GameObject settingsMenu;
    public GameObject mainMenu;

    public void Play()
    {
        // TODO: Load Game Scene
    }

    public void MenuTabManager()
    {
        if(settingsMenu.activeInHierarchy == true)
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
