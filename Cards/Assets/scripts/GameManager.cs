using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerGameManager : MonoBehaviour
{
    public enum GameState
    {
        Init = 0,
        Start,
        Deal,
        Bet,
        CardChange,
        ShowDown,
        Result
    }

    public GameState gameState = GameState.Init;

    public Dealer Dealer;
    public Player Player;
    public CPUPlayer CPUPlayer;
    public Chip Chip;

    //Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Init:
                //初期化するものがあれば初期化してStateへ
                gameState = GameState.Start;
                break;
            case GameState.Start:
                //STARTの時にしたいことを書く、終わればDealへ
                gameState = GameState.Deal;
                break;
            case GameState.Deal:
                //DealerがPlayerとCPUPlayerにカードを配る
                Dealer.CardDeal();
                Dealer.CardView();
                gameState = GameState.Bet;
                break;
            case GameState.Bet:
                //PlayerとCPUPlayerがチップの枚数を宣言する
                Player.PlayerChipBet(1);
                CPUPlayer.CPUChipBet(1);
                gameState = GameState.CardChange;
                break;
            case GameState.CardChange:
                //Playerが捨てた枚数分、再びPlayerに手札を配る
                if (Player.CardChange)
                {
                    Dealer.CardChange(Player.CardChanges);
                    Dealer.CardView();
                    gameState = GameState.ShowDown;
                }
                break;
            case GameState.ShowDown:
                //PlayerとCPUPlayerの手札をオープン・役を比較する
                Dealer.GameJudge();
                gameState = GameState.Result;
                break;
            case GameState.Result:
                break;
        }
    }
}