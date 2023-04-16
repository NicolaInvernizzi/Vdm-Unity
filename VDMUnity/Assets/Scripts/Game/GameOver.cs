using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text textBox;
    public GameObject door;
    public int maxCounter;
    internal int counter;
    internal bool openArea2;
    public AudioSource ghost1;
    public AudioSource ghost2;
    private AudioSource areaSwitch_Audio;

    private void Awake()
    {
        areaSwitch_Audio = GetComponent<AudioSource>();
    }
    private void Start()
    {
        openArea2 = false;
        counter = 0;
    }
    private void Update()
    {
        if (!openArea2 && counter == maxCounter / 2)
            OpenArea2();

        if (counter == maxCounter)
            Win();     
    }
    private void Win()
    {
        Debug.Log("Win");
    }
    private void OpenArea2()
    {
        ghost1.Play();
        ghost2.Play();
        Debug.Log("Destroy");
        Destroy(door);
        openArea2 = true;
    }
    public void updateTextBox()
    {
        textBox.text = counter.ToString();
    }
}
