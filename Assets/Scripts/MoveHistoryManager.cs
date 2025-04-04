using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MoveSnapshot
{
    public Vector3 playerPos;
    public Vector3 ballPos;

    public MoveSnapshot(Vector3 playerPos, Vector3 ballPos)
    {
        this.playerPos = playerPos;
        this.ballPos = ballPos;
    }
}

public class MoveHistoryManager : MonoBehaviour
{
    public GameObject player;
    public GameObject ball;

    private Stack<MoveSnapshot> moveStack = new Stack<MoveSnapshot>();

    int snapshotCount = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ball = GameObject.FindWithTag("Ball");
    }

    // 이동 전 상태 저장
    public void SaveState()
    {
        snapshotCount++;
        var snapshot = new MoveSnapshot(player.transform.position, ball.transform.position);
        moveStack.Push(snapshot);
    }

    // 백스페이스 눌렀을 때 복구
    public void Undo()
    {
        if (moveStack.Count > 0)
        {
            MoveSnapshot snapshot = moveStack.Pop();
            player.transform.position = snapshot.playerPos;
            ball.transform.position = snapshot.ballPos;

            Debug.Log($"[UNDO] 플레이어 되돌림 → {snapshot.playerPos}, 현재 위치: {player.transform.position}");
            Debug.Log($"[UNDO] 볼 되돌림 → {snapshot.ballPos}, 현재 위치: {ball.transform.position}");

            Debug.Log("되돌리기 성공");
        }
    }
}


