using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LoadButtons : MonoBehaviour
{
    private LoadManager loadManager;
    private void Awake()
    {
        loadManager = FindObjectOfType<LoadManager>();
    }
    void Update()
    {
        if (!File.Exists(Application.persistentDataPath + "/InventoryData.json"))
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
    public void Load()
    {
        loadManager.load = true;
        SceneManager.LoadScene(1);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
