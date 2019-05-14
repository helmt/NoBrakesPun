using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class LeaderBoardElement
{
    public string nickname;
    public int score;

    public LeaderBoardElement(string nickname, int score)
    {
        this.nickname = nickname;
        this.score = score;
    }
}
