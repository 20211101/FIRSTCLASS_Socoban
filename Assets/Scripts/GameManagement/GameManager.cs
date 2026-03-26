using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IStageTurnListener
{
    [SerializeField] private GameObject clearUI;
    [SerializeField] private MapCreator mapCreator;

    private const int MaxUndoCount = 3;

    private Player player;
    private Goal[] goals;
    private readonly Stack<MoveRecord> moveHistory = new Stack<MoveRecord>();
    private int remainingUndoCount = MaxUndoCount;

    private void Start()
    {
        CreateStage();
        RefreshGoals();
    }

    private void CreateStage()
    {
        StageInfo stageInfo = mapCreator.CreateStage();

        goals = stageInfo.Goals;

        if (stageInfo.Player == null)
        {
            Debug.LogError("Stage 생성 결과에 Player가 없습니다.");
            return;
        }

        player = stageInfo.Player;
        player.Init(this);
    }

    public void OnPlayerActionFinished(MoveRecord record)
    {
        moveHistory.Push(record);
        Physics.SyncTransforms();
        RefreshGoals();
    }

    public void OnUndoRequested()
    {
        if (moveHistory.Count == 0 || remainingUndoCount <= 0)
            return;

        remainingUndoCount--;
        MoveRecord record = moveHistory.Pop();
        player.transform.position = record.PlayerPrevPos;

        if (record.MovedBall != null)
            record.MovedBall.transform.position = record.BallPrevPos;

        Physics.SyncTransforms();
        RefreshGoals();
    }

    private void RefreshGoals()
    {
        if (goals == null || goals.Length == 0)
            return;

        int filledGoalCount = 0;

        foreach (Goal goal in goals)
        {
            if (goal != null && goal.HasBall())
                filledGoalCount++;
        }

        if (filledGoalCount == goals.Length)
            GameClear();
    }

    private void GameClear()
    {
        if (clearUI != null)
            clearUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
