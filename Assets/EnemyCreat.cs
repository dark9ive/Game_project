using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreat : MonoBehaviour
{
    public bool IsReady = false;
    public bool TimerEnble;// 為false時,停止計數
    public float GameTime;// 生怪的依據
    public int Wave;// 讀取陣列的第X值

    private GameCtrl GM;
    public StageInfoClass[] StageInfo;

    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameCtrl>();

        StartCoroutine( Timer (StageInfo [GM.playerStage].UpdateTime));
    }

    void FixedUpdate()
    {
        if(Wave < StageInfo[GM.playerStage].EnemyID.Length)
        {
            if (GameTime >= StageInfo[GM.playerStage].EnemyTime[Wave])
            {
                StartCoroutine(MakeEnemy(StageInfo[GM.playerStage].EnemyID[Wave], StageInfo[GM.playerStage].Cnt[Wave], StageInfo[GM.playerStage].time[Wave], StageInfo[GM.playerStage].pos[Wave]));
                Wave += 1;
            }
        }
    }

    public IEnumerator Timer(float time)
    {
        for(; TimerEnble; )
        {
            GameTime += time;
            yield return new WaitForSeconds(time);
        }

        yield return new WaitForSeconds(3);// 3秒後再度重啟,配合gm更換關卡資訊
        TimerEnble = true;
        StartCoroutine(Timer(StageInfo[GM.playerStage].UpdateTime));
    }

    public IEnumerator MakeEnemy(int ID, int Cnt, float time, int pos)
    {
        Vector3 ObjPos = new Vector3(0f, 0f, -10);

        switch(pos)
        {
            case 1:
                ObjPos = new Vector3(10.5f, 2.36f, -10);
                break;
            case 2:
                ObjPos = new Vector3(10.5f, -2f, -10);
                break;
        }

        for (int i = 0; i < Cnt; i++)
        {
            Instantiate(StageInfo[GM.playerStage].EnemyObj[ID], ObjPos, Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);
            yield return new WaitForSeconds(time);
        }
    }
}
