using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 0 : Empty
// 1 : Wall
// 2 : Player
// 3 : Ball
// 4 : Target
public class MapCreator : MonoBehaviour
{
    //public enum MapObjectType
    //{
    //    EMPTY = 0,
    //    WALL = 1,
    //    PLAYER = 2,
    //    BALL = 3,
    //    TARGET = 4
    //}

    //const int E = (int)MapObjectType.EMPTY;
    //const int W = (int)MapObjectType.WALL;
    //const int P = (int)MapObjectType.PLAYER;
    //const int B = (int)MapObjectType.BALL;
    //const int T = (int)MapObjectType.TARGET;

    //int[][] Map =
    //{
    //    new int[] { W, W, W, W, W, W, W},
    //    new int[] { W, E, E, E, E, T, W},
    //    new int[] { W, E, B, B, E, E, W},
    //    new int[] { W, E, E, W, E, W, W},
    //    new int[] { W, E, E, W, E, W, W},
    //    new int[] { W, P, E, W, T, W, W},
    //    new int[] { W, W, W, W, W, W, W},
    //};

    int[][] Map =
    {
        new int[] { 1, 1, 1, 1, 1, 1, 1},
        new int[] { 1, 0, 0, 0, 0, 4, 1},
        new int[] { 1, 0, 3, 3, 0, 0, 1},
        new int[] { 1, 0, 0, 1, 0, 1, 1},
        new int[] { 1, 0, 0, 1, 0, 1, 1},
        new int[] { 1, 2, 0, 1, 4, 1, 1},
        new int[] { 1, 1, 1, 1, 1, 1, 1},
    };

    [SerializeField]
    GameObject Wall;
    [SerializeField]
    GameObject Player;
    [SerializeField]
    GameObject Ball;
    [SerializeField]
    GameObject Target;

    private void Awake()
    {
        int height = Map.Length;

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < Map[i].Length; j++)
            {
                // y축 기준을 배열 맨 아래부터 위로 뒤집기
                int reversedI = height - 1 - i;

                Vector3 spawnPos = new Vector3(j, 0.5f, reversedI);

                switch (Map[i][j])
                {
                    case 0:
                        break;
                    case 1:
                        Instantiate(Wall, spawnPos, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Player, spawnPos, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Ball, spawnPos, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(Target, spawnPos, Quaternion.identity);
                        GameManager.Instance.PurposeCnt++;
                        break;
                    default:
                        Debug.LogError("너, 뭐하는 짓이냐...");
                        break;
                }
            }
        }
    }

}
