using UnityEngine;

public interface IInteractable
{
    void OnPickup(PlayerController player);
    void OnThrow(Vector3 direction);
    void OnPutDown();
}

// ƒTƒ“ƒvƒ‹ŽÀ‘•
public class InteractableObject : MonoBehaviour, IInteractable
{
    [SerializeField] private float throwForce = 10f;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnPickup(PlayerController player)
    {
        if (rb != null)
        {
            rb.isKinematic = true;
        }
        player.SetHeldObject(this);
    }

    public void OnThrow(Vector3 direction)
    {
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.AddForce(direction * throwForce, ForceMode.Impulse);
        }
    }

    public void OnPutDown()
    {
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}