using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl_1 : MonoBehaviour
{
    public Ray ray;
    public float rayLong;
    public RaycastHit2D rayHit;

    private GameObject obj;

    // ================尋找目標用============== //
    public float speed;
    private bool IsReady = false;
    public GameObject target;
    public Vector2 dir;
    public Vector2 pos;

    public int power;
    public int damage;
    public Raytest player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Raytest>();
        power = player.Power;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region 射線處理
        if (gameObject.transform.position.x > 11)
        {
            Destroy(gameObject);
        }

        Debug.DrawRay(transform.position, Vector3.right * rayLong);

        ray = new Ray(transform.position, Vector3.right * rayLong);

        rayHit = Physics2D.Raycast(ray.origin, ray.direction, rayLong, -1);

        if (rayHit.collider)
        {
            if(rayHit.collider.tag == "Enemy")
            {
                Destroy(gameObject);
                rayHit.collider.GetComponent<EnemyClass>().HP -= damage;
            }
        }
        #endregion
        
        if(!target)
        {
            try
            {
                target = player.target;
                if(target.transform.position.x < 10)
                {
                    // 目標存在
                    IsReady = false;
                    dir = target.transform.position - gameObject.transform.position;
                    dir = dir.normalized;
                    IsReady = true;
                }
                else
                {
                    gameObject.transform.position += new Vector3(0.5f, 0, 0);
                }
                /*if ((GameObject.FindGameObjectWithTag("Enemy").transform.position.x < 10))
                {
                    IsReady = false;
                    target = GameObject.FindGameObjectWithTag("Enemy");
                    dir = target.transform.position - gameObject.transform.position;
                    dir = dir.normalized;
                    IsReady = true;
                }
                else
                    gameObject.transform.position += new Vector3(0.5f, 0, 0);*/
            }
            catch
            {
                gameObject.transform.position += new Vector3(0.5f, 0, 0);
            }
        }
        if(IsReady && target)
        {
            dir = target.transform.position - gameObject.transform.position;
            dir = dir.normalized;

            pos = transform.position;
            pos += dir * speed * Time.fixedDeltaTime;
            gameObject.transform.position = pos;
        }

        if (player.Power >= 10)
            damage = 5;
        else if (player.Power >= 30)
            damage = 10;
        else if (player.Power >= 70)
            damage = 20;
        else if (player.Power >= 120)
            damage = 40;
        else if (player.Power >= 150)
            damage = 50;
        else
            damage = 5;
    }
}
