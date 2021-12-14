using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10.0f;

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
            DestroyObject();
        }
    }

    private void DestroyObject()
    {
        Destroy(transform.parent.gameObject);
        _speed = 10.0f;
    }

}
