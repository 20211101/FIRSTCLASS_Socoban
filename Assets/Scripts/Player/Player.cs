using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Player : MonoBehaviour
{
    public MoveHistoryManager historyManager; 

    void Start()
    {
        historyManager = FindObjectOfType<MoveHistoryManager>(); // 가져오기
    }

    void Update()
    {
        RaycastHit hit; 
        Vector3 UP = transform.forward;
        Vector3 DOWN = -transform.forward;
        Vector3 RIGHT = transform.right;
        Vector3 LEFT = -transform.right;

        Vector3 moveDirection = Vector3.zero; 
        if (Input.GetKeyDown(KeyCode.W))
            moveDirection = UP;
        else if (Input.GetKeyDown(KeyCode.A))
            moveDirection = LEFT;
        else if (Input.GetKeyDown(KeyCode.S))
            moveDirection = DOWN;
        else if (Input.GetKeyDown(KeyCode.D))
            moveDirection = RIGHT;

        if (moveDirection != Vector3.zero)
        {
            // 오브젝트 위치에서, 이동방향으로 1만큼의 거리를 탐지하고, 탐지된 것을 hit에 담기
            if (Physics.Raycast(transform.position, moveDirection, out hit, 1))
            {
                if (hit.collider.tag == "Wall")         // 벽일 경우
                {
                    // NOTHING
                }
                else if (hit.collider.tag == "Ball")    // 공일 경우
                {
                    historyManager.SaveState(); // 볼과 플레이어 모두 이동 전에 저장
                    bool isBallMove = hit.collider.GetComponent<Ball>().Move(moveDirection);
                    if (isBallMove)
                    {
                        gameObject.transform.position = gameObject.transform.position + moveDirection;
                    }
                }
                else if (hit.collider.tag == "Target")  // 목표일 경우
                {
                    // 타겟일 때는 굳이 저장 X
                    gameObject.transform.position = gameObject.transform.position + moveDirection;
                }
            }
            else// 빈 공간일 때 이동
            {
                historyManager.SaveState(); // 플레이어만 움직일 때는 플레이어 이동 전에만 스택에 저장
                gameObject.transform.position = gameObject.transform.position + moveDirection;
            }
        }
        
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            historyManager.Undo(); // 되돌리기 함수 실행
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