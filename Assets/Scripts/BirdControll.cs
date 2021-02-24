using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdControll : MonoBehaviour
{

    private float _speed;
    private Rigidbody _rb;

    private GameManager _gameManager;

    [SerializeField]
    private GameObject _peninha;

    [SerializeField]
    private AudioClip _somFinal, _somHit, _somVoa, _somScore, _somPick;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        if(SceneManager.GetActiveScene().name == "FelpudoSceneAR")
        {
            _gameManager = Camera.main.GetComponent<GameManager>();
        }
        else
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        

        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.timeToRestartGame)
        {
            if (Input.anyKeyDown)
            {
                //if (!GameManager.isOver)
                //{
                if (GameManager.isStarted)
                {
                    voaBird();
                }
                else
                {
                    transform.localScale = new Vector3(0.71f, 0.71f, 0.71f);
                    transform.position = new Vector3(0.79f, 1.24f, -1.18f);
                    _rb.useGravity = true;
                    GameManager.isStarted = true;
                    GameManager.isOver = false;
                    _gameManager.scroolSpeed = -0.5f;
                    voaBird();
                }

                //}
            }
        }
    }

    // -- Executa automaticamente após o Update
    // -- Usado para acertar últimos ajustes 
    private void LateUpdate()
    {
        _speed = _rb.velocity.y;
        transform.rotation = Quaternion.Euler((_speed * -5), 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CANO" || other.gameObject.tag == "PISO")
        {
            Debug.Log("PERDEU!!!");
            _gameManager.gameOver();
            _audioSource.PlayOneShot(_somHit, 0.5f);
            _audioSource.PlayOneShot(_somFinal, 0.5f);
            _rb.velocity = new Vector3(0, 0, 0);
            _rb.AddForce(new Vector3(0, 200, -200), ForceMode.Force);
            GameManager.timeToRestartGame = false;
            StartCoroutine(waitForRestart());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "VAO")
        {
            Debug.Log("MARCOU PONTO!!!");
            GameManager.points += 1;
            _audioSource.PlayOneShot(_somScore, 0.5f);
        }
    }

    void voaBird()
    {
        _audioSource.PlayOneShot(_somVoa, 0.5f);
        //transform.position = new Vector3(0.24f, .15f - 0, 0.24f);
        // -- Lógica para o pássaro inclinar para cima quando sobe e para baixo quando desce
        _rb.velocity = new Vector3(0, 0, 0);
        _rb.AddForce(new Vector3(0, 200, 0), ForceMode.Force);
        criaPeninhas();
    }

    IEnumerator waitForRestart()
    {
        yield return new WaitForSeconds(4.0f);
        GameManager.timeToRestartGame = true;
    }

    public void criaPeninhas()
    {

        float dx, dy, dz, rx, ry, rz;

        for(int i = 0; i < 5; i++)
        {
            dx = Random.Range(-50, 50);
            dy = Random.Range(-50, 50);
            dz = Random.Range(-50, 50);
            rx = Random.Range(-180, 180);
            ry = Random.Range(-180, 180);
            rz = Random.Range(-180, 180);

            float randomEscala = Random.Range(0.35f, 0.55f);
            GameObject novaPeninha = Instantiate(_peninha, transform.position, Quaternion.Euler(0, 0, 0));
            novaPeninha.transform.localScale = new Vector3(randomEscala, randomEscala, randomEscala);
            Rigidbody rb = novaPeninha.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(dx, dy, dz), ForceMode.Force); // -- Empurra peninha
            rb.AddTorque(new Vector3(rx, ry, rz)); // -- Rotaciona peninha
        }
    }
}
