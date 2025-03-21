using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct MoveData
{
    // �̵� ���� ��ġ�� ���� �ϴ� ������
    public Vector3 moveDirection;
    // �̵� �� �� ���� ������ ���� ������ �ش� ������Ʈ ���� ��� ������
    public Ball movedBall;
    public MoveData(Vector3 moveDirection, Ball movedBall)
    {
        this.moveDirection = moveDirection;
        this.movedBall = movedBall;
    }
}
class MoveStack
{
    MoveData[] list = new MoveData[100];
    int top = -1;
    public bool Add(MoveData data)
    {
        if (list.Length <= top) return false;
        top++;
        Debug.Log(top);
        list[top] = data;
        return true;
    }
    public MoveData Pop()
    {
        if (top < 0) return new MoveData(Vector3.zero, null);
        MoveData returnVal = list[top];
        top--;
        return returnVal;
    }
}
