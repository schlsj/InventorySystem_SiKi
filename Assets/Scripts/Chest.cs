﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Inventory {
    private static Chest _instance;

    public static Chest Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("ChestPanel").GetComponent<Chest>();
            }
            return _instance;
        }
    }
}
