using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]   //连接Boundary类与MonoBehaviour类，使Boundary类中的变量能够显示在unity上
public class Boundary
{
    public float xmin, xmax, zmin, zmax;
}

public class PlayerController : MonoBehaviour {
    public Rigidbody rb;
    public float speed=10;
    public Boundary boundary;
    public float titl;
    public GameObject shoot;
    public Transform shootSpawn;
    public float fireDelta;   //设置开火间隔时间
    private float nextFire;   //默认为0
    public AudioSource ad;
    
    void Start () {
         rb = GetComponent<Rigidbody>();   //用rb变量引用rigidbody组件
         ad = GetComponent<AudioSource>();
	}
	
	
	void Update () {
        
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time+ fireDelta;
             Instantiate(shoot,shootSpawn.position, shootSpawn.rotation);  //实例化子弹在飞船位置
            ad.Play();    //发射子弹时音效
        }
    }

     void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement*speed;
        rb.position = new Vector3(                                       //限制飞船位置
            Mathf.Clamp(rb.position.x, boundary.xmin, boundary.xmax),    
            0.0f,
            Mathf.Clamp(rb.position.z,boundary.zmin,boundary.zmax)
            );
        rb.rotation = Quaternion.Euler(0.0f,0.0f,rb.velocity.z*titl);   //设置飞船沿y轴的旋转
    }
}
