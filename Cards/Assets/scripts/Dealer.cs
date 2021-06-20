using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Dealer : MonoBehaviour
{
    public const int DealNum = 5;

    public List<Card> GameDeck = new List<Card>();

    public Image[] PlayerCards = new Image[5];
    public Image[] EnemyCards = new Image[5];

    public SpriteAtlas spriteAtlas;

    public GameResult GameResult;

    public Player Player;

    public CPUPlayer CPUPlayer;

    // Start is called before the first frame update

    private void Start()
    {
        GameDeck = Deck.ShuffleDeck(Deck.GetDeck());
    }

    public void CardDeal()
    {
        Player.PlayerHand.Clear();

        if (GameDeck.Count < DealNum)
        {
            GameDeck.Clear();
            GameDeck = Deck.ShuffleDeck(Deck.GetDeck());
        }

        for (int i = 0; i < DealNum; i++)
        {
            Player.PlayerHand.Add(Deck.GetCard(GameDeck));
        }
        CPUPlayer.CPUHand.Clear();
        if (GameDeck.Count < DealNum)
        {
            GameDeck.Clear();
            GameDeck = Deck.ShuffleDeck(Deck.GetDeck());
        }
        for (int i = 0; i < DealNum; i++)
        {
            CPUPlayer.CPUHand.Add(Deck.GetCard(GameDeck));
        }
    }


    public void CardChange(int[] changeNum)
    {
        for (int i = 0; i < changeNum.Length; i++)
        {
            if (changeNum[i] != -1)
            {
                Player.PlayerHand.RemoveAt(changeNum[i]);
                Player.PlayerHand.Insert(changeNum[i], Deck.GetCard(GameDeck));
            }
        }
    }

    public void CardView()
    {
        for (int i = 0; i < Player.PlayerHand.Count; i++)
        {
            PlayerCards[i].sprite = spriteAtlas.GetSprite
                ($"Card_{(int)Player.PlayerHand[i].CardSuit * 13 + Player.PlayerHand[i].CardNumber - 1}");
            Debug.Log($"Player:{Player.PlayerHand[i].CardSuit}:{Player.PlayerHand[i].CardNumber}");
        }
        for (int i = 0; i < CPUPlayer.CPUHand.Count; i++)
        {
            EnemyCards[i].sprite = spriteAtlas.GetSprite
                ($"Card_{(int)CPUPlayer.CPUHand[i].CardSuit * 13 + CPUPlayer.CPUHand[i].CardNumber - 1}");
            Debug.Log($"{CPUPlayer.CPUHand[i].CardSuit}:{CPUPlayer.CPUHand[i].CardNumber}");
        }
    }
    public void GameJudge()
    {
        Debug.Log($"Ž©•ª‚ÌŽè–ð‚Í{PokerHand.CardHand(Player.PlayerHand)}");
        Debug.Log($"‘ŠŽè‚ÌŽè–ð‚Í{PokerHand.CardHand(CPUPlayer.CPUHand)}");
        GameResult.GameResultTextView(PokerHand.CardHand(CPUPlayer.CPUHand) < PokerHand.CardHand(Player.PlayerHand));
    }
}





