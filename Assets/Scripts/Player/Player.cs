using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    void Start()
    {
        
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

        if (Physics.Raycast(transform.position, moveDirection, out hit, 1))
        {
            if (hit.collider.tag == "Wall")
            {
                // NOTHING
            }
            else if (hit.collider.tag == "Ball")
            {
                // ������ �̵� ��ȣ ����
                // bool �� ���� �޾Ƽ� �̵� ���� ���� Ȯ�� �� �����̸� �� �ִ� ��ġ�� �̵�
                bool isBallMove = hit.collider.GetComponent<Ball>().Move(moveDirection);
                if (isBallMove)
                {
                    gameObject.transform.position = gameObject.transform.position + moveDirection;
                }
            }
            else if (hit.collider.tag == "Target")
            {
                // �̵� ���
                gameObject.transform.position = gameObject.transform.position + moveDirection;
            }
        }
        else// �� ������ �� �̵�
        {
            gameObject.transform.position = gameObject.transform.position + moveDirection;
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
        //            // ������ �̵� ��ȣ ����
        //            // bool �� ���� �޾Ƽ� �̵� ���� ���� Ȯ�� �� �����̸� �� �ִ� ��ġ�� �̵�
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(UP);
        //            if(isBallMove) 
        //            {
        //                gameObject.transform.position = gameObject.transform.position + UP;
        //            }
        //        }
        //        else if(hit.collider.tag == "Target")
        //        {
        //            // �̵� ���
        //            gameObject.transform.position = gameObject.transform.position + UP;
        //        }
        //    }
        //    else// �� ������ �� �̵�
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
        //            // ������ �̵� ��ȣ ����
        //            // bool �� ���� �޾Ƽ� �̵� ���� ���� Ȯ�� �� �����̸� �� �ִ� ��ġ�� �̵�
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(LEFT);
        //            if (isBallMove)
        //            {
        //                gameObject.transform.position = gameObject.transform.position + LEFT;
        //            }
        //        }
        //        else if (hit.collider.tag == "Target")
        //        {
        //            // �̵� ���
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
        //            // ������ �̵� ��ȣ ����
        //            // bool �� ���� �޾Ƽ� �̵� ���� ���� Ȯ�� �� �����̸� �� �ִ� ��ġ�� �̵�
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(DOWN);
        //            if (isBallMove)
        //            {
        //                gameObject.transform.position = gameObject.transform.position + DOWN;
        //            }
        //        }
        //        else if (hit.collider.tag == "Target")
        //        {
        //            // �̵� ���
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
        //            // ������ �̵� ��ȣ ����
        //            // bool �� ���� �޾Ƽ� �̵� ���� ���� Ȯ�� �� �����̸� �� �ִ� ��ġ�� �̵�
        //            bool isBallMove = hit.collider.GetComponent<Ball>().Move(RIGHT);
        //            if (isBallMove)
        //            {
        //                gameObject.transform.position = gameObject.transform.position + RIGHT;
        //            }
        //        }
        //        else if (hit.collider.tag == "Target")
        //        {
        //            // �̵� ���
        //            gameObject.transform.position = gameObject.transform.position + RIGHT;
        //        }
        //    }
        //    else
        //    {
        //        gameObject.transform.position = gameObject.transform.position + RIGHT;
        //    }
        //}