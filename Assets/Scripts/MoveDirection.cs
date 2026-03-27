using UnityEngine;

public class MoveDirection
{
    private readonly Vector3 dir;

    private MoveDirection(Vector3 dir)
    {
        this.dir = dir;
    }

    public Vector3 GetDir() => dir;

    public static readonly MoveDirection None = new MoveDirection(Vector3.zero);
    public static readonly MoveDirection UP = new MoveDirection(Vector3.forward);
    public static readonly MoveDirection DOWN = new MoveDirection(Vector3.back);
    public static readonly MoveDirection RIGHT = new MoveDirection(Vector3.right);
    public static readonly MoveDirection LEFT = new MoveDirection(Vector3.left);
}