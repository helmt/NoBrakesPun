using System;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class LeaderBoard : MonoBehaviourPun, IPunObservable
{
    private Photon.Realtime.Player[] _players;
    private LeaderBoardElement[] scoreboard;
    private int playerCount;

    public List<TextMeshProUGUI> nicknames;
    public List<TextMeshProUGUI> scores;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){}
    
    private void Start()
    {
        _players = PhotonNetwork.PlayerList;
        playerCount = _players.Length;
        scoreboard = new LeaderBoardElement[playerCount];
        int i = 0;
        foreach (Photon.Realtime.Player player in _players)
            scoreboard[i++] = new LeaderBoardElement(player.NickName, 0);
        UpdateDisplay();
    }

    private int FindIndex(string nickname)
    {
        for (int i = 0; i < playerCount; i++)
            if (scoreboard[i].nickname == nickname) return i;
        return -1;
    }

    [PunRPC]
    public void RankingUpdate(string nick, int score)
    {
        int i = FindIndex(nick);
        scoreboard[i].score = score;
        while (i > 0 && scoreboard[i].score > scoreboard[i - 1].score)
        {
            LeaderBoardElement tmp = scoreboard[i];
            scoreboard[i] = scoreboard[i - 1];
            scoreboard[i - 1] = tmp;
            i--;
        }

        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < playerCount; i++)
        {
            nicknames[i].text = scoreboard[i].nickname;
            scores[i].text = scoreboard[i].score + " $";
        }
    }
}
