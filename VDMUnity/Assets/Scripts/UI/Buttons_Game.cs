using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons_Game : MonoBehaviour
{
    public GameObject playMenu;
    public GameObject pauseMenu;
    public void Pause()
    {
        //Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        playMenu.SetActive(false);
    }
    public void Resume()
    {
        //Time.timeScale = 1f;
        playMenu.SetActive(true);
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
