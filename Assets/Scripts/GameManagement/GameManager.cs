using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IStageTurnListener
{
    [SerializeField] private GameObject clearUI;
    [SerializeField] private MapCreator mapCreator;

    private Player player;
    private Goal[] goals;
    private Ball[] balls;

    private Stack<TurnSnapshot> undoStack = new Stack<TurnSnapshot>();

    private void Start()
    {
        CreateStage();
        RefreshGoals();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoMove();
        }
    }

    private void CreateStage()
    {
        StageInfo stageInfo = mapCreator.CreateStage();

        player = stageInfo.Player;
        goals = stageInfo.Goals;
        balls = stageInfo.Balls;

        if (player == null)
        {
            Debug.LogError("Stage 생성 결과에 Player가 없습니다.");
            return;
        }

        player.Init(this);
    }

    public void SaveUndoState()
    {
        if (player == null)
            return;

        Vector3[] ballPositions = new Vector3[balls.Length];

        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] != null)
                ballPositions[i] = balls[i].transform.position;
        }

        TurnSnapshot snapshot = new TurnSnapshot(player.transform.position, ballPositions);
        undoStack.Push(snapshot);
    }

    public void OnPlayerActionFinished()
    {
        Physics.SyncTransforms();
        RefreshGoals();
    }

    public void UndoMove()
    {
        if (undoStack.Count == 0 || player == null)
            return;

        TurnSnapshot snapshot = undoStack.Pop();

        player.transform.position = snapshot.PlayerPosition;

        for (int i = 0; i < balls.Length; i++)
        {
            if (balls[i] != null)
                balls[i].transform.position = snapshot.BallPositions[i];
        }

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
        else if (clearUI != null)
            clearUI.SetActive(false);
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