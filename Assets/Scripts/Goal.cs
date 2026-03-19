using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private float checkRadius = 0.1f;

    public bool HasBall()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Ball"))
                return true;
        }

        return false;
    }
}