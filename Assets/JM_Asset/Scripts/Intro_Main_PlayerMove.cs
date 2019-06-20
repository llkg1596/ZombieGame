using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Main_PlayerMove : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 100.0f;

    private Animator playerAnimator;
    private Rigidbody playerRigidbody;

    // Start is called before the first frame update
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        Rotate();
        playerAnimator.SetFloat("Move", 10.0f);
    }
   
    private void Rotate()
    {
        float turn = rotateSpeed * Time.deltaTime;
        playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0, turn, 0f);
    }
}
