using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mover : MonoBehaviour {
    public Rigidbody rb;
    public float speed;
	
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;     //设置子弹方向和速度
    }
	
	
}
