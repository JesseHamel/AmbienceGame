using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour {

    public float speed = 10;

	// Use this for initialization
	void Start () {
        //StartCoroutine(SpeedUp());
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.left * speed * Time.deltaTime;
	}

    //IEnumerator SpeedUp()
    //
    //    speed = speed + 1;
    //    yield return new WaitForSeconds(1f);
    //    StartCoroutine(SpeedUp());
    //}
}
