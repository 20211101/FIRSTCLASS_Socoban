using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IStageTurnListener
{
    [SerializeField] private GameObject clearUI;
    [SerializeField] private MapCreator mapCreator;
    [SerializeField] private UndoRecorder undoRecorder;

    private Goal[] goals;

    private void Start()
    {
        CreateStage();
        RefreshGoals();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }
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

        if (undoRecorder != null)
        {
            undoRecorder.Clear();
        }
    }

    public void OnPlayerActionFinished(TurnRecord record)
    {
        undoRecorder?.Push(record);
        Physics.SyncTransforms();
        RefreshGoals();
    }

    private void Undo()
    {
        if (undoRecorder == null)
            return;

        bool undoSuccess = undoRecorder.TryUndo();
        if (!undoSuccess)
            return;

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
        {
            GameClear();
        }
        else
        {
            clearUI.SetActive(false);
        }
    }

    private void GameClear()
    {
        clearUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}