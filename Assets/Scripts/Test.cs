﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.A))
	    {
	        int id = Random.Range(1, 2);
	        Knapsack.Instance.StoreItem(id);
	    }	
	}
}
