using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeninhaControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(DestroyGameObject());
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > 0.0f)
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}
