using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct MoveData
{
    // 이동 전에 위치를 저장 하는 데이터
    public Vector3 moveDirection;
    // 이동 할 때 같이 움직인 공이 있으면 해당 오브젝트 정보 담는 데이터
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
