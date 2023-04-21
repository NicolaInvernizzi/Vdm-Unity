using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public SaveSystem saveSystem;
    public Transform player;
    [SerializeField, Range(0f, 1f)] private float cameraSensibility;
    [SerializeField, Range(10f, 90f)] private float motionRange_UPDown;

    private Data data;
    private float playerRotationY;
    private float cameraRotationX;
    private void Awake()
    {
        data = GetComponent<Data>();
    }
    void Update()
    {
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
            playerRotationY += Input.GetAxis("Mouse X") * cameraSensibility;
            cameraRotationX -= Input.GetAxis("Mouse Y") * cameraSensibility;
            //cameraRotationX = Mathf.Clamp(cameraRotationX, -motionRange_UPDown, motionRange_UPDown);
            player.localRotation = Quaternion.Euler(0f, playerRotationY, 0f);
            transform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
        }
    }
}
