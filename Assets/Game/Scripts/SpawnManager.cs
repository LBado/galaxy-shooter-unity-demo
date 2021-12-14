using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyShipPre;

    [SerializeField]
    private GameObject[] _powerups = new GameObject[3];

    [SerializeField]
    private GameObject _player;

    private GameManager _gameManager;

    //korutino shranimo v variable IEnumerator
    private IEnumerator _enemySpawnCorutine;
    private IEnumerator _powerUpSpawnCorutine;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _enemySpawnCorutine = EnemySpawnRoutine();
        _powerUpSpawnCorutine = PowerupSpawnRoutine();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //korutina ki vsako 1 sec in pol spawna enemy
    private IEnumerator EnemySpawnRoutine()
    {
        do
        {
            yield return new WaitForSeconds(1.5f);
            Instantiate(_enemyShipPre, RandomRange(), Quaternion.identity);
        } while (_gameManager.isGameOver == false);
    }

    //korutina ki vsakih 5sec spawna random powerup
    private IEnumerator PowerupSpawnRoutine()
    {
        do
        {
            yield return new WaitForSeconds(5.0f);
            Instantiate(_powerups[Random.Range(0, 3)], RandomRange(), Quaternion.identity);

         //dokler igra traja (isgameover == false) spawna enemies
        } while (_gameManager.isGameOver == false);
    }

    private Vector3 RandomRange()
    {
        return new Vector3(Random.Range(-8, 8), 6, 0);
    }

    public void StartSpawnManager()
    {
        Instantiate(_player);
        StartCoroutine(_enemySpawnCorutine);
        StartCoroutine(_powerUpSpawnCorutine);
    }

    public void StopSpawnManager()
    {
        Debug.Log("S T O P P I N G  S P A W N  M A N A G E R");
        StopCoroutine(_enemySpawnCorutine);
        StopCoroutine(_powerUpSpawnCorutine);
    }

}
