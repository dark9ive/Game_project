using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCtrl : MonoBehaviour
{
    public int pointNum;// 若為1為power,2為point

    private Ray ray;
    private RaycastHit2D hit;

    private Raytest player;
    private GameCtrl GM;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Raytest>();
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameCtrl>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 碰撞處理
        ray = new Ray(transform.position, Vector3.up);
        hit = Physics2D.Raycast(ray.origin, ray.direction, 0.1f, -1);

        if(hit.collider)
        {
            if(hit.collider.tag == "Player")
            {
                switch (pointNum)
                {
                    case 1:
                        player.Power += 5;
                        break;

                    case 2:
                        // 加分
                        break;
                }

                Destroy(gameObject);
            }

            if (hit.collider.tag == "block")
            {
                Destroy(gameObject);
            }
        }



        if ((transform.position.x - player.transform.position.x < 2) && (transform.position.x - player.transform.position.x > -2))
        {
            // 點進入玩家x軸附近
            if ((transform.position.y - player.transform.position.y < 2) && (transform.position.y - player.transform.position.y > -2))
            {
                // 點進入玩家y軸附近
                ChaseObj("Player");
            }
            else
            {
                transform.position -= new Vector3(GM.TerrainSpeed, 0, 0);
            }
        }
        else
        {
            transform.position -= new Vector3(GM.TerrainSpeed, 0, 0);
        }
    }

    public float speed;
    private bool IsReady = false;
    private GameObject target;
    private Vector2 dir;
    private Vector2 pos;

    public void ChaseObj(string ObjTag)
    {
        if (!target)
        {
            try
            {
                IsReady = false;
                target = GameObject.FindGameObjectWithTag(ObjTag);
                dir = target.transform.position - gameObject.transform.position;
                dir = dir.normalized;
                IsReady = true;
            }
            catch
            {
                // 找不到物件
            }
        }
        if (IsReady)
        {
            if(player.Sliding)
            {
                dir = target.transform.position - gameObject.transform.position - new Vector3(0.4f, 0, 0);// 調整滑行時物品射線射不到玩家判定位置的現象
                dir = dir.normalized;

                pos = transform.position ;
                pos += dir * speed * Time.fixedDeltaTime;
                gameObject.transform.position = pos;
            }
            else
            {
                dir = target.transform.position - gameObject.transform.position;
                dir = dir.normalized;

                pos = transform.position;
                pos += dir * speed * Time.fixedDeltaTime;
                gameObject.transform.position = pos;
            }
            
        }
    }
}
