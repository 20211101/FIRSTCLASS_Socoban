using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCheck : MonoBehaviour
{
    Stack<Vector3> PreturnMove = new Stack<Vector3>();
    Stack<Vector3> BreturnMove = new Stack<Vector3>();
    public StageInfo stageInfo;

    [SerializeField] GameObject[] ball;
    [SerializeField] GameObject player;

    public void Set()
    {

        ball = GameObject.FindGameObjectsWithTag("Ball");

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            moveReturn();
        }
    }

    public void PositionCheck()
    {
        PreturnMove.Push(player.gameObject.transform.position);
        for (int i = 0; i < ball.Length; i++)
        {
            BreturnMove.Push(ball[i].gameObject.transform.position);
        }
    }

    public void moveReturn()
    {
        if (PreturnMove.Count != 0 && BreturnMove.Count != 0)
        {
            player.transform.position = PreturnMove.Pop();
            for (int i = 0; i < ball.Length; i++)
            {
                ball[i].transform.position = BreturnMove.Pop();
            }
        }
    }
}
