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
    int[][] Map =
    {
        new int[] { 1, 1, 1, 1, 1, 1, 1},
        new int[] { 1, 1, 1, 0, 0, 1, 1},
        new int[] { 1, 0, 3, 0, 0, 0, 1},
        new int[] { 1, 0, 1, 1, 0, 1, 1},
        new int[] { 1, 0, 1, 1, 0, 1, 1},
        new int[] { 1, 2, 1, 1, 4, 1, 1},
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
        for(int i = 0; i < Map.Length; i++)
        {
            for(int j = 0; j < Map[i].Length; j++)
            {
                switch (Map[i][j])
                {
                    case 0:
                        break;
                    case 1:
                        Instantiate(Wall, new Vector3(j, 0.5f, i), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(Player, new Vector3(j, 0.5f, i), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(Ball, new Vector3(j, 0.5f, i), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(Target, new Vector3(j, 0.5f, i), Quaternion.identity);
                        GameManager.Instance.PurposeCnt++;
                        break;
                    default:
                        Debug.LogError("³Ê, ¹¹ÇÏ´Â ÁþÀÌ³Ä...");
                        break;
                }
            }
        }
    }
}
