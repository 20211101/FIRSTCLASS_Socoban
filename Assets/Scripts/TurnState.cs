using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TurnState
{
    public Vector3 playerPos;
    public List<(Ball ball, Vector3 pos)> balls = new List<(Ball, Vector3)>();
}