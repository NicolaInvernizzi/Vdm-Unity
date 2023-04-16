using UnityEngine;

public class Particles : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<ParticleSystem>().Stop();
    }
}
