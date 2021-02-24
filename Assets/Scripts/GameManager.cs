using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float scroolSpeed;
    private float _velocidadeObjeto = -190.0f;
    private float _posicaoZInicialObjetos = 5.3f;
    private GameObject _objetoX;

    public Material materialPiso;
    public GameObject nodeRootCena, cerca, canos, arbusto, nuvem, pedras;

    public static bool isStarted = false;
    public static bool isOver = false;

    public static bool timeToRestartGame = true;

    public static int points = 0;

    [SerializeField]
    private AudioSource _somFundo;

    // Start is called before the first frame update
    void Start()
    {

        if (SceneManager.GetActiveScene().name == "FelpudoScene")
        {
            DefaultTrackableEventHandler.targetFound = true;
        }

        // -- Fator de tempo da Unity
        Time.timeScale = 1.0f;
        //scroolSpeed = -0.5f;
        scroolSpeed = 0.0f;
        InvokeRepeating("criaCerca", 1, 0.54f);
        InvokeRepeating("criaCano", 1, 4.5f);
        InvokeRepeating("criaArbustoNuvemPedras", 1, 1.4f);
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scroolSpeed;
        Debug.Log("scroolSpeed :" + scroolSpeed);
        // -- Vector2 é apenas altura e largura
        // -- Queremos atualizar apenas o X (largura), na velocidade do offset
        materialPiso.SetTextureOffset("_MainTex", new Vector2(offset, 0));

        if (isStarted && DefaultTrackableEventHandler.targetFound)
        {
            if (!_somFundo.isPlaying)
            {
                _somFundo.loop = true;
                _somFundo.volume = 2.0f;
                _somFundo.Play();
            }
        }
        else
        {
            _somFundo.Stop();
        }

    }

    void criaCerca()
    {

        if (isStarted && DefaultTrackableEventHandler.targetFound)
        {
            //Instantiate(_explodePrefabe, transform.position, Quaternion.identity);
            //GameObject novoObjeto = (GameObject)Instantiate(cerca);
            // -- Cria variável nova
            GameObject novoObjeto = Instantiate(cerca);
            // -- O pai será o nodeCena, como estão os outros elementos
            novoObjeto.transform.parent = nodeRootCena.transform;
            // -- Cria novo vetor para posição
            novoObjeto.transform.position = new Vector3(-0.75f, 0, _posicaoZInicialObjetos);
            // -- Cria nova rotação para corigir erro de importação do 3D Max
            novoObjeto.transform.rotation = Quaternion.Euler(-90, 0, 0);
            // -- Busca um rigitbody do objeto
            Rigidbody rb = novoObjeto.GetComponent<Rigidbody>();
            // -- Empurra o objeto nesse posição
            /*
             Force is applied continuously along the direction of the force vector. 
             Specifying the ForceMode mode allows the type of force to be changed to an Acceleration, Impulse or Velocity Change.
             Force can be applied only to an active Rigidbody. If a GameObject is inactive, AddForce has no effect.
             By default the Rigidbody's state is set to awake once a force is applied, unless the force is Vector3.zero.
             */
            rb.AddForce(new Vector3(0, 0, _velocidadeObjeto), ForceMode.Force);
            //novoObjeto.rigidbody.AddForce(new Vector3(0, 0, _velocidadeObjeto), ForceMode.Force);

        }
    }

    void criaCano()
    {
        if (isStarted && DefaultTrackableEventHandler.targetFound)
        {
            // -- Lógica para que a altura seja ajustada entre 0 e -.5
            float offSetCano = Random.Range(-1.5f, 0.0f);
            Debug.Log("offSetCano: " + offSetCano);
            GameObject novoObjeto = Instantiate(canos);
            novoObjeto.transform.parent = nodeRootCena.transform;
            novoObjeto.transform.position = new Vector3(0.24f, (3.0f + offSetCano), _posicaoZInicialObjetos);
            novoObjeto.transform.rotation = Quaternion.Euler(0, 0, 0);
            Rigidbody rb = novoObjeto.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, 0, _velocidadeObjeto), ForceMode.Force);
        }
    }

    void criaArbustoNuvemPedras()
    {
        if (isStarted && DefaultTrackableEventHandler.targetFound)
        {
            int sorteiaObjeto = (int)Random.Range(1, 4);
            float offSetNuvem = Random.Range(1.5f, 2.95f);
            float giroRandom = Random.Range(-180.0f, 180.0f);
            float giroNuvem = 0.0f;
            float posicaoX = 0.0f;

            if ((int)(Random.Range(1, 3) % 2) == 0)
            {
                posicaoX = -0.45f;
                giroNuvem = 0.0f;
            }
            else
            {
                posicaoX = 0.75f;
                giroNuvem = 180.0f;
            }

            switch (sorteiaObjeto)
            {
                case 1:
                    _objetoX = Instantiate(arbusto);
                    _objetoX.transform.parent = nodeRootCena.transform;
                    _objetoX.transform.position = new Vector3(posicaoX, 0, _posicaoZInicialObjetos);
                    _objetoX.transform.rotation = Quaternion.Euler(-90, giroRandom, 0);
                    break;
                case 2:
                    _objetoX = Instantiate(nuvem);
                    _objetoX.transform.parent = nodeRootCena.transform;
                    _objetoX.transform.position = new Vector3(posicaoX, offSetNuvem, _posicaoZInicialObjetos);
                    _objetoX.transform.rotation = Quaternion.Euler(-90, giroNuvem, 0);
                    break;
                case 3:
                    _objetoX = Instantiate(pedras);
                    _objetoX.transform.parent = nodeRootCena.transform;
                    _objetoX.transform.position = new Vector3((posicaoX + 0.02f), 0, _posicaoZInicialObjetos);
                    _objetoX.transform.rotation = Quaternion.Euler(-90, giroRandom, 0);
                    break;
                default: break;
            }

            Rigidbody rb = _objetoX.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(0, 0, _velocidadeObjeto), ForceMode.Force);
        }
    }

    public void gameOver()
    {
        if (!isOver)
        {
            isOver = true;
            isStarted = false;
            scroolSpeed = 0.0f;
            points = 0;
        }
    }

    public void paraObjetos()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("OBJETOS");
        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(0, 0, 0);
                Destroy(obj);
            }
        }
    }
}
