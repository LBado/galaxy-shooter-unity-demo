using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SpawnManager _spawnManager;
        
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        isGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }

    private void StartGame()
    {
        isGameOver = false;
        _spawnManager.StartSpawnManager();
        UIManager.Instance.ShowUI();
        
    }

    public void GameOver()
    {
        Debug.Log("G A M E  O V E R");
        isGameOver = true;
        _spawnManager.StopSpawnManager();
        UIManager.Instance.HideUI();
    }
}
