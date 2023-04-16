using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public bool load;
    public static LoadManager istance;
    private void Awake()
    {
        load = false;
        if (istance == null)
        {
            istance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
