using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController instace;

    //variáveis de controle de jogo
    [SerializeField] int money;
    bool isPaused;

    //variáveis para componentes de menu
    [Header("Menus Components")]
    [SerializeField] TextMeshProUGUI moneyTxt;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject mallScreen;
    

    private void Awake()
    {
        instace = this;
        // o jogo começará na tela de game
        SwitchScreen(0);
    }

    private void Start()
    {
        moneyTxt.text = money.ToString();
    }

    //método para ser atrelado em botão e definir o custo de compra
    public void BuyCapacity(int _value)
    {
        if (_value <= money)
        {
            money -= _value;
            moneyTxt.text = money.ToString();
            PlayerController.instance.IncrementMaxNpcsInBack(_value/10);
        }
    }
   
    //método chamado quando se "vende" um npc nas costas do player e ganha dinheiro
    public void SellObjects()
    {
        money += 10;
        moneyTxt.text = money.ToString();
    }

    //método para pausar o jogo
    public void PauseUnpause()
    {
        if (isPaused)
        {
            isPaused = false;
            SwitchScreen(0);
        }
        else
        {
            isPaused = true;
            SwitchScreen(1);
            PlayerController.instance.rb.velocity = Vector3.zero;
        }
        PlayerController.instance.canMove = !isPaused;
    }

    //método utilizado para navegação de telas
    public void SwitchScreen(int _screenIndex)
    {
        //tela de jogo index 0
        gameScreen.SetActive(false);
        //tela de pause index 1
        pauseScreen.SetActive(false);
        //tela da loja index 2
        mallScreen.SetActive(false);

        switch (_screenIndex)
        {
            case 0:
                gameScreen.SetActive(true);
                break;
            case 1:
                pauseScreen.SetActive(true);
                break;
            case 2:
                mallScreen.SetActive(true);
                break;
        }
    }

    //método para fechar aplicativo
    public void QuitGame()
    {
        Application.Quit();
    }
}
