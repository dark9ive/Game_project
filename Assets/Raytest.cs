using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raytest : MonoBehaviour
{
	// =======================連結腳本======================= //
	private GameCtrl GMObj;
	// =======================射線相關變數======================= //
	public float DownRayLong; // 用於偵測地面的射線長度
	public Vector3 DownRayPos; // 用於偵測牆面的射線初始位置

	public float FrontRayLong; // 用於偵測牆面的射線長度
	public Vector3 FrontRayPos; // 用於偵測牆面的射線初始位置

	public float UpRayLong; // 用於偵測頭頂方塊的射線長度
	public Vector3 UpRayPos; // 用於偵測頭頂方塊的射線長度

	public float BackRayLong; // 用於偵測頭頂方塊的射線長度
	public Vector3 BackRayPos; // 用於偵測頭頂方塊的射線長度

	public float GRayLong; // 用於偵測頭頂方塊的射線長度

	public Ray DownRay;
	public Ray FrontRay;
	public Ray UpRay;
	public Ray BackRay;
	public Ray GRay;
	public RaycastHit2D DownHit;
	public RaycastHit2D FrontHit;
	public RaycastHit2D UpHit;
	public RaycastHit2D BackHit;
	public RaycastHit2D GHit;
	// =======================重力相關變數======================= //
	private float Gravity;
	public Vector3 Velocity;
	private float deltaTime;
	public Vector3 deltaMove;
	// =======================移動相關變數======================= //
	private bool OnUpBlock = true;// 玩家是否正在上方塊,預設為是
	private bool TeleportCool = false;// 玩家的傳送是否正冷卻
	public float TeleportCoolTime;// 限制玩家的傳送頻率

	public bool Jumping = false;
	public float JumpPower; // 影響跳躍高度

	public bool Sliding = false;
	// =======================角色狀態列======================= //
	public int HP;
	public int Power;

	private bool Invincible; // 若此值true,玩家不會受到任何傷害
	public float InvincibleTime; // 與TeleportCool同計算方式

	private SpriteRenderer SP; // 角色圖示閃爍用

	public bool LockCool = false; // 鎖定鍵的冷卻
	public GameObject target; // 會讓子彈讀取你鎖定的目標
	void Start()
	{
		SP = GetComponent<SpriteRenderer>();
	}

	void Update()
	{

	}

	void FixedUpdate()
	{
		// =======================連結腳本======================= //
		GMObj = GameObject.FindGameObjectWithTag("GM").GetComponent<GameCtrl>();

		Gravity = GMObj.Gravity;
		// =======================射線處理======================= //
		Debug.DrawRay(transform.position + DownRayPos, Vector3.right * DownRayLong);// 顯示射線
		Debug.DrawRay(transform.position, Vector3.down * GRayLong);// 顯示射線

		if(Sliding)
		{

		}
		else
			DownRayLong = 0.9f + Mathf.Abs(deltaMove.y);

		DownRay = new Ray(transform.position + DownRayPos, Vector3.right); //更新射線位置
		GRay = new Ray(transform.position, Vector3.down); //更新射線位置


		if (Sliding)
		{
			GRayLong = 0.49f + Mathf.Abs(deltaMove.y);

			FrontRayLong = 0.5f;
			BackRayLong = 0.5f;

			DownRay = new Ray(transform.position + new Vector3(0f, 0.3f, 0) + DownRayPos, Vector3.right);
			FrontRay = new Ray(transform.position + new Vector3(0.85f, 0.2f, 0), Vector3.down);
			BackRay = new Ray(transform.position + new Vector3(-0.85f, 0.2f, 0), Vector3.down);
			Debug.DrawRay(transform.position + new Vector3(0.85f, 0.2f, 0), Vector3.down * FrontRayLong);// 顯示射線
		}
		else
		{
			GRayLong = 0.86f + Mathf.Abs(deltaMove.y);

			FrontRayLong = 1.0f;
			BackRayLong = 1.0f;

			FrontRay = new Ray(transform.position + FrontRayPos, Vector3.down);
			BackRay = new Ray(transform.position + BackRayPos, Vector3.down);
			Debug.DrawRay(transform.position + FrontRayPos, Vector3.down * FrontRayLong);// 顯示射線
		}

		DownHit = Physics2D.Raycast(DownRay.origin, DownRay.direction, DownRayLong, -1);
		FrontHit = Physics2D.Raycast(FrontRay.origin, FrontRay.direction, FrontRayLong, -1);
		UpHit = Physics2D.Raycast(UpRay.origin, UpRay.direction, UpRayLong, -1);
		BackHit = Physics2D.Raycast(BackRay.origin, BackRay.direction, BackRayLong, -1);
		GHit = Physics2D.Raycast(GRay.origin, GRay.direction, GRayLong, -1);

		// =======================移動處理======================= //

		// 射線判定地板分成重力線和底線
		// 重力線隨速度改變長度,用於防止玩家陷入地下
		// 底線只判定腳下,用於防止在踩到障礙物時重力射線被阻擋所導致不斷往下掉的問題
		if ((DownHit.collider && (DownHit.collider.tag == "block")) || (GHit.collider && (GHit.collider.tag == "block")))// 偵測下方物體
		{
			// 射線碰到方塊

			if (Jumping)// 重製跳躍
				Jumping = false;

			// 除錯用,當射線碰到方塊,將射線長度變成方塊長度,並將各數值歸零
			deltaMove = new Vector3(0, 0, 0);
			Velocity.y = 0;
		}
		else
		{
			// 如果射線沒偵測到Block,將會往下掉
			GravityMove();
		}

		if (FrontHit.collider && (FrontHit.collider.tag == "Obstacle"))// 偵測右方物體
		{

			//gameObject.transform.position -= new Vector3(0.2f, 0f, 0);
			GivePlayerDamage(10, 1.5f);// 如果撞到方塊,將受到傷害
			//Debug.Log("R Block Hit");
		}
		else if (BackHit.collider && (BackHit.collider.tag == "Obstacle"))// 偵測左方物體
		{
			GivePlayerDamage(10, 1.5f);// 如果撞到方塊,將受到傷害
			//Debug.Log("L Block Hit");
		}

		if (FrontHit.collider && (FrontHit.collider.tag == "Enemy"))// 偵測右方物體
		{
			GivePlayerDamage(FrontHit.collider.GetComponent<EnemyClass>().TouchDamage, 1.5f);// 如果撞到敵人,將受到傷害
			//Debug.Log("R Enemy Hit");
		}
		else if (BackHit.collider && (BackHit.collider.tag == "Enemy"))// 偵測左方物體
		{
			GivePlayerDamage(10, 1.5f);// 如果撞到敵人,將受到傷害
			//Debug.Log("L Enemy Hit");
		}
		// =======================Input處理======================= //
		if (Input.GetKeyDown(KeyCode.Z))
		{
			if(!TeleportCool)
			{
				if(OnUpBlock)
				{
					transform.position += new Vector3(0, -4.5f, 0);
					OnUpBlock = false;
					StartCoroutine(teleportCoolConut(TeleportCoolTime)); // 開始冷卻時間的倒數
				}
				else
				{
					transform.position += new Vector3(0, 4.5f, 0);
					OnUpBlock = true;
					StartCoroutine(teleportCoolConut(TeleportCoolTime));
				}
			}
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			if(!Sliding)
			{
				DownRayLong += 0.3f;
				if (DownHit.collider && (DownHit.collider.tag == "block") || (GHit.collider && (GHit.collider.tag == "block")))
				{
					transform.position -= new Vector3(0, DownRayLong - 1.7f, 0);
					Slie();
				}
			}
		}
		else
		{
			if (Sliding)
			{
				Stand();
			}
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			if (Sliding)// 滑行時若使用跳躍指令會中斷滑行
			{
				Stand();
			}

			if (!Jumping)
			{
				gameObject.transform.position += new Vector3(0f, 0.2f, 0);
				Jumping = true;
				Velocity.y = -JumpPower;
			}
		}

		if(Input.GetKeyDown(KeyCode.C))
		{
			// 鎖定目標
			if (!LockCool)
				StartCoroutine(findTarget());
		}

		// =======================狀態處理======================= //
		if (Power > 150)
			Power = 150;
	}

	public IEnumerator findTarget()
	{
		LockCool = true;

		GameObject _target = null;
		GameObject[] enemys;

		enemys = GameObject.FindGameObjectsWithTag("Enemy");

		for (int i = 0; i < enemys.Length; i++){
			if (!_target){
				_target = enemys[i];
			}
			else{
				if(_target.transform.position.x - transform.position.x > enemys[i].transform.position.x - transform.position.x && enemys[i].transform.position.x - transform.position.x >= 0){
					// 比較目標與玩家的距離,近的為優先
					_target = enemys[i];
				}
			}
		}
		if (_target){
			if(_target.transform.position.x - transform.position.x < 0){
				_target = null;
			}
			target = _target;
		}
		else{
			Debug.Log("No Target!");
		}

		yield return new WaitForSeconds(0.1f);

		LockCool = false;
	}

	void GravityMove()
	{
		deltaTime = Time.deltaTime;
		Velocity.y += Gravity * deltaTime;
		deltaMove = Velocity * deltaTime;
		gameObject.transform.position -= deltaMove;
	}

	public void Slie()// 從站立變為滑行
	{
		Sliding = true;
		transform.rotation = Quaternion.Euler(0, 0, 90);
		transform.position -= new Vector3(0, 0.55f, 0);
	}

	public void Stand()// 從滑行變為站立
	{
		Sliding = false;
		transform.rotation = Quaternion.Euler(0, 0, 0);
		transform.position += new Vector3(0, 0.55f, 0);
	}

	public IEnumerator teleportCoolConut(float time)
	{
		TeleportCool = true;
		yield return new WaitForSeconds(time);
		TeleportCool = false;
	}

	public void GivePlayerDamage(int damage,float invincibleTime)
	{
		if (!Invincible) // 玩家沒有無敵時此函式才有作用
		{
			HP -= damage;
			StartCoroutine(InvincibleCoolConut(invincibleTime));
		}
	}

	private bool SpEnble = true;
	public IEnumerator InvincibleCoolConut(float time) // 這個函式time變數會/10
	{
		Invincible = true;
		time *= 10;

		for(int i = 0; i < time; i++)
		{
			if(SpEnble)
				SP.color = new Color32(255, 255, 255, 0);
			else
				SP.color = new Color32(255, 255, 255, 255);
			SpEnble = !SpEnble;
			yield return new WaitForSeconds(0.1f);
		}

		SpEnble = true;
		SP.color = new Color32(255, 255, 255, 255);

		Invincible = false;
	}

}
