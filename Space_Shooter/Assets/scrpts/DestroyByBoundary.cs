using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

	void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);           //销毁飞出边界的物体
    }

    
}
