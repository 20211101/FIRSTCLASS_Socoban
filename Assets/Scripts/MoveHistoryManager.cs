using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MoveSnapshot
{
    public Vector3 playerPos;                           // 플레이어는 위치 좌표만 저장하면 됨
    public Dictionary<GameObject, Vector3> ballStates;  // 볼은 각각의 번호와 위치 저장

    public MoveSnapshot(Vector3 playerPos, Dictionary<GameObject, Vector3> ballStates)
    {
        this.playerPos = playerPos;
        this.ballStates = new Dictionary<GameObject, Vector3>(ballStates);
    }
}



public class MoveHistoryManager : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> balls = new List<GameObject>(); // 여러 개의 공을 저장할 리스트

    private Stack<MoveSnapshot> undoStack = new Stack<MoveSnapshot>(); // Undo 스택
    private Stack<MoveSnapshot> redoStack = new Stack<MoveSnapshot>(); // Redo 스택

    private const int MAX_SNAPSHOT_COUNT = 50; // 최대 한도

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        balls.AddRange(GameObject.FindGameObjectsWithTag("Ball")); // 태그로 공 전부 찾아서 리스트에 넣기
    }

    // 이동 전 상태 저장
    public void SaveState()
    {
        Dictionary<GameObject, Vector3> ballStates = new Dictionary<GameObject, Vector3>();

        foreach (GameObject ball in balls) // balls 리스트에서 ball인스턴스 하나하나 꺼내기
        {
            Ball ballScript = ball.GetComponent<Ball>();
            if (ballScript != null && !ballScript.isComplete) // ball이 완료된 상태면 스킵
            {
                ballStates[ball] = ball.transform.position;
            }
        }

        var snapshot = new MoveSnapshot(player.transform.position, ballStates); // 스택에 스냡샷 저장

        // 최대 개수 초과 시 가장 오래된 항목 제거
        if (undoStack.Count >= MAX_SNAPSHOT_COUNT)
        {
            undoStack = TrimOldestSnapshot(undoStack); 
        }

        undoStack.Push(snapshot);
    }

    public void Undo() // 되돌리기
    {
        if (undoStack.Count > 0)
        {
            MoveSnapshot snapshot = undoStack.Pop(); // 스냅샷 꺼내기

            SaveCurrentToRedoStack(); // 현재 상태를 Redo용으로 저장

            player.transform.position = snapshot.playerPos; // 저장된 스냅샷 위치로 복원

            foreach (var pair in snapshot.ballStates)   // 꺼낸 스냡샷의 딕셔너리 하나씩 꺼내기
            {
                GameObject ball = pair.Key; // 공 번호
                Vector3 pos = pair.Value;   // 공 좌표

                Ball ballScript = ball.GetComponent<Ball>();
                if (ballScript != null && !ballScript.isComplete) // ball이 완료된 상태면 스킵
                {
                    ball.transform.position = pos;
                }
            }
        }
    }
    public void Redo() // undo와 구조 같음
    {
        if (redoStack.Count > 0)
        {
            MoveSnapshot snapshot = redoStack.Pop(); 

            SaveCurrentToUndoStack();

            player.transform.position = snapshot.playerPos;

            foreach (var pair in snapshot.ballStates)
            {
                GameObject ball = pair.Key;
                Vector3 pos = pair.Value;

                Ball ballScript = ball.GetComponent<Ball>();
                if (ballScript != null && !ballScript.isComplete)
                {
                    ball.transform.position = pos;
                }
            }
        }
    }

    private void SaveCurrentToRedoStack() // 현재 상태를 Redo용 스택에 저장
    {
        Dictionary<GameObject, Vector3> ballStates = new Dictionary<GameObject, Vector3>();
        foreach (GameObject ball in balls)
        {
            Ball ballScript = ball.GetComponent<Ball>();
            if (ballScript != null && !ballScript.isComplete) 
            {
                ballStates[ball] = ball.transform.position;
            }
        }

        var snapshot = new MoveSnapshot(player.transform.position, ballStates);
        redoStack.Push(snapshot);
    }

    private void SaveCurrentToUndoStack() // 현재 상태를 Undo용 스택에 저장 (Redo 시에 사용)
    {
        Dictionary<GameObject, Vector3> ballStates = new Dictionary<GameObject, Vector3>();
        foreach (GameObject ball in balls)
        {
            Ball ballScript = ball.GetComponent<Ball>();
            if (ballScript != null && !ballScript.isComplete)
            {
                ballStates[ball] = ball.transform.position;
            }
        }

        var snapshot = new MoveSnapshot(player.transform.position, ballStates);
        if (undoStack.Count >= MAX_SNAPSHOT_COUNT)
        {
            undoStack = TrimOldestSnapshot(undoStack);
        }
        redoStack.Push(snapshot);
    }

    private Stack<MoveSnapshot> TrimOldestSnapshot(Stack<MoveSnapshot> original) // 오래된 스냅샷을 제거하여 스택 크기 유지
    {
        var items = new List<MoveSnapshot>(original);
        items.RemoveAt(0); // 가장 오래된 항목 제거
        items.Reverse();   // 다시 스택 순서로 복원

        return new Stack<MoveSnapshot>(items);
    }
}