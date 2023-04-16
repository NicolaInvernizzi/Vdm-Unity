using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject door;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == door)
        { 
            Destroy(door);
            Destroy(gameObject);
        }
    }
}
