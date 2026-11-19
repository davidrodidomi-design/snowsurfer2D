using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Gotocredits()
    {
        SceneManager.LoadScene("Credits");

    }
    public void Gotocharacter()
    {
        SceneManager.LoadScene("Characters");
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SelectCharacter( int characterIndex)
    {
       PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
       GoToMenu();
    }
}
