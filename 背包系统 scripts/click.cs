using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour {
	
	
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Input!");
            int index = Random.Range(1, 5);
            BackpackManager._instance.storeItem(index);
        }
	}
}
