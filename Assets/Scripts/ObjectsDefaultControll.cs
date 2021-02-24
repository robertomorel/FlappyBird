using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDefaultControll : MonoBehaviour
{

    private bool _cresce = false;
    private bool _diminui = false;
    private Vector3 _vetorEscalaObj = new Vector3(0.035f, 0.035f, 0.035f);

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        _cresce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -5.2f)
        {
            Destroy(this.gameObject);
        }
        if ((transform.localScale.x < 1.0f) && _cresce)
        {
            transform.localScale += _vetorEscalaObj;
        }
        else
        {
            _cresce = false;
        }

        if (transform.position.z < -3.8f)
        {
            _diminui = true;
        }

        if (_diminui)
        {
            if (this.transform.localScale.x > 0.0f)
            {
                transform.localScale -= _vetorEscalaObj;
            }
            else
            {
                _diminui = true;
            }
        }
    }
}