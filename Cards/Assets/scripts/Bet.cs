using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bet : MonoBehaviour
{
    public Chip Chip;
    public bool CheckBet(int betNum, bool isPlayer)
    {
        if (isPlayer)
        {
            return Chip.PlayerChip > betNum;
        }
        else
        {
            return Chip.CPUChip > betNum;
        }
    }

    public void ChipBet(int betNum, bool isPlayer)
    {
        if (!CheckBet(betNum, isPlayer))
        {
            Debug.LogError("bet‚·‚é‚±‚Æ‚ª‚Å‚«‚Ü‚¹‚ñ");
            return;
        }

        if (isPlayer)
        {
            Chip.PlayerChip -= betNum;
        }
        else
        {
            Chip.CPUChip -= betNum;
        }
        Chip.GameChip += betNum;
    }
}