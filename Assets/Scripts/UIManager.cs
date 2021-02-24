using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _inicialText, _pontos;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "FelpudoSceneAR")
        {
            _gameManager = Camera.main.GetComponent<GameManager>();
        }
        else
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        _inicialText.text = "Toque para Iniciar";
        _pontos.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isStarted)
        {
            _inicialText.enabled = false;
        }
        else
        {
            _inicialText.enabled = true;
            //StartCoroutine(waitForShowText());
        }

        _pontos.text = GameManager.points.ToString();
    }

    IEnumerator waitForShowText()
    {
        yield return new WaitForSeconds(3.0f);
        _inicialText.enabled = true;
    }

    public void BackToMainMenu()
    {
        GameManager.isStarted = false;
        GameManager.isOver = false;
        GameManager.timeToRestartGame = true;
        GameManager.points = 0;
        SceneManager.LoadScene("MainScene");
    }

}