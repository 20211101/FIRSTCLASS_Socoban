using System;
using UnityEngine;

public struct StageInfo
{
    public Player Player { get; }
    public Goal[] Goals { get; }

    public StageInfo(Player player, Goal[] goals)
    {
        Player = player;
        Goals = goals;

    }
}