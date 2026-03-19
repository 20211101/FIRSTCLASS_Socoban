using System.Collections.Generic;
using UnityEngine;

public class UndoRecorder : MonoBehaviour
{
    private readonly Stack<TurnRecord> records = new Stack<TurnRecord>();

    public void Push(TurnRecord record)
    {
        if (!record.IsValid)
            return;

        records.Push(record);
    }

    public bool TryUndo()
    {
        if (records.Count == 0)
            return false;

        TurnRecord record = records.Pop();
        record.Undo();
        return true;
    }

    public void Clear()
    {
        records.Clear();
    }
}