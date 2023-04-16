using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public SaveSystem saveSystem;
    public Transform player;
    [SerializeField, Range(10f, 200f)] private float cameraSensibility;
    [SerializeField, Range(10f, 90f)] private float motionRange_UPDown;

    private Data data;
    private float playerRotationY;
    private float cameraRotationX;
    private void Awake()
    {
        data = GetComponent<Data>();
    }
    //private void Start()
    //{
    //    playerRotationY = 0;
    //    cameraRotationX = 0;
    //}

    void Update()
    {
        //if (saveSystem.playerRotation)
        //{
        //    player.localRotation = Quaternion.Euler(0f, data.rotation.y, 0f);
        //    Debug.Log(data.rotation.y);
        //    transform.localRotation = Quaternion.Euler(data.rotation.x, 0f, 0f);
        //    Debug.Log(data.rotation.x);
        //    saveSystem.playerRotation = false;
        //}

        if (saveSystem.playerRotation)
        {
            playerRotationY = data.rotation.y;
            Debug.Log(data.rotation.y);
            cameraRotationX = data.rotation.x;
            Debug.Log(data.rotation.x);
            saveSystem.playerRotation = false;
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            playerRotationY += Input.GetAxis("Mouse X") * cameraSensibility * Time.deltaTime;
            cameraRotationX -= Input.GetAxis("Mouse Y") * cameraSensibility * Time.deltaTime;
            //cameraRotationX = Mathf.Clamp(cameraRotationX, -motionRange_UPDown, motionRange_UPDown);
            player.localRotation = Quaternion.Euler(0f, playerRotationY, 0f);
            transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
        }
    }
}
