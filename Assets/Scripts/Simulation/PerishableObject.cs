using UnityEngine;

public class PerishableObject : MonoBehaviour
{
    [SerializeField] private float timeUntilPerish;
    private float perishTimer;

    private void OnEnable()
    {
        perishTimer = timeUntilPerish;
    }

    private void FixedUpdate()
    {
        if (perishTimer > 0) perishTimer -= Time.fixedDeltaTime;
        else Destroy(gameObject);
    }
}
