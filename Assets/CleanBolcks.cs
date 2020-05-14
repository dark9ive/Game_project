using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanBolcks : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
            Destroy(col.gameObject);

        if (col.tag == "Container")
            Destroy(col.gameObject);

        if (col.tag == "Point")
            Destroy(col.gameObject);
    }

}
