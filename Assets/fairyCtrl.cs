using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fairyCtrl : EnemyClass
{
    public bool upSideDown;
    private float time;

    void Start()
    {
        GetPointObj();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime * 1.5f;

        if(upSideDown)
            gameObject.transform.position -= new Vector3(speed, Mathf.Cos(time) / 28, 0);
        else
            gameObject.transform.position -= new Vector3(speed, Mathf.Sin(time) / 28, 0);

        DropPoint();

        if (HP <= 0)
        {
            Dead();
        }
            
    }
}
