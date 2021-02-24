using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControll : MonoBehaviour
{

    public Texture2D texturaFundo, texturaLogo, texturaFelpudo;

    private int _larguraLogo = 200;
    private int _alturaLogo = 120;

    private int _larguraFelpudo = 280;
    private int _alturaFelpudo = 250;

    private int _larguraBotao = 240;
    private int _alturaBotao = 60;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        // -- new Rect(0, 0, Screen.width, Screen.height) --> Criar um frame retangular na posição zero e do tamanho adaptado à tela
        // -- ScaleMode.StretchToFill --> Ocupar a tela inteira
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texturaFundo, ScaleMode.StretchToFill);
        //GUI.DrawTexture(new Rect(Screen.width - _larguraLogo - 10, 10, _larguraLogo, _alturaLogo), texturaLogo, ScaleMode.StretchToFill);
        //GUI.DrawTexture(new Rect(10, Screen.height/2 -_alturaFelpudo/2, _larguraFelpudo, _alturaFelpudo), texturaFelpudo, ScaleMode.StretchToFill);

        if (GUI.Button(new Rect(Screen.width / 2 - _larguraBotao / 2, Screen.height / 2 + _alturaBotao + 45, _larguraBotao, _alturaBotao), "Jogar SEM Câmera"))
        {
            SceneManager.LoadScene("FelpudoScene");
        }

        if (GUI.Button(new Rect(Screen.width / 2 - _larguraBotao / 2, Screen.height / 2 + _alturaBotao - 45, _larguraBotao, _alturaBotao), "Jogar COM Câmera"))
        {
            SceneManager.LoadScene("FelpudoSceneAR");
        }
    }
}
