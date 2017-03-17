﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
	private float nextSpawn = 0;
	public Transform prefabToSpawn;
	//public float spawnRate = 1;
	//public float randomDelay = 1;
	public AnimationCurve spawnCurve;
	public float curveLengthInSeconds = 30f;
	private float startTime;
	public float jitter = 0.25f;

	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time > nextSpawn)
		{
			Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
			//nextSpawn = Time.time + spawnRate + Random.Range(0, randomDelay);

			float curvePosition = (Time.time - startTime) / curveLengthInSeconds;

			if(curvePosition > 1f)
			{
				curvePosition = 1f;
				startTime = Time.time;
			}	

			nextSpawn = Time.time + spawnCurve.Evaluate(curvePosition) + Random.Range(-jitter, jitter);
		}
	}
}
