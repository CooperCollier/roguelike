using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    //--------------------------------------------------------------------------------

    public float smoothSpeed = 0.125f;

    public Transform playerTransform;

    //--------------------------------------------------------------------------------

    void LateUpdate() {
    	transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
    }

    //--------------------------------------------------------------------------------

}