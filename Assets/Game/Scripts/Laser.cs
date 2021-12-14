using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;
    //ko se laser enejbla(ko preide iz active=false v active=true)
    //po 2 sekundah invokamo metodo ki spremeni active=false
    /*private void OnEnable()
    {
        Invoke("DisableObject", 4f);
    }
    */

    private void Update()
    {

        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        //naredimo da gre laser čedalje hitreje
        _speed *= 1.03f;
        CheckLaserPos();
    }


    private void CheckLaserPos()
    {
        if (transform.position.y > 6.0f)
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
        _speed = 10.0f;
    }
}
