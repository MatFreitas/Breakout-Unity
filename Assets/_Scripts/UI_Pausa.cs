using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pausa : MonoBehaviour
{
    GameManager gm;
    public Text message_retomar;
    public Text message_inicio;

    private void OnEnable() 
    {
        gm = GameManager.GetInstance();
        message_retomar.text = "Retomar";
        message_inicio.text = "Início";
    }

    public void Retornar() 
    {
        gm.ChangeState(GameManager.GameState.GAME);
    }

    public void Inicio()
    {
        gm.ChangeState(GameManager.GameState.MENU);
    }
}
