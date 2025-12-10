using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    //Player Setup
    public Camera playerCamera;
    public Transform holdPosition;
    public float pickupRange = 3f;

    //Throwing Setup
    public float maxThrowForce = 20f;
    public float chargeSpeed = 10f;

    private GameObject heldObject;
    private Rigidbody heldRb;
    private ThrowableTile heldScript;

    private bool holdingObject = false;
    private float throwCharge = 0f;

    void Update()
    {
        if (!holdingObject)
        {
            TryPickup();
        }
        else
        {
            HandleHoldingAndThrowing();
        }
    }

    void TryPickup()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, pickupRange)) //checks if the player is looking at something interactable
            {
                // ALL interactable objects use tag "Tile"
                if (hit.collider.CompareTag("Tile"))
                {
                    // Clone the object the player clicked
                    GameObject original = hit.collider.gameObject;
                    heldObject = Instantiate(original, holdPosition.position, Quaternion.identity);

                    // Setup Rigidbody
                    heldRb = heldObject.GetComponent<Rigidbody>();
                    if (heldRb == null) heldRb = heldObject.AddComponent<Rigidbody>();
                    heldRb.isKinematic = true;

                    // Add Throwable script if missing
                    heldScript = heldObject.GetComponent<ThrowableTile>();
                    if (heldScript == null) heldScript = heldObject.AddComponent<ThrowableTile>();

                    holdingObject = true;
                    throwCharge = 0f;

                    // Remove the original
                    Destroy(original);
                }
            }
        }
    }

    void HandleHoldingAndThrowing()
    {
        // keep the object in front of the player
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
            holdingObject = false;
        }
    }
}
