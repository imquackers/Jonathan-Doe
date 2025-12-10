using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator anim;

    public void OpenDoor()
    {
        if (anim != null)
            anim.Play("DoorOpen");
    }
}

