﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inactiveOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once perframe
    void Update()
    {
        
    }
}
