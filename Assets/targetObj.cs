using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetObj : MonoBehaviour
{


    // Start is called before the first frame update
    IEnumerator Start()
    {
        transform.localScale = new Vector2(0.5f, 0);

        for (int i = 0; i < 10; i++)
        {
            transform.localScale = new Vector2(0.5f, i / 10);

            yield return 1;
            yield return 1;
        }

        for (int i = 0; i < 5; i++)
        {
            transform.localScale = new Vector2(0.5f + 0.1f * i, 1);

            yield return 1;
            yield return 1;
        }

        yield return new WaitForSeconds(2);

        for (int i = 10; i > 2; i--)
        {
            transform.localScale = new Vector2(0.5f, i / 10);

            yield return 1;
            yield return 1;
        }

        Destroy(gameObject);
    }

}
