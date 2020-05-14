using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : EnemyClass
{
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetPointObj();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        gameObject.transform.position += new Vector3(0, Mathf.Sin(time) / 18, 0);

        DropPoint();

        if (HP <= 0)
            Dead();
    }
}
