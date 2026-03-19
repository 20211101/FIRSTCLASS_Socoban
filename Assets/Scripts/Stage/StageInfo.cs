using UnityEngine;

public readonly struct StageInfo
{
    public Player Player { get; }
    public Goal[] Goals { get; }

    public StageInfo(Player player, Goal[] goals)
    {
        Player = player;
        Goals = goals;
    }
}