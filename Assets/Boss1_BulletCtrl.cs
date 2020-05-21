using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_BulletCtrl : MonoBehaviour
{
    public float speed;
    public float Ready;
    public int BulletID;

    public int Damage;
    public float InvinTime;


    public int mode; // 會隨機指定這個子彈為哪種飛行模式

    private Raytest player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Raytest>();

            player.GivePlayerDamage(Damage, InvinTime);
        }
    }

    void Start()
    {
        switch(BulletID)
        {
            case 1: // 影狼的白符子彈
                mode = Random.Range(1, 8); // 5種飛行模式
                StartCoroutine(WaitAndShot(2));
                break;
            case 2: // 帝帝的跳跳球
                StartCoroutine(JumpBall());
                break;
            case 3:
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Raytest>();
                StartCoroutine(shotToPos(player.transform.position));
                break;
        }

    }

    private Vector2 _pos;
    private Vector2 dir;
    private float z;
    private bool Moving; // 防呆用
    public IEnumerator shotToPos(Vector2 pos)
    {
        if (Moving)
        {
            Debug.Log("已在移動中");
            yield break;
        }

        Moving = true;
        z = transform.position.z;

        _pos = transform.position;

        dir = pos - _pos;

        for (; !(transform.position.x < -12);)
        {
            _pos += dir * speed * Time.deltaTime;
            gameObject.transform.position = new Vector3(_pos.x, _pos.y, z);

            yield return 1;
        }
        Moving = false;
        Destroy(gameObject);
    }

    public IEnumerator JumpBall()
    {
        float T = 0,X = 1000;

        for (; !(transform.position.x < -12);)
        {
            gameObject.transform.rotation = Quaternion.Euler(0f, 0f, T);
            T += 2f/Time.fixedDeltaTime*Time.deltaTime;

            X += Time.deltaTime*3f;
            if (X > (270 * Mathf.Deg2Rad))
            {
                X = (90 * Mathf.Deg2Rad);
            }
            transform.position -= new Vector3(speed/Time.fixedDeltaTime*Time.deltaTime, -Mathf.Sin(X) / 15/Time.fixedDeltaTime*Time.deltaTime, 0.0f);

            yield return 1;
        }

        Destroy(gameObject);
    }

    public IEnumerator WaitAndShot(float time)
    {
        yield return new WaitForSeconds(time);

        switch (BulletID)
        {
            case 1:
                switch (mode)
                {
                    case 1:
                        StartCoroutine(shotToPos(new Vector2(-6.3f, 3.2f)));
                        // 瞄準上平台,使用滑行閃避
                        break;
                    case 2:
                        StartCoroutine(shotToPos(new Vector2(-6.3f, 2.3f)));
                        // 瞄準上平台,使用跳躍閃避
                        break;
                    case 3:
                        StartCoroutine(shotToPos(new Vector2(-6.3f, -1.84f)));
                        // 瞄準下平台,使用跳躍閃避
                        break;
                    case 4:
                        StartCoroutine(shotToPos(new Vector2(-6.3f, -1.04f)));
                        // 瞄準下平台,使用跳躍閃避
                        break;
                    default:
                        StartCoroutine(shotToPos(new Vector2(-6.3f, Random.Range(-10, 10))));
                        break;
                }
                break;
        }
    }
}
