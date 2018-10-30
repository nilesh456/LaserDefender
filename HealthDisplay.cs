﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    Text healthText;
    Playermovement player;

    // Use this for initialization
    void Start () {
        healthText = GetComponent<Text>();
        player = FindObjectOfType<Playermovement>();

    }
	
	// Update is called once per frame
	void Update () {
        healthText.text = player.GetHealth().ToString();    
    }
}
