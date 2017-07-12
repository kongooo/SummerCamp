using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall : MonoBehaviour {

    private int hp = 2;
    public Sprite damageSprite;

	public void damage()             //砍两次墙后销毁
    {
        hp-=1;
        GetComponent<SpriteRenderer>().sprite = damageSprite;
        if (hp == 0)
            Destroy(this.gameObject);
    }
}
