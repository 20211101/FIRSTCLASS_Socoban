using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Stack<TurnState> history = new Stack<TurnState>();
    private IStageTurnListener stageTurnListener;

    private void SaveState()
    {
        TurnState state = new TurnState();

        // 플레이어 위치
        state.playerPos = transform.position;

        // 공 위치
        Ball[] balls = FindObjectsOfType<Ball>();
        foreach (Ball b in balls)
        {
            state.balls.Add((b, b.transform.position));
        }

        history.Push(state);
    }
    private void Undo()
    {
        if (history.Count == 0)
            return;

        TurnState state = history.Pop();

        // 플레이어 복원
        transform.position = state.playerPos;

        // 공 복원
        foreach (var b in state.balls)
        {
            b.ball.transform.position = b.pos;
        }
    }

    public void Init(IStageTurnListener stageTurnListener)
    {
        this.stageTurnListener = stageTurnListener;
    }

    private void Update()
    {
        // 🔹 Undo 먼저 처리
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
            return;
        }

        MoveDirection moveDirection = GetInputDir();
        if (moveDirection == MoveDirection.None)
            return;

        SaveState(); // ⭐ 이동 전에 저장

        ScanResult scanResult = Scan(moveDirection);

        bool actionSuccess = TryMove(moveDirection, scanResult);

        if (!actionSuccess)
        {
            history.Pop(); // ⭐ 실패하면 저장 취소
            return;
        }

        stageTurnListener?.OnPlayerActionFinished();
    }

    private bool TryMove(MoveDirection moveDirection, ScanResult scanResult)
    {
        switch (scanResult.Type)
        {
            case ObjectType.None:
            case ObjectType.Goal:
                MoveSelf(moveDirection);
                return true;

            case ObjectType.Wall:
                return false;

            case ObjectType.Ball:
                return TryPushBall(moveDirection, scanResult.Target);

            default:
                Debug.LogError($"처리되지 않은 ObjectType: {scanResult.Type}");
                return false;
        }
    }

    private bool TryPushBall(MoveDirection moveDirection, GameObject target)
    {
        if (target == null)
            return false;

        Ball ball = target.GetComponent<Ball>();
        if (ball == null)
        {
            Debug.LogError("Ball 태그 오브젝트에 Ball 컴포넌트가 없습니다.");
            return false;
        }

        bool pushSuccess = ball.Move(moveDirection);
        if (!pushSuccess)
            return false;

        MoveSelf(moveDirection);
        return true;
    }

    private void MoveSelf(MoveDirection moveDirection)
    {
        transform.position += moveDirection.GetDir();
    }

    private ScanResult Scan(MoveDirection moveDirection)
    {
        Vector3 direction = moveDirection.GetDir();

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1f))
        {
            GameObject target = hit.transform.gameObject;

            if (hit.collider.CompareTag("Wall"))
                return new ScanResult(ObjectType.Wall, target);

            if (hit.collider.CompareTag("Ball"))
                return new ScanResult(ObjectType.Ball, target);

            if (hit.collider.CompareTag("Goal"))
                return new ScanResult(ObjectType.Goal, target);

            Debug.LogError($"등록되지 않은 Tag의 오브젝트입니다. Tag = {hit.collider.tag}");
        }

        return ScanResult.None;
    }

    private MoveDirection GetInputDir()
    {
        if (Input.GetKeyDown(KeyCode.W))
            return MoveDirection.UP;

        if (Input.GetKeyDown(KeyCode.A))
            return MoveDirection.LEFT;

        if (Input.GetKeyDown(KeyCode.S))
            return MoveDirection.DOWN;

        if (Input.GetKeyDown(KeyCode.D))
            return MoveDirection.RIGHT;

        return MoveDirection.None;
    }
}