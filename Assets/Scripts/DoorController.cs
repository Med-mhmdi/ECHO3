using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    public string doorChildName = "Door";
    public float openAngle = 90f;
    public float speed = 1f;

    [Header("Player Detection")]
    public float interactionRange = 0.5f;
    public LayerMask playerLayer;

    private Transform door;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    private bool isOpening = false;
    private bool isClosing = false;

    private Animator playerAnimator;
    private Transform playerTransform;

    void Start()
    {
        door = transform.Find(doorChildName);
        if (door == null)
        {
            Debug.LogError("Door child not found: " + doorChildName);
            return;
        }

        // Set the base rotation once
        float baseY = door.localEulerAngles.y;

        // Set closed and open rotations explicitly
        closedRotation = Quaternion.Euler(0, baseY, 0);
        openRotation = Quaternion.Euler(0, baseY - openAngle, 0); // Change to +openAngle if backward
    }

    void Update()
    {
        DetectPlayer();

        if (playerTransform != null &&
            Vector3.Distance(transform.position, playerTransform.position) <= interactionRange &&
            Input.GetKeyDown(KeyCode.Return))
        {
            playerAnimator.SetTrigger("OpenDoor");
        }

        RotateDoor();
    }

    void RotateDoor()
    {
        if (isOpening)
        {
            door.localRotation = Quaternion.Lerp(door.localRotation, openRotation, Time.deltaTime * speed);
            if (Quaternion.Angle(door.localRotation, openRotation) < 0.1f)
            {
                door.localRotation = openRotation;
                isOpening = false;
            }
        }

        if (isClosing)
        {
            door.localRotation = Quaternion.Lerp(door.localRotation, closedRotation, Time.deltaTime * speed);
            if (Quaternion.Angle(door.localRotation, closedRotation) < 0.1f)
            {
                door.localRotation = closedRotation;
                isClosing = false;
            }
        }
    }

    void DetectPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, interactionRange, playerLayer);
        if (hits.Length > 0)
        {
            playerTransform = hits[0].transform;
            playerAnimator = playerTransform.GetComponent<Animator>();

            var relay = playerTransform.GetComponent<PlayerDoorInteraction>();
            if (relay != null)
            {
                relay.currentDoor = this;
            }
        }
        else
        {
            playerTransform = null;
            playerAnimator = null;
        }
    }

    public void OpenDoorEvent()
    {
        isOpening = true;
        isClosing = false;
    }

    public void CloseDoorEvent()
    {
        isClosing = true;
        isOpening = false;
    }
}
