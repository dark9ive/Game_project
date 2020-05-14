using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyCtrl2 : EnemyClass
{
    public float destoryTime;
    public GameObject Bullet;

    private float time;

    // Start is called before the first frame update
    void Start()
    {
        GetPointObj();
    }

    private bool shoted = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        gameObject.transform.position -= new Vector3(Mathf.Cos(time) / 8, 0, 0);

        DropPoint();

        if(HP <= 0)
        {
            Dead();
        }

        if (Mathf.Cos(time) < 0.1f)
        {
            if(!shoted)
            {
                Instantiate(Bullet, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("GM").transform);
                shoted = true;
            }

        }

        if (time > destoryTime)
            Destroy(gameObject);
    }
}
