using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Start or quit the game
/// </summary>
public class Deathmenu : MonoBehaviour
{
    private Button[] buttons;
   // private Image[] images;

    void Awake()
    {
        // Get the buttons
        buttons = GetComponentsInChildren<Button>();

        //Get text
       // images = GetComponentsInChildren<Image>();

        // Disable them
        HideButtons();
       // HideImages();
    }

    public void HideButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
        }
    }

    //Doesn't work as of now.

   /* public void HideImages()
    {
        foreach (var b in images)
        {
            b.gameObject.SetActive(false);
        }
    }

    public void ShowImages()
    {
        foreach (var b in images)
        {
            b.gameObject.SetActive(true);
        }
    } */

    public void ExitToMenu()
    {
        // Reload the level
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        // Reload the level
        SceneManager.LoadScene("Alpha");
    }
}
