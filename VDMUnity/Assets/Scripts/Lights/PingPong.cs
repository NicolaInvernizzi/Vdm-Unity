using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    public Transform stopPosition;
    public float speed;
    Vector3 startPosition;
    float timer;
    int invert;
    private void Start()
    {
        startPosition = transform.position;
        invert = 1;
    }
    void Update()
    {
        timer += speed * Time.deltaTime;
        switch(invert)
        {
            case 1:
                transform.position = LerpPingPong(startPosition, stopPosition.position);
                break;
            case -1:
                transform.position = LerpPingPong(stopPosition.position, startPosition);
                break;
        }
    }
    private Vector3 LerpPingPong(Vector3 start, Vector3 stop)
    {
        Vector3 result;
        result = Vector3.Lerp(start, stop, timer);
        if (result == stop)
        {
            invert *= -1;
            timer = 0;
        }
        return result;
    }
}
