using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour {

    private float NextSpawn = 0;
    public Transform PrefabToSpawn;
    public AnimationCurve SpawnCurve;
    public float CurveLengthInSeconds = 30f;
    private float StartTime;
    public float Jitter = 0.25f;


	// Use this for initialization
	void Start () {
        StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(Time.time > NextSpawn)
        {
            Instantiate(PrefabToSpawn, transform.position, Quaternion.identity);
            //NextSpawn = Time.time + SpawnRate + Random.Range(0, RandomDelay);

            float CurvePos = (Time.time - StartTime) / CurveLengthInSeconds;
            if(CurvePos > 1f)
            {
                CurvePos = 1f;
                StartTime = Time.time;
            }

            NextSpawn = Time.time + SpawnCurve.Evaluate(CurvePos) + Random.Range(-Jitter, Jitter);

        } 

	}
}
