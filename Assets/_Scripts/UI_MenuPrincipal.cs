using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MenuPrincipal : MonoBehaviour
{
    GameManager gm;
    public Text message;
    
    private void OnEnable() 
    {
        gm = GameManager.GetInstance();
        message.text = "Começar";
    }

    public void Comecar() 
    {
        gm.ChangeState(GameManager.GameState.GAME);
    }
}
