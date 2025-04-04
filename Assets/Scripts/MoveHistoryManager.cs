using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MoveSnapshot                      // 플레이어와 볼의 위치를 저장하는 스냅샷 클래스
{
    public Vector3 playerPos;                  // 플레이어의 위치
    public Vector3 ballPos;                    // 공의 위치

    public MoveSnapshot(Vector3 playerPos, Vector3 ballPos)  // 생성자: 위치 정보를 받아 저장
    {
        this.playerPos = playerPos;            // 플레이어 위치 저장
        this.ballPos = ballPos;                // 볼 위치 저장
    }
}

public class MoveHistoryManager : MonoBehaviour
{
    public GameObject player;                   // 플레이어 오브젝트 
    public GameObject ball;                     // 볼 오브젝트 

    private Stack<MoveSnapshot> moveStack = new Stack<MoveSnapshot>(); // MoveSnapshot자료형을 저장할 스택

    void Start()
    {
        player = GameObject.FindWithTag("Player"); // Player    태그로 연결
        ball = GameObject.FindWithTag("Ball");     // Ball      태그로 연결
    }

    // 이동 전 상태 저장
    public void SaveState()
    { 
        var snapshot = new MoveSnapshot(    // 플레이어, 볼 위치로 새 스냅샷 생성
            player.transform.position,
            ball.transform.position
        );
        moveStack.Push(snapshot);           // 스택에 스냅샷을 저장
    }

    // 되돌리기
    public void Undo()
    {
        if (moveStack.Count > 0) // 저장된 스냅샷이 있을 때만
        {
            MoveSnapshot snapshot = moveStack.Pop();        // 스택에서 스냅샷 꺼냄
            player.transform.position = snapshot.playerPos; // 스냅샷 위치로 이동
            ball.transform.position = snapshot.ballPos;     // 스냅샷 위치로 이동
        }
    }
}


