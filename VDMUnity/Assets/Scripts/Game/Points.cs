using UnityEngine;

public class Points : MonoBehaviour
{
    public SaveSystem saveSystem;
    public GameOver gameOver_Script;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Box")
        {
            audioSource.Play();
            other.gameObject.GetComponent<AudioBox>().activeAudio = false;
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        GameObject box = other.gameObject;
        if (box.tag == "Box" && 
            !box.GetComponent<AudioBox>().activeAudio &&
            !box.GetComponent<AudioBox>().audioSource.isPlaying)
        {
            Destroy(other.gameObject);
            gameOver_Script.counter++;
            gameOver_Script.updateTextBox();
            ParticleSystem particles = transform.GetComponentInChildren<ParticleSystem>();
            if (!particles.isPlaying)
                transform.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
