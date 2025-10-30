using UnityEngine;
using UnityEngine.Events;

public class Wall : MonoBehaviour
{
    public static UnityEvent OnObjectHitWall = new();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody _))
        {
            Destroy(other.gameObject);
            OnObjectHitWall?.Invoke();
        }
    }
}
