using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Pool manager is null!");
            }

            return _instance;
        }
    }

    //laser container je empty gameobject kamor bodo šli vsi laserji(za boljšo preglednost)
    [SerializeField]
    private GameObject _laserContainer;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private List<GameObject> _laserPool = new List<GameObject>();

    [SerializeField]
    private bool _generateMore = true;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        //na začetku instanciramo 10 laserjev ki jih bomo uporabili
        GeneratePool(12);
    }

    //metoda ki instancira laserje in jih doda v _laserPool list
    private void GeneratePool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var laser = Instantiate(_laserPrefab);
            laser.transform.parent = _laserContainer.transform;
            laser.SetActive(false);
            _laserPool.Add(laser);
        }

    }

    //metoda ki returna neaktiven laser playerju in ga nato aktivira
    //ko returna se funkcija zaključi
    public GameObject RequestLaser()
    {
        foreach (var laser in _laserPool)
        {
            //activeinhierarchy = če je aktiven v scene
            if (laser.activeInHierarchy == false)
            {
                laser.SetActive(true);
                return laser;
            }
            

        }

        //v scene ni našel neaktivnega laserja in ni še zaključil metode z returnom
        //ker smo mu dovolili da generira nove laserje bo generiral novega, ga dodal v hierarhijo in ga returnal
        if (_generateMore == true)
        {
            Debug.Log("Generating more");
            var newLaser = Instantiate(_laserPrefab);
            newLaser.transform.parent = _laserContainer.transform;
            _laserPool.Add(newLaser);
            return newLaser;
        }

        return null;
            
    }




}
