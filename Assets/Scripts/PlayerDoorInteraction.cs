using UnityEngine;

public class PlayerDoorInteraction : MonoBehaviour
{
    [HideInInspector] public DoorController currentDoor;

    // Called by animation event
    public void OpenDoorEvent()
    {
        if (currentDoor != null)
            currentDoor.OpenDoorEvent();
    }

    // Called by animation event
    public void CloseDoorEvent()
    {
        if (currentDoor != null)
            currentDoor.CloseDoorEvent();
    }
}
