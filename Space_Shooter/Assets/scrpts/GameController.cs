﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValues;

    public int hazardCount;
    private int score = 0;

    public float spawnWait;
    public float waveWait;
    public float startWait;

    public GUIText scoreText;   
    public GUIText gameover_text;
    public GUIText restart_text;

    public bool gameover;
    public bool restart;
	
	void Start () {
        StartCoroutine( SpawnWaves());    //开始协程
        scoreText.text = "score: " + score;     //初始化text
        gameover_text.text = "";
        restart_text.text = "";
        gameover = false;
        restart = false;
	}
	IEnumerator  SpawnWaves()   //自定义协程
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            yield return new WaitForSeconds(waveWait);         //开始游戏前暂停waveWait
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;        //四元数无旋转？？？
                Instantiate(hazard, spawnPosition, spawnRotation);   //x轴随机实例化石块
                yield return new WaitForSeconds(spawnWait);        //第一波石块发出后暂停spawnWait
            }
        }
    }
    
    public void addScore(int newScoreValue)         //为scoretext添加分数
    {
        score += newScoreValue;
        scoreText.text = "Score: " + score;              
    }


}
