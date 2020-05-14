using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Ctrl : EnemyClass
{
	public bool lite; // 勾選的話,將只使用部分技能
	public float AtkCoolTime; // 攻擊的冷卻時間
	private bool AtkCool = false;

	public GameObject bullet_JumpBall1;

	public GameObject bullet1;
	public GameObject bullet2;
	public GameObject bullet3;

	public GameObject worfHead;// 技能之一的狼頭

	private Animator AniCtrl;

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(moveToPos(new Vector2(6.5f, 3)));

		GetPointObj();

		AniCtrl = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		DropPoint();

		if (lite)
		{
			switch (step)
			{
				case 1:

					if (!AtkCool)
					{
						if (HP > 600)
						{
							StartCoroutine(AtkCoolEnter(AtkCoolTime));
							AniCtrl.SetBool("Atk", true);
						}
						else
						{
							AtkCoolTime = 1;
							StartCoroutine(AtkCoolEnter(AtkCoolTime));
							AniCtrl.SetBool("Atk", true);
						}
					}
					break;
			}
			if (HP <= 0){
				Boss_dead(3);
				Dead();
			}

		}
		else
		{
			switch (step)
			{
				case 1:
					if (!AtkCool)
					{
						StartCoroutine(AtkCoolEnter(AtkCoolTime));
						Atk1(4);
					}
					break;
			}
		}


	}

	public void LiteAtk1()
	{
		// 帝丟出跳跳球
		int y;
		y = Random.Range(1, 3);
		if(y == 1)
		{
			StartCoroutine(moveToPos(new Vector2(Random.Range(5f, 6.1f), -2.14f)));
			Instantiate(bullet_JumpBall1, transform.position + new Vector3(Random.Range(-2, 2), 0, transform.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);

		}
		else
		{
			StartCoroutine(moveToPos(new Vector2(Random.Range(5f, 6.1f), 2.34f)));
			Instantiate(bullet_JumpBall1, transform.position + new Vector3(Random.Range(-2, 2f), 0, transform.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);

		}



	}

	public void Atk1(int bulletCnt)
	{
		// 白符王攻擊
		StartCoroutine(moveToPos(new Vector2(Random.Range(5f, 7f), Random.Range(-2.5f, 2.5f))));

		for(int i = 0; i < bulletCnt; i++)
		{
			Instantiate(bullet1, transform.position + new Vector3(Random.Range(0,1.5f),Random.Range(-1,1),transform.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);
		}
	}

	public IEnumerator AtkCoolEnter(float time) // 使用此函式讓攻擊進入冷卻
	{
		AtkCool = true;
		yield return new WaitForSeconds(time);
		AtkCool = false;
	}

	// 動畫相關函數
	private int blink;
	public void Blink()
	{
		// 五次wait後眨眼
		if(blink < 10)
		{
			blink++;
		}
		else
		{
			blink = 0;
			AniCtrl.SetBool("Blink", true);
		}
	}

	public void Wait()
	{
		AniCtrl.SetBool("Blink", false);
		AniCtrl.SetBool("Atk", false);
	}
}

