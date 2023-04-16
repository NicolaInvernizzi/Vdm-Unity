using UnityEngine;
using UnityEngine.UI;

public class RayCasting : MonoBehaviour
{
    public PlayerMovement playerMovement_Script;
    private ProjectileDraw projectileDraw_Script;
    [SerializeField, Range(1f, 10f)] private float pickUpRange;
    [Header("Pointer settings"), Space(5)]
    public GameObject pointer;
    [SerializeField, Range(0.001f, 2f)] private float restSize;
    [SerializeField, Range(0.001f, 2f)] private float hitSize;
    [SerializeField] private Color restColor;
    public Color hitColor;
    [Header("Buttons"), Space(5)]
    public KeyCode pickUpButton;
    public KeyCode dropButton;
    public KeyCode throwButton;
    [Header("Trhow force settings"),Space(5)]
    [SerializeField, Range(50, 500)] private float default_throwForce;
    [SerializeField, Range(50, 500)] private float min_throwForce;
    [SerializeField, Range(500, 1000)] private float max_throwForce;
    [SerializeField, Range(1f, 1000f)] private float sens_throwForce;
    [Header("Rotation settings"), Space(5)]
    [SerializeField, Range(-1, 0f)] private float randomRotation_min;
    [SerializeField, Range(0f, 1)] private float randomRotation_max;

    private RectTransform rectTransform;
    private Image image;
    private Transform container;
    private bool grab;
    private RaycastHit raycastHit;
    private float throwForce;
    private LayerMask layerMask;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rectTransform = pointer.GetComponent<RectTransform>();
        image = pointer.GetComponent<Image>();
        projectileDraw_Script = GetComponent<ProjectileDraw>();
        container = gameObject.transform.Find("Container");
    }
    void Start()
    {
        layerMask = LayerMask.GetMask("Pickable") | LayerMask.GetMask("Ground");
        throwForce = default_throwForce;
        ChangePointer(new Vector3(restSize, restSize, restSize), restColor);
    }

    void Update()
    {
        if (playerMovement_Script.isGrounded &&
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, pickUpRange, layerMask))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Pickable"))
            {
                if (!grab && Input.GetKeyDown(pickUpButton))
                    PickUp(hit);
                ChangePointer(new Vector3(hitSize, hitSize, hitSize), hitColor);
                Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.green);
            }
            else
                ResetRay();

        }
        else
            ResetRay();

        if (grab)
        {
            if(Input.GetKey(throwButton))
            {
                throwForce += Input.GetAxis("Mouse ScrollWheel") * sens_throwForce * 100 * Time.deltaTime;
                throwForce = Mathf.Clamp(throwForce, min_throwForce, max_throwForce);
                projectileDraw_Script.DrawTrajectory(
                    container.position, 
                    ((transform.forward * throwForce)/container.GetComponentInChildren<Rigidbody>().mass) /50);
            }
            if (Input.GetKeyUp(throwButton))
                Throw(raycastHit);
            if(Input.GetKeyDown(dropButton))
                Drop(raycastHit);
        }
        else if (!grab && throwForce != default_throwForce)
            throwForce = default_throwForce;
    }
    private void ChangePointer(Vector3 scale, Color color)
    {
        if(rectTransform.localScale != scale)
        {
            rectTransform.localScale = scale;
            image.color = color;
        } 
    }
    private void PickUp(RaycastHit hit)
    {
        hit.transform.GetComponentInChildren<ParticleSystem>().Stop();
        if(hit.transform.gameObject.GetComponent<AudioBox>() != null)
        {
            AudioBox audioBox_Script = hit.transform.gameObject.GetComponent<AudioBox>();
            if (!audioBox_Script.activeAudio)
                hit.transform.gameObject.GetComponent<AudioBox>().activeAudio = true;
        }
        audioSource.Play();
        grab = true;
        raycastHit = hit;
        hit.transform.SetParent(container);
        hit.transform.localPosition = Vector3.zero;
        hit.rigidbody.isKinematic = true;
        hit.collider.isTrigger = true;
    }
    private void Drop(RaycastHit hit)
    {
        grab = false;
        hit.transform.SetParent(null);
        hit.rigidbody.isKinematic = false;
        hit.collider.isTrigger = false;
    }
    private void Throw(RaycastHit hit)
    {
        projectileDraw_Script.DisableLineRenderer();
        Drop(hit);
        hit.rigidbody.AddForce(transform.forward * throwForce);
        RandomRotate(-1, 1,hit);
    }
    private void RandomRotate(float min, float max, RaycastHit hit)
    {
        hit.rigidbody.AddTorque(
            Random.Range(min, max), 
            Random.Range(min, max), 
            Random.Range(min, max), 
            ForceMode.Impulse);
    }
    private void ResetRay()
    {
        ChangePointer(new Vector3(restSize, restSize, restSize), restColor);
        Debug.DrawRay(transform.position, transform.forward * pickUpRange, Color.red);
    }
}
