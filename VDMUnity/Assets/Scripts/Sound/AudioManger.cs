using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManger : MonoBehaviour
{
    public static AudioManger istance;
    private void Awake()
    {
        if(istance == null)
        {
            istance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
