using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe;

public class EndGameState : BaseGameStates {

    public override void Enter()
    {
        base.Enter();
        if (Game.winner == Mark.None)
        {
            GameStateLabel.text = "Tie Game!";
        }
        else if (Game.winner == LocalPlayer.mark)
        {
            GameStateLabel.text = "You Win!";
            LocalPlayer.score++;
        }
        else
        {
            GameStateLabel.text = "You Lose!";
            RemotePlayer.score++;
        }
        RefreshPlayerLabels();
        if (!LocalPlayer.isServer)
            StartCoroutine(Restart());
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5);
        LocalPlayer.CmdCoinToss();
    }
}
