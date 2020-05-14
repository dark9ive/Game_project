using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageInfo_", menuName = "StageInfoClass")]
public class StageInfoClass : ScriptableObject
{
    public float UpdateTime; // 一次計數的間隔,如UpdateTime = 0.2f,計時器就會以0.2秒為單位去判定一次陣列

    public int[] EnemyID;
    public int[] pos;// 強制指定生成位置,1 = 右方上排, 2 = 右方下排, 3 = 正上方, 4 = 正下方, 0 = 預設
    public int[] Cnt;// 是否會生成多隻一樣的敵人
    public int[] time;// 生成多隻一樣的敵人的間隔
    public float[] EnemyTime;// 若時間軸到達這個陣列的時間,將推進WAVE並生成對應的怪物
    public GameObject[] EnemyObj;
}
