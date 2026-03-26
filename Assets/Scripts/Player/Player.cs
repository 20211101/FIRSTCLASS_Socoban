using UnityEngine;

public class Player : MonoBehaviour
{
    private IStageTurnListener stageTurnListener;

    public void Init(IStageTurnListener stageTurnListener)
    {
        this.stageTurnListener = stageTurnListener;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            stageTurnListener?.OnUndoRequested();
            return;
        }

        MoveDirection moveDirection = GetInputDir();
        if (moveDirection == MoveDirection.None)
            return;

        ScanResult scanResult = Scan(moveDirection);

        MoveRecord? record = TryMove(moveDirection, scanResult);
        if (record == null)
            return;

        stageTurnListener?.OnPlayerActionFinished(record.Value);
    }

    private MoveRecord? TryMove(MoveDirection moveDirection, ScanResult scanResult)
    {
        switch (scanResult.Type)
        {
            case ObjectType.None:
            case ObjectType.Goal:
                MoveRecord moveRecord = new MoveRecord(transform.position);
                MoveSelf(moveDirection);
                return moveRecord;

            case ObjectType.Wall:
                return null;

            case ObjectType.Ball:
                return TryPushBall(moveDirection, scanResult.Target);

            default:
                Debug.LogError($"처리되지 않은 ObjectType: {scanResult.Type}");
                return null;
        }
    }

    private MoveRecord? TryPushBall(MoveDirection moveDirection, GameObject target)
    {
        if (target == null)
            return null;

        Ball ball = target.GetComponent<Ball>();
        if (ball == null)
        {
            Debug.LogError("Ball 태그 오브젝트에 Ball 컴포넌트가 없습니다.");
            return null;
        }

        Vector3 ballPrevPos = ball.transform.position;

        bool pushSuccess = ball.Move(moveDirection);
        if (!pushSuccess)
            return null;

        MoveRecord record = new MoveRecord(transform.position, ball, ballPrevPos);
        MoveSelf(moveDirection);
        return record;
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
