using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject UI; 

    private static GameManager _Instance;
    public static GameManager Instance
    { get { return _Instance; } }

    // 공과 목적지가 겹쳐야 하는 수
    int purposeCnt = 0;
    public int PurposeCnt
    {
        set 
        { 
            purposeCnt = value;    
        }
        get { return purposeCnt; }
    }
    int curCnt = 0;
    public int CurCnt
    {
        set
        {
            curCnt = value;
            if(curCnt == purposeCnt)
            {
                GameClear();
            }
        }
        get { return curCnt; }
    }

    private void GameClear()
    {
        UI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void Awake()
    {
        if (_Instance != null)
            Destroy(gameObject);
        _Instance = this;
    }
}
