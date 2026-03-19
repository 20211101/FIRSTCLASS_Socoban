using UnityEngine;

public readonly struct TurnRecord
{
    public Player Player { get; }
    public Vector3 PlayerPrevPosition { get; }
    public Vector3 PlayerNextPosition { get; }

    public Ball Ball { get; }
    public Vector3 BallPrevPosition { get; }
    public Vector3 BallNextPosition { get; }

    public bool HasBallMove => Ball != null;
    public bool IsValid => Player != null;

    public TurnRecord(
        Player player,
        Vector3 playerPrevPosition,
        Vector3 playerNextPosition,
        Ball ball,
        Vector3 ballPrevPosition,
        Vector3 ballNextPosition)
    {
        Player = player;
        PlayerPrevPosition = playerPrevPosition;
        PlayerNextPosition = playerNextPosition;
        Ball = ball;
        BallPrevPosition = ballPrevPosition;
        BallNextPosition = ballNextPosition;
    }

    public void Undo()
    {
        if (Ball != null)
        {
            Ball.transform.position = BallPrevPosition;
        }

        if (Player != null)
        {
            Player.transform.position = PlayerPrevPosition;
        }
    }
}