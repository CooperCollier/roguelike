using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    
    //--------------------------------------------------------------------------------

    public float smoothSpeed = 0.125f;

    public Transform playerTransform;

    public bool shaking = false;

    //--------------------------------------------------------------------------------

    void LateUpdate() {
        if (!shaking) {
    	   transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
        }
    }

    //--------------------------------------------------------------------------------

    /* https://www.youtube.com/watch?v=9A9yj8KnM8c */

    public IEnumerator Shake(float duration, float magnitude) {

        shaking = true;
        
        Vector3 startPosition = transform.localPosition;
        float timeRemaining = duration;

        while (timeRemaining > 0f) {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition += new Vector3(x, y, 0);
            timeRemaining -= Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPosition;

        shaking = false;

    }

    //--------------------------------------------------------------------------------

}