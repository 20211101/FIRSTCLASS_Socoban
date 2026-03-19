using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MoveDirection
{
    Vector3 dir;
    MoveDirection(Vector3 dir)
    { this.dir = dir; }

    public Vector3 GetDir() => dir;

    public static MoveDirection None = new MoveDirection(new Vector3(0, 0, 0));
    public static MoveDirection UP = new MoveDirection(new Vector3(0, 0, 1));
    public static MoveDirection DOWN = new MoveDirection(new Vector3(0, 0, -1));
    public static MoveDirection RIGHT = new MoveDirection(new Vector3(1, 0, 0));
    public static MoveDirection LEFT = new MoveDirection(new Vector3(-1, 0, 0));
}