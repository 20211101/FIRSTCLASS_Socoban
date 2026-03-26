public interface IStageTurnListener
{
    void OnPlayerActionFinished(MoveRecord record);
    void OnUndoRequested();
}
