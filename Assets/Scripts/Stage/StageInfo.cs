using UnityEngine;

public readonly struct StageInfo
{
    public Player Player { get; }
    public Goal[] Goals { get; }
    public Ball[] Balls { get; }

    public StageInfo(Player player, Goal[] goals, Ball[] balls)
    {
        Player = player;
        Goals = goals;
        Balls = balls;
    }
}