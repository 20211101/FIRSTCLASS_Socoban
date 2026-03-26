using UnityEngine;

public readonly struct MoveRecord
{
    public readonly Vector3 PlayerPrevPos;
    public readonly Ball MovedBall;
    public readonly Vector3 BallPrevPos;

    public MoveRecord(Vector3 playerPrevPos, Ball movedBall = null, Vector3 ballPrevPos = default)
    {
        PlayerPrevPos = playerPrevPos;
        MovedBall = movedBall;
        BallPrevPos = ballPrevPos;
    }
}
