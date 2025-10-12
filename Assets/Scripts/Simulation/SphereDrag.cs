using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SphereDrag : MonoBehaviour
{
    [SerializeField] private float crossSectionalArea;

    private const float AIR_DENSITY = 1.225f;
    private const float DRAG_COEFFICIENT = 0.47f;
    private Rigidbody rb;

    private void Awake()
    {
        TryGetComponent(out rb);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;
        float speed = velocity.magnitude;

        float dragMagnitude = 0.5f * AIR_DENSITY * DRAG_COEFFICIENT * crossSectionalArea * Mathf.Pow(speed, 2);

        Vector3 dragForce = -velocity.normalized * dragMagnitude;
        rb.AddForce(dragForce, ForceMode.Force);
    }
}
