public interface IStageTurnListener
{
    void SaveUndoState();
    void OnPlayerActionFinished();
}