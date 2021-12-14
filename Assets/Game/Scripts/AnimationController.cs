using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalMove = Input.GetAxisRaw("Horizontal");
        if (horizontalMove == 0)
        {
            IdleAnimation();
        }
        else if (horizontalMove == -1)
        {
            TurnLeft();
        }
        else if (horizontalMove == 1)
        {
            TurnRight();
        }


        /*
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _animator.SetBool("Turn_left", true);
            _animator.SetBool("Turn_right", false);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _animator.SetBool("Turn_left", false);
            _animator.SetBool("Turn_right", false);
        }
        
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _animator.SetBool("Turn_right", true);
            _animator.SetBool("Turn_left", false);
        }
        else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _animator.SetBool("Turn_right", false);
            _animator.SetBool("Turn_left", false);
        }
        */
    }

    private void IdleAnimation()
    {
        _animator.SetBool("Turn_left", false);
        _animator.SetBool("Turn_right", false);
    }

    private void TurnLeft()
    {
        _animator.SetBool("Turn_left", true);
        _animator.SetBool("Turn_right", false);
    }

    private void TurnRight()
    {
        _animator.SetBool("Turn_left", false);
        _animator.SetBool("Turn_right", true);
    }
}
