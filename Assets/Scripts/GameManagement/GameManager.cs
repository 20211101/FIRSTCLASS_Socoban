using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IStageTurnListener
{
    [SerializeField] private GameObject clearUI;
    [SerializeField] private MapCreator mapCreator;

    private Goal[] goals;

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

        stageInfo.Player.Init(this);
    }

    public void OnPlayerActionFinished()
    {
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