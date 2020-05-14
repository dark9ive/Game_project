using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCtrl : MonoBehaviour
{
    private bool x = false;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < 0.2f && !x)
        {
            Instantiate(gameObject, new Vector3(17.79f, 0, 5), Quaternion.identity, GameObject.FindGameObjectWithTag("Terrain").transform);
            x = true;
        }

        if (transform.position.x < -17.79f)
            Destroy(gameObject);
    }
}
