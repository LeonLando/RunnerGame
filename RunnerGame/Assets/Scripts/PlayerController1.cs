using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] private Rigidbody _playerRig;
    [SerializeField] private float _speed;
    private float _movedLines; 
    void Start()
    {
        _movedLines = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _movedLines > -1)
        {
            _playerRig.AddForce(-_speed, 0, 0, ForceMode.Impulse);
            Debug.Log("left");
        }
        if (Input.GetKeyDown(KeyCode.D) && _movedLines < 1)
        {
            _playerRig.AddForce(_speed, 0, 0, ForceMode.Force);
            Debug.Log("right");
        }

    }
}
