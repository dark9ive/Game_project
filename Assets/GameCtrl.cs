using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : MonoBehaviour
{
    public int playerStage = 1;

    public float Gravity;
    public float TerrainSpeed;

    public GameObject Point;
    public GameObject Power;

    private TerrainCtrl TerCtrl;

    void Start()
    {
        TerCtrl = GameObject.FindGameObjectWithTag("Terrain").GetComponent<TerrainCtrl>();
        // 連結地形控制腳本

        Time.timeScale = 1;// 調節FixedUpdate速率
    }

    void FixedUpdate()
    {
        StartCoroutine(ObstacleCounter(3));
    }

    private bool ObjMakeCool = false; // 此值為true時,停止生成障礙物

    public IEnumerator ObstacleCounter(float time) // time為幾秒生成一次障礙物 ObjCnt為一次生成的數量
    {
        if(!ObjMakeCool)
        {
            ObjMakeCool = true;

            TerCtrl.makeObstacle(playerStage);

            yield return new WaitForSeconds(time);

            ObjMakeCool = false;
        }
    }

    
}
