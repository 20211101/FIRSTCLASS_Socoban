using UnityEngine;

public readonly struct TurnSnapshot
{
    public Vector3 PlayerPosition { get; }
    public Vector3[] BallPositions { get; }

    public TurnSnapshot(Vector3 playerPosition, Vector3[] ballPositions)
    {
        PlayerPosition = playerPosition;
        BallPositions = ballPositions;
    }
}