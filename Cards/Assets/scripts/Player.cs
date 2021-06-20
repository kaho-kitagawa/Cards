using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bet Bet;
    public List<Card> PlayerHand = new List<Card>();
    public int[] CardChanges = new int[5];

    public bool CardChange = false;

    private void Start()
    {
        ResetCardChanges();
    }

    public void CardChangeChoice(int changeNum)
    //Player‚ÍŒðŠ·‚µ‚½‚¢ŽèŽD‚ð‘I‚Ô
    {
        for (int i = 0; i < CardChanges.Length; i++)
        {
            if (CardChanges[i] == -1)
            {
                CardChanges[i] = changeNum;
                break;
            }
        }
    }

    public void CardChangeDone()
    {
        CardChange = true;
    }

    public void ResetCardChanges()
    {
        for (int i = 0; i < CardChanges.Length; i++)
        {
            CardChanges[i] = -1;
        }
    }

    public void PlayerChipBet(int BetNum)
    {
        Bet.ChipBet(BetNum, true);
    }
}