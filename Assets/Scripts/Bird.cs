using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
        }

    }
    
    private void Jump(){
        
    }
}
