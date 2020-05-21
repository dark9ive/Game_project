using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyClass : MonoBehaviour
{
	public int MaxHP;// 最大生命值
	public int HP;// 目前生命值
	public int step;// 目前為第幾階段
	public int MaxStep;// 總共有幾階段
	public int TouchDamage; // 玩家碰到此怪物會受到多少傷害
	public int Drop_PointCnt;
	public int Drop_PowerCnt;
	public int Drop_PointDamage; // 每次怪物受到多少傷害時掉落P點和藍點
	public int Drop_PointDamageInt;

	public float speed;

	private GameCtrl GM;
	private GameObject Point;
	private GameObject Power;

	public void GetPointObj()
	{
		if (MaxStep != 0)
			step = 1;

		HP = MaxHP;
		GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameCtrl>();

		Point = GM.Point;
		Power = GM.Power;
	}

	private bool A = false;// 使程式不進行多次除法
	private float x;// 用於隨機處理點的掉落方向
	private float y;
	public void DropPoint()
	{
		if(!(Drop_PointDamage == 0))// 若無設定此值則為小怪單位
		{
			if (!A)
			{
				Drop_PointDamageInt = (MaxHP / Drop_PointDamage) - 1;
				A = true;
			}
		}

		if(HP < Drop_PointDamageInt * Drop_PointDamage)
		{
			if (MaxStep != 0 && step != 0)
				step += 1;

			Drop_PointDamageInt -= 1;
			DropPointByManual(Drop_PointCnt, Drop_PowerCnt);
		}
	}

	public void DropPointByManual(int PointCnt, int PowerCnt)// Manual手動,此函式用於強制掉落指定數量的P點和藍點
	{
		for (int i = 0; i < PointCnt; i++)
		{
			x = Random.Range(-2.1f, 2.1f);
			y = Random.Range(-2.1f, 2.1f);

			Instantiate(Point, transform.position + new Vector3(x, y, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);
		}

		for (int i = 0; i < PowerCnt; i++)
		{
			x = Random.Range(-2.1f, 2.1f);
			y = Random.Range(-2.1f, 2.1f);

			Instantiate(Power, transform.position + new Vector3(x, y, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);
		}
	}

	public void Dead()
	{
		DropPointByManual(Drop_PointCnt, Drop_PowerCnt);
		Destroy(gameObject);
	}

	private Vector2 _pos;
	private Vector2 dir;
	private float z;
	private bool Moving; // 防呆用
	public IEnumerator moveToPos(Vector2 pos)
	{
		if (Moving)
		{
			//Debug.Log("已在移動中");
			yield break;
		}

		Moving = true;
		z = transform.position.z;
		for (; !((transform.position.x < pos.x + 0.2f) && (transform.position.x > pos.x - 0.2f) && (transform.position.y < pos.y + 0.2f) && (transform.position.y > pos.y - 0.2f));)
		{
			_pos = transform.position;

			dir = pos - _pos;

			_pos += dir * speed * Time.deltaTime;
			gameObject.transform.position = new Vector3(_pos.x, _pos.y, z);

			yield return 1;
		}
		Moving = false;
	}

	private BGMCtrl BGMM ;
	public void Boss_dead(int BGMIndex)// 不論是小王或是王,都會運行,並且跑動畫然後改變stageInfo,小王會將其改成該面的下半段
	{
		GM.playerStage += 1;
		BGMM = GameObject.FindGameObjectWithTag("BGMManager").GetComponent<BGMCtrl>();
		BGMM.ReadyToPlayBGM = BGMIndex;
	}
}
