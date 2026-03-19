using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 0 : Empty
// 1 : Wall
// 2 : Player
// 3 : Ball
// 4 : Goal
public class MapCreator : MonoBehaviour
{
    private int[][] map =
    {
        new int[] { 1, 1, 1, 1, 1, 1, 1 },
        new int[] { 1, 1, 1, 0, 0, 1, 1 },
        new int[] { 1, 0, 3, 0, 0, 0, 1 },
        new int[] { 1, 0, 1, 1, 0, 1, 1 },
        new int[] { 1, 0, 1, 1, 0, 1, 1 },
        new int[] { 1, 2, 1, 1, 4, 1, 1 },
        new int[] { 1, 1, 1, 1, 1, 1, 1 },
    };

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject goalPrefab;

    public StageInfo CreateStage()
    {
        List<Goal> createdGoals = new List<Goal>();
        Player createdPlayer = null;

        for (int z = 0; z < map.Length; z++)
        {
            for (int x = 0; x < map[z].Length; x++)
            {
                Vector3 spawnPosition = new Vector3(x, 0.5f, z);

                switch (map[z][x])
                {
                    case 0:
                        break;

                    case 1:
                        Instantiate(wallPrefab, spawnPosition, Quaternion.identity);
                        break;

                    case 2:
                        {
                            GameObject playerObject = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
                            createdPlayer = playerObject.GetComponent<Player>();

                            if (createdPlayer == null)
                                Debug.LogError("Player 프리팹에 Player 컴포넌트가 없습니다.");

                            break;
                        }

                    case 3:
                        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
                        break;

                    case 4:
                        {
                            GameObject goalObject = Instantiate(goalPrefab, spawnPosition, Quaternion.identity);
                            Goal goal = goalObject.GetComponent<Goal>();

                            if (goal == null)
                            {
                                Debug.LogError("Goal 프리팹에 Goal 컴포넌트가 없습니다.");
                                break;
                            }

                            createdGoals.Add(goal);
                            break;
                        }

                    default:
                        Debug.LogError("정의되지 않은 맵 값입니다.");
                        break;
                }
            }
        }

        return new StageInfo(createdPlayer, createdGoals.ToArray());
    }
}