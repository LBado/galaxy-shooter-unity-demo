using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private GameObject _explosion;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private float _speed = 12.5f;

    [SerializeField]
    private float _fireDelay = 0.3f;

    private float _nextFire = 0.0f;

    public int health = 3;

    [SerializeField]
    private GameObject[] _engineFailure = new GameObject[2];

    //[SerializeField]
    //private GameObject _tripleShot;

    [SerializeField]
    private bool canUseTripleShot = false;

    [SerializeField]
    private bool canUseSpeedUp = false;

    [SerializeField]
    private bool hasShield = false;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _laserSound;

    [SerializeField]
    private AudioClip _explosionSound;

    [SerializeField]
    private AudioClip _powerUpSound;

    private int _hitCount;

    private int _previousRand;

    // Start is called before the first frame update
    void Start()
    {
        //na začetku health postavimo na 3
        UIManager.Instance.UpdateHealth(health);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _audioSource = GetComponent<AudioSource>();

        _shield.SetActive(false);

        _hitCount = 0;

        _previousRand = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        CheckPositionX();

        CheckPositionY();

        /*
         * Time.time šteje koliko časa je preteklo od začetka
         * _nextfire je trenutno nič a po 60sekundah bo 60 + 0.5sek(fire rate oz time delay) = 60.5
         * če je Time.time(je recimo trenutno 65 sek od začetka) presegel 60.5sec(_nextFire) potem lahko spet ustrelimo laser
        */

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootLaser(canUseTripleShot);
        }

    }

    private void CheckPositionX()
    {
        //if moved offscreen, appear on other side
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }

    }

    private void CheckPositionY()
    {
        //block player from moving further than limit
        if (transform.position.y > 4.0f)
        {
            transform.position = new Vector3(transform.position.x, 4.0f, 0);
        }

        else if (transform.position.y < -4.0f)
        {
            transform.position = new Vector3(transform.position.x, -4.0f, 0);
        }
    }

    private void Movement()
    {
        int speedMultiplier = 1;
        //če je canusespeedup true potem setamo hitrost *2, speedmultiplier je lokalna spremenljivka
        if (canUseSpeedUp == true)
        {
            speedMultiplier = 2;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 horizontalMovement = new Vector3(horizontalInput, 0, 0);
        transform.Translate(horizontalMovement * (_speed * speedMultiplier) * Time.deltaTime);

        float verticalInput = Input.GetAxis("Vertical");
        Vector3 verticalMovement = new Vector3(0, verticalInput, 0);
        transform.Translate(verticalMovement * (_speed * speedMultiplier) * Time.deltaTime);

    }

    private void ShootLaser(bool tripleShot)
    {
        if (Time.time > _nextFire)
        {
            _audioSource.PlayOneShot(_laserSound);
            _nextFire = Time.time + _fireDelay;

            Vector3 playerPos = transform.position;
            Vector3 laserPos = playerPos + new Vector3(0, 0.9f, 0);

            //array vektorjev3 z pozicijami laser shooterjev/ srednji, desni, levi
            Vector3[] positions = new Vector3[3]
            {
                playerPos + new Vector3(0, 0.9f, 0),
                playerPos + new Vector3(0.55f, 0, 0),
                playerPos + new Vector3(-0.55f, 0, 0)
            };

            if (tripleShot == true)
            {
                for (int i = 0; i < 3; i++)
                {
                    GameObject tLaser = PoolManager.Instance.RequestLaser();
                    tLaser.transform.position = positions[i];
                }
                //1. način ki requesta 3 laserje in jih razporedi - relativno dolg način ki se da narediti bolj elegantno
                /*
                GameObject laser1 = PoolManager.Instance.RequestLaser();
                GameObject laser2 = PoolManager.Instance.RequestLaser();
                GameObject laser3 = PoolManager.Instance.RequestLaser();

                laser1.transform.position = laserPos;
                laser2.transform.position = transform.position + new Vector3(0.55f, 0, 0);
                laser3.transform.position = transform.position + new Vector3(-0.55f, 0, 0);
                */

                //2. gi način ki instanciira/naredi prefab triple shot - ne uporablja poola
                //Instantiate(_tripleShot, transform.position, Quaternion.identity);
                
            }
            else
            {
                //requestamo laser iz instanciranih laserjev in ga pozicioniramo pred player
                GameObject laser = PoolManager.Instance.RequestLaser();
                laser.transform.position = laserPos;
            }

        }
    }

    public void StartPowerUpTimer(int id)
    {
        _audioSource.PlayOneShot(_powerUpSound);
        switch (id)
        {
            case 0:
                canUseTripleShot = true;
                StartCoroutine(TripleShotCorutine());
                break;
            case 1:
                canUseSpeedUp = true;
                StartCoroutine(SpeedUpCorutine());
                break;
            case 2:
                hasShield = true;
                _shield.SetActive(true);
                break;
            default:
                break;
        }
    }
    //korutina za power down po 3sec
    public IEnumerator TripleShotCorutine()
    {
        yield return new WaitForSeconds(3.0f);
        canUseTripleShot = false;
    }

    public IEnumerator SpeedUpCorutine()
    {
        yield return new WaitForSeconds(3.0f);
        canUseSpeedUp = false;
    }
    
    //generiramo random value ki ni prejšni value(prejšni value generiramo na začetku)
    //trenutni value(rand) shranimo v prejšnega(_previousRand)
    private int RandomValue()
    {
        int rand = Random.Range(0, 2);
        while (rand == _previousRand)
        {
            rand = Random.Range(0, 2);
        }
        _previousRand = rand;
        return rand;
    }

    //damage funkcija je bolj smiselna kot da bi poklical samo health--
    //je del playerja - kaj dela - se poškoduje
    public void Damage()
    {
        if (hasShield == true)
        {
            _shield.SetActive(false);
            hasShield = false;
            //return je ubistvu da se izvajanje funkcije zaključi
            return;
        }

        _hitCount++;

        int random = RandomValue(); //_previousRand, rand = 0

        //če je rand/random 0 potem damageamo levi engine drugače desnega
        //ne moremo 2x izbrati istega
        if (random == 0)
        {
            _engineFailure[0].SetActive(true);
        }
        else
        {
            _engineFailure[1].SetActive(true);
        }

        health--;
        UIManager.Instance.UpdateHealth(health);
        if (health == 0)
        {
            Instantiate(_explosion, this.gameObject.transform.position, Quaternion.identity);
            //kliče game over v GameManager ki ustavi spawnanje in pokaže title screen
            _gameManager.GameOver();
            AudioSource.PlayClipAtPoint(_explosionSound, Camera.main.transform.position, 0.1f);
            Destroy(this.gameObject);
        }
    }
}
