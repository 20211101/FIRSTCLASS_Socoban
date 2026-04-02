using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private IStageTurnListener stageTurnListener;
    private Stack<MoveDirection> moveHistory = new Stack<MoveDirection>(); //추가

    public void Init(IStageTurnListener stageTurnListener)
    {
        this.stageTurnListener = stageTurnListener;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Undo();
            return;
        }

        MoveDirection moveDirection = GetInputDir();
        if (moveDirection == MoveDirection.None)
            return;

        ScanResult scanResult = Scan(moveDirection);

        bool actionSuccess = TryMove(moveDirection, scanResult);
        if (!actionSuccess) { return; }

        moveHistory.Push(moveDirection); //추가

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

    //======추가==========
    private void Undo()
    {
        if (moveHistory.Count == 0) return;

        // 마지막 이동 방향을 꺼냄
        MoveDirection lastMove = moveHistory.Pop();

        // 반대 방향 계산
        MoveDirection reverseDir = GetReverseDirection(lastMove);

        // 플레이어 뒤로 이동
        // 주의: 소코반 특성상 '되돌리기'는 물리적 충돌을 무시하고 강제로 위치를 옮겨야 합니다.
        // 만약 공을 밀면서 이동했었다면 공도 당겨와야 하는데, 
        // 여기서는 단순화를 위해 플레이어의 이전 위치에 공이 있는지 체크 후 당겨오는 로직이 필요할 수 있습니다.

        // 되돌리기 경로에 공이 있었는지 확인 (플레이어가 갔던 방향에 공이 있는지 확인)
        if (Physics.Raycast(transform.position, lastMove.GetDir(), out RaycastHit hit, 1f))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                // 공을 플레이어가 있던 현재 위치로 당김
                Ball ball = hit.transform.GetComponent<Ball>();
                if (ball != null) ball.Move(reverseDir);
            }
        }
        // 플레이어 자신을 반대 방향으로 이동
        MoveSelf(reverseDir);

        // 턴 종료 알림 (필요 시)
        stageTurnListener?.OnPlayerActionFinished();
    }

    private MoveDirection GetReverseDirection(MoveDirection dir)
    {
        if (dir == MoveDirection.UP) return MoveDirection.DOWN;
        if (dir == MoveDirection.DOWN) return MoveDirection.UP;
        if (dir == MoveDirection.LEFT) return MoveDirection.RIGHT;
        if (dir == MoveDirection.RIGHT) return MoveDirection.LEFT;

        return MoveDirection.None;
    }
}