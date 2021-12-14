using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("UI manager not set!");
            }
            return _instance;
        }
    }

    [SerializeField]
    private Sprite[] _livesSpriteArr = new Sprite[4];

    public GameObject livesDisplay;

    public GameObject scoreText;

    public GameObject titleDisplay;

    private int _score;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        titleDisplay.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metoda ki "zamenja" sprite glede na to koliko lives imamo
    //ko pokličemo metodo v parameter damo število lives in temu primerno updejtamo sprite ki se nahaja v array
    public void UpdateHealth(int lives)
    {
        livesDisplay.GetComponent<Image>().sprite = _livesSpriteArr[lives];
    }

    public void UpdateScore()
    {
        _score++;
        scoreText.GetComponent<Text>().text = "Score: " + _score;
    }

    public void HideUI()
    {
        titleDisplay.SetActive(true);
        livesDisplay.SetActive(false);
        scoreText.SetActive(false);
    }

    public void ShowUI()
    {
        titleDisplay.SetActive(false);
        ResetScore();
        livesDisplay.SetActive(true);
        scoreText.SetActive(true);
    }

    public void ResetScore()
    {
        _score = 0;
        scoreText.GetComponent<Text>().text = "Score: " + _score;
    }
}
