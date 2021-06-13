
using System.Collections.Generic;
using System.Linq;


public class PokerHand
{
    ///<summary>
    ///�|�[�J�[�̖�
    ///</summary>

    public enum Hand
    {
        None = -1, //����
        OnePair, //�����y�A
        TwoPair, //�c�[�y�A
        ThreeOfKind, //�X���[�J�[�h
        Straight, //�X�g���[�g
        Flush, //�t���b�V��
        FullHouse, //�t���n�E�X
        FourOfKind, //�t�H�[�J�[�h
        StraightFlush, //�X�g���[�g�t���b�V��
        RoyalFlush, //���C�����X�g���[�g�t���b�V��
    }

    public static Hand CardHand(List<Card> cards)
    {
        //�܂��������\�[�g���܂�(1�C2�C3�C4�C5��)
        cards.Sort((a, b) => a.CardNumber - b.CardNumber);

        var kinds = 0;
        var cardsElement = 0;

        foreach (var card in cards.ToLookup(s => s.CardNumber))
        {
            if (card.Count() > 1)
            {
                cardsElement++;
                kinds += card.Count();
            }
        }
        //�t���b�V���̊m�F�@Suit���S�ē�����
        var clubRoyal = cards.TrueForAll(s => s.CardSuit == Card.Suit.Club);
        var diaRoyal = cards.TrueForAll(s => s.CardSuit == Card.Suit.Dia);
        var heartRoyal = cards.TrueForAll(s => s.CardSuit == Card.Suit.Heart);
        var spadeRoyal = cards.TrueForAll(s => s.CardSuit == Card.Suit.Spade);

        var firstCardNo = cards[0].CardNumber;

        #region ���C�����X�g���[�g�t���b�V��(1�C10�C11�C12�C13)Suit���S�Ĉꏏ
        if (firstCardNo.Equals(1))
        {
            if (cards[1].CardNumber.Equals(10))
            {
                var count = 0;
                for (int i = 2; i < cards.Count; i++)
                {
                    if (cards[i].CardNumber.Equals(9 + i))
                    {
                        count++;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                //Straight�͊m��
                if (count.Equals(3))
                {
                    if (clubRoyal || diaRoyal || heartRoyal || spadeRoyal)
                    {
                        return Hand.RoyalFlush;
                    }
                    else
                    {
                        return Hand.Straight;
                    }
                }
            }
        }
        #endregion

        #region �X�g���[�g�t���b�V��
        var straightCount = 0;
        for (int i = 1; i < cards.Count; i++)
        {
            var straightCardNo = firstCardNo;
            if (cards[i].CardNumber.Equals(straightCardNo + i))
            {
                straightCount++;
                continue;
            }
            else
            {
                break;
            }
        }
        if (straightCount.Equals(4))
        {

            //���bool�̂����ꂩ��true�������ꍇ��StraightFlush
            if (clubRoyal || diaRoyal || heartRoyal || spadeRoyal)
            {
                return Hand.StraightFlush;
            }
            else
            {
                return Hand.Straight;
            }
        }
        #endregion

        #region�@�t�H�[�J�[�h
        if (cardsElement.Equals(2) && kinds.Equals(4))
        {
            return Hand.FourOfKind;
        }
        #endregion

        #region �t���n�E�X
        if (cardsElement.Equals(2) && kinds.Equals(5))
        {
            return Hand.FullHouse;
        }
        #endregion

        #region �t���b�V��
        if (clubRoyal || diaRoyal || heartRoyal || spadeRoyal)
        {
            return Hand.Flush;
        }
        #endregion

        #region �����y�A���c�[�y�A���X���[�J�[�h
        if (kinds.Equals(3))
        {
            return Hand.ThreeOfKind;
        }
        if (cardsElement.Equals(2) && kinds.Equals(4))
        {
            return Hand.TwoPair;
        }
        if (kinds.Equals(2))
        {
            return Hand.OnePair;
}
        #endregion
        return Hand.None;
    }
}
