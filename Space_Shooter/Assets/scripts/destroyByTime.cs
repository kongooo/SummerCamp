﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyByTime : MonoBehaviour {

    public float lifeTime;
	
	void Start () {
        Destroy(gameObject, lifeTime);       //存活lifeTime时间后销毁
	}
	
	
}
