using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour {

    public float tumple;
    public Rigidbody rb;
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumple;   //得到一个x,y,z随机的向量
        //把ungularDrag（角阻力）设置为0，否则会逐渐停止
	}
}
