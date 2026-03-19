using UnityEngine;

public readonly struct ScanResult
{
    public ObjectType Type { get; }
    public GameObject Target { get; }

    public bool HasTarget => Target != null;

    public ScanResult(ObjectType type, GameObject target)
    {
        Type = type;
        Target = target;
    }

    public static ScanResult None => new ScanResult(ObjectType.None, null);
}