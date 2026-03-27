using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool CanMove(MoveDirection moveDirection)
    {
        Vector3 direction = moveDirection.GetDir();

        if (direction == Vector3.zero)
            return false;

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1f))
        {
            if (hit.collider.CompareTag("Wall"))
                return false;

            if (hit.collider.CompareTag("Ball"))
                return false;
        }

        return true;
    }

    public bool Move(MoveDirection moveDirection)
    {
        if (!CanMove(moveDirection))
            return false;

        transform.position += moveDirection.GetDir();
        return true;
    }
}