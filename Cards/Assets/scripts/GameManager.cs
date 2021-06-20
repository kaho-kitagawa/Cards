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
                //������������̂�����Ώ���������State��
                gameState = GameState.Start;
                break;
            case GameState.Start:
                //START�̎��ɂ��������Ƃ������A�I����Deal��
                gameState = GameState.Deal;
                break;
            case GameState.Deal:
                //Dealer��Player��CPUPlayer�ɃJ�[�h��z��
                Dealer.CardDeal();
                Dealer.CardView();
                gameState = GameState.Bet;
                break;
            case GameState.Bet:
                //Player��CPUPlayer���`�b�v�̖�����錾����
                Player.PlayerChipBet(1);
                CPUPlayer.CPUChipBet(1);
                gameState = GameState.CardChange;
                break;
            case GameState.CardChange:
                //Player���̂Ă��������A�Ă�Player�Ɏ�D��z��
                if (Player.CardChange)
                {
                    Dealer.CardChange(Player.CardChanges);
                    Dealer.CardView();
                    gameState = GameState.ShowDown;
                }
                break;
            case GameState.ShowDown:
                //Player��CPUPlayer�̎�D���I�[�v���E�����r����
                Dealer.GameJudge();
                gameState = GameState.Result;
                break;
            case GameState.Result:
                break;
        }
    }
}