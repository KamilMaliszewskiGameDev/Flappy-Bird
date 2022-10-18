using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float JUMP_AMOUNT = 100f;
    private Rigidbody2D rigidBody2D;

    private void Awake(){
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            Jump();
        }

    }
    
    private void Jump(){
        rigidBody2D.velocity = Vector2.up * JUMP_AMOUNT;
    }
}
