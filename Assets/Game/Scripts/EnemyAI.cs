using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyExpPrefab;

    private GameManager _gameManager;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        CheckPos();
        CheckIfGameOver();
    }

    private void CheckPos()
    {
        if (transform.position.y < -6f)
        {
            float randomPos = Random.Range(-8.0f, 8.0f);
            transform.position = new Vector3(randomPos, 6.0f, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //če smo zadeli player
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            
            if (player != null)
            {
                UpdateScore();
                //instanciramo animation prefab (eksplozijo)
                Instantiate(_enemyExpPrefab, this.gameObject.transform.position, Quaternion.identity);
                AudioSource.PlayClipAtPoint(_explosionSound, Camera.main.transform.position, 0.1f);
                Destroy(this.gameObject);
                player.Damage();
            }
        }
        //če smo zadeli laser
        else if (other.CompareTag("Laser"))
        {
            UpdateScore();
            //deaktiviramo laser ker ga imamo v poolu
            other.gameObject.SetActive(false);
            Instantiate(_enemyExpPrefab, this.gameObject.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_explosionSound, Camera.main.transform.position, 0.1f);
            Destroy(this.gameObject);
        }

    }

    //kličemo UImanager metodo ki poveča score za 1 in izpiše text
    private void UpdateScore()
    {
        UIManager.Instance.UpdateScore();
    }

    private void CheckIfGameOver()
    {
        if (_gameManager.isGameOver == true)
        {
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }
}
