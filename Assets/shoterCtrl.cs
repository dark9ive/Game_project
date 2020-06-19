using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoterCtrl : MonoBehaviour
{
    public float speed = 0;

    public GameObject Bullet;
    private GameObject player;
    private Raytest playerInfo;

    void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
            return;

        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<Raytest>();
    }

    // Update is called once per frame
    /*
    void FixedUpdate()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(gameObject);
            return;
        }
            

        StartCoroutine(Shot());

        if (!(transform.position.y > player.transform.position.y - 0.15f && transform.position.y < player.transform.position.y + 0.15f))
        {
            // 若子機不在玩家旁
            if (transform.position.y < player.transform.position.y)
                speed += 0.01f;
            else
                speed -= 0.01f;

            transform.position += new Vector3(0, speed, 0);
        }
        else
        {
            speed = 0;
        }
    }
    */
    void Update(){
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            Destroy(gameObject);
            return;
        }
            

        StartCoroutine(Shot());

        if (!(transform.position.y > player.transform.position.y - 0.15f && transform.position.y < player.transform.position.y + 0.15f))
        {
            // 若子機不在玩家旁
            if (transform.position.y < player.transform.position.y)
                speed += 0.01f;
            else
                speed -= 0.01f;

            transform.position += new Vector3(0, speed, 0);
        }
        else
        {
            speed = 0;
        }

    }

    public bool shoting = false;
    public IEnumerator Shot() // 障礙物生成函數
    {
        if(!shoting)
        {
            shoting = true;

            if(playerInfo.Power >= 150)
            {
                Instantiate(Bullet,  new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);
                Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);
            }
            else
                Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);

            yield return new WaitForSeconds(0.1f);
            shoting = false;
        }
        
    }
}
