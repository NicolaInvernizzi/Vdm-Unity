using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isGrounded;

    [SerializeField] private Buttons_Game buttons_Game_Script;
    [SerializeField, Range(1f, 100f)] private float speed;
    [SerializeField] private float activeGravity;
    [SerializeField] private float defaultGravity;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private float jumpSpeed;
    public LayerMask groundMask;
    private CharacterController controller;
    private float xMovement;
    private float yMovement;
    private float zMovement;
    private bool ladder;

    void Start()
    {
        gameObject.GetComponent<CharacterController>().enabled = false;
        yMovement = defaultGravity;
        controller = GetComponent<CharacterController>();
        StartCoroutine(EnableCharacterController());
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            buttons_Game_Script.Pause();
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded)
            {
                if (yMovement < defaultGravity)
                    yMovement = defaultGravity;
                if (Input.GetButtonDown("Jump"))
                    yMovement = jumpSpeed;
            }
            else
                yMovement += activeGravity * Time.deltaTime;

            xMovement = Input.GetAxis("Horizontal") * speed;
            zMovement = Input.GetAxis("Vertical") * speed;
            switch (ladder)
            {
                case true:
                    controller.Move(((transform.right * xMovement) +
                                    (transform.up * zMovement)) *
                                     Time.deltaTime);
                    break;
                default:
                    controller.Move(((transform.right * xMovement) +
                                     (transform.up * yMovement) +
                                     (transform.forward * zMovement)) *
                                      Time.deltaTime);
                    break;
            }
        }
    }
    IEnumerator EnableCharacterController()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<CharacterController>().enabled = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ladder" && isGrounded)
        {
            if(!ladder && zMovement > 0)
                ladder = true;
            if(ladder && zMovement < 0)
                ladder = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
            ladder = false;
    }
}
