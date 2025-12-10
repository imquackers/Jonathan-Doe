using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    //Player Setup
    public Camera playerCamera;//main camera
    public Transform holdPosition;//hold point where object is held
    public float pickupRange = 3f;//how far player can reach 

    //Throwing Setup
    public float maxThrowForce = 20f;//maximum force to throw
    public float chargeSpeed = 10f;//how much it charges the throw

    private GameObject heldObject;//the object to be held
    private Rigidbody heldRb;//needs rigid body to be picked up and thrown
    private ThrowableTile heldScript;//needs to add the throwable script to whatever is picked up 

    private bool holding = false;//not holding anything at the start
    private float throwCharge = 0f;//throw force up to 20

    void Update()
    {
        if (!holding)
        {
            TryPickup();//always running
        }
        else
        {
            HandleHoldingAndThrowing();
        }
    }

    void TryPickup()
    {
        if (Input.GetKeyDown(KeyCode.E))//always checking if E is pressed
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange)) //always checks if the player is looking at something interactable
            {
                if (hit.collider.CompareTag("Tile"))//now checks if the player is looking at something with the tag "Tile"
                {
                    GameObject original = hit.collider.gameObject;//clones the object and sets the object interacted with as "original" object
                    heldObject = Instantiate(original, holdPosition.position, Quaternion.identity);//puts the object in the hold position

                    //set up the rigid body
                    heldRb = heldObject.GetComponent<Rigidbody>();
                    if (heldRb == null) heldRb = heldObject.AddComponent<Rigidbody>();
                    heldRb.isKinematic = true;

                    // add throwabletile script
                    heldScript = heldObject.GetComponent<ThrowableTile>();
                    if (heldScript == null) heldScript = heldObject.AddComponent<ThrowableTile>();

                    holding = true;//set holding to true
                    throwCharge = 0f;

                    //remove the original object
                    Destroy(original);
                }
            }
        }
    }

    void HandleHoldingAndThrowing()
    {
        // keep the object in front of the player in hold posiotion
        heldObject.transform.position = holdPosition.position;
        heldObject.transform.rotation = Quaternion.identity;

        // charging throw
        if (Input.GetMouseButton(0))
        {
            throwCharge += chargeSpeed * Time.deltaTime;
            throwCharge = Mathf.Clamp(throwCharge, 0, maxThrowForce);
        }

        // throw on release
        if (Input.GetMouseButtonUp(0))
        {
            heldRb.isKinematic = false;

            heldScript.OnThrown();

            heldRb.AddForce(playerCamera.transform.forward * throwCharge, ForceMode.Impulse);

            // destroy after 5 seconds
            Destroy(heldObject, 5f);

            heldObject = null;
            heldRb = null;
            heldScript = null;
            holding = false;
        }
    }
}
