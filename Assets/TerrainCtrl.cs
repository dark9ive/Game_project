using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCtrl : MonoBehaviour
{
    private GameCtrl GM;

    public GameObject[] obstacle;

    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameCtrl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position -= new Vector3(GM.TerrainSpeed, 0, 0);
    }
    
    private float ObjPosY = 0;

    public void makeObstacle(int stage) // 障礙物生成函數
    {
        switch (stage)
        {
            case 1: // 第一關會生成的物件資訊
                if (Random.Range(0, 2) == 0)// 只為 0 or 1 , 0表示物件位置在下層
                    ObjPosY = -4.5f;
                else
                    ObjPosY = 0;

                Instantiate(obstacle[Random.Range(0, 6)], new Vector3(17.79f, ObjPosY, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Terrain").transform);
                // ^^決定生成物件的種類,為陣列中0~5的物件
                break;
        }
    }
}
