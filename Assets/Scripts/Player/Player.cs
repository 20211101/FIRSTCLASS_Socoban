using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    MoveStack stack = new MoveStack();
    Vector3 UP = Vector3.forward;
    Vector3 DOWN = -Vector3.forward;
    Vector3 RIGHT = Vector3.right;
    Vector3 LEFT = -Vector3.right;
    void Update()
    {
        UpdateMove();
        UpdateGoBack();
    }

    public void UpdateMove()
    {
        RaycastHit hit;

        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W))
            moveDirection = UP;
        else if (Input.GetKeyDown(KeyCode.A))
            moveDirection = LEFT;
        else if (Input.GetKeyDown(KeyCode.S))
            moveDirection = DOWN;
        else if (Input.GetKeyDown(KeyCode.D))
            moveDirection = RIGHT;

        if (moveDirection == Vector3.zero) return;
        if (Physics.Raycast(transform.position, moveDirection, out hit, 1))
        {
            if (hit.collider.tag == "Wall")
            {
                // NOTHING
            }
            else if (hit.collider.tag == "Ball")
            {
                // 공에게 이동 신호 전달
                // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
                Ball hitBall = hit.collider.GetComponent<Ball>();
                bool isBallMove = hitBall.Move(moveDirection);
                if (isBallMove)
                {
                    stack.Add(new MoveData(moveDirection, hitBall));
                    gameObject.transform.position = gameObject.transform.position + moveDirection;
                }
            }
            else if (hit.collider.tag == "Target")
            {
                // 이동 허용
                gameObject.transform.position = gameObject.transform.position + moveDirection;
            }
        }
        else// 빈 공간일 때 이동
        {
            stack.Add(new MoveData(moveDirection, null));
            gameObject.transform.position = gameObject.transform.position + moveDirection;
        }
    }
    public void UpdateGoBack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveData data = stack.Pop();
            if (data.moveDirection == Vector3.zero)//스택이 없음을 의미
            { }
            else
            {
                transform.position -= data.moveDirection;
                if (data.movedBall != null)
                {
                    data.movedBall.Move(-data.moveDirection);
                }
            }
        }
    }
}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    if (Physics.Raycast(transform.position, UP, out hit, 1))
        //    {
        //        if (hit.collider.tag == "Wall")
        //        {
        //            // NOTHING
        //        }
        //        else if(hit.collider.tag == "Ball")
        //        {
        //            // 공에게 이동 신호 전달
        //            // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(UP);
        //            if(isBallMove) 
        //            {
        //                gameObject.transform.position = gameObject.transform.position + UP;
        //            }
        //        }
        //        else if(hit.collider.tag == "Target")
        //        {
        //            // 이동 허용
        //            gameObject.transform.position = gameObject.transform.position + UP;
        //        }
        //    }
        //    else// 빈 공간일 때 이동
        //    {
        //        gameObject.transform.position = gameObject.transform.position + UP;
        //    }
        //}
        //else if(Input.GetKeyDown(KeyCode.A))
        //{
        //    if (Physics.Raycast(transform.position, LEFT, out hit, 1))
        //    {
        //        if (hit.collider.tag == "Wall")
        //        {
        //            // NOTHING
        //        }
        //        else if (hit.collider.tag == "Ball")
        //        {
        //            // 공에게 이동 신호 전달
        //            // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(LEFT);
        //            if (isBallMove)
        //            {
        //                gameObject.transform.position = gameObject.transform.position + LEFT;
        //            }
        //        }
        //        else if (hit.collider.tag == "Target")
        //        {
        //            // 이동 허용
        //            gameObject.transform.position = gameObject.transform.position + LEFT;
        //        }
        //    }
        //    else
        //    {
        //        gameObject.transform.position = gameObject.transform.position + LEFT;
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.S))
        //{
        //    if (Physics.Raycast(transform.position, DOWN, out hit, 1))
        //    {
        //        if (hit.collider.tag == "Wall")
        //        {
        //            // NOTHING
        //        }
        //        else if (hit.collider.tag == "Ball")
        //        {
        //            // 공에게 이동 신호 전달
        //            // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(DOWN);
        //            if (isBallMove)
        //            {
        //                gameObject.transform.position = gameObject.transform.position + DOWN;
        //            }
        //        }
        //        else if (hit.collider.tag == "Target")
        //        {
        //            // 이동 허용
        //            gameObject.transform.position = gameObject.transform.position + DOWN;
        //        }
        //    }
        //    else
        //    {
        //        gameObject.transform.position = gameObject.transform.position + DOWN;
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.D))
        //{
        //    if (Physics.Raycast(transform.position, RIGHT, out hit, 1))
        //    {
        //        if (hit.collider.tag == "Wall")
        //        {
        //            // NOTHING
        //        }
        //        else if (hit.collider.tag == "Ball")
        //        {
        //            // 공에게 이동 신호 전달
        //            // bool 값 리턴 받아서 이동 성공 여부 확인 후 성공이면 공 있던 위치로 이동
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(RIGHT);
        //            if (isBallMove)
        //            {
        //                gameObject.transform.position = gameObject.transform.position + RIGHT;
        //            }
        //        }
        //        else if (hit.collider.tag == "Target")
        //        {
        //            // 이동 허용
        //            gameObject.transform.position = gameObject.transform.position + RIGHT;
        //        }
        //    }
        //    else
        //    {
        //        gameObject.transform.position = gameObject.transform.position + RIGHT;
        //    }
        //}