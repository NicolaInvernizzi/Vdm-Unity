using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
