using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameState : BaseGameStates {

    public override void Enter()
    {
        base.Enter();
        GameStateLabel.text = "Waiting for players.";
        LocalPlayerLabel.text = "";
        RemotePlayerLabel.text = "";
    }

    public override void Exit()
    {
        base.Exit();
        LocalPlayer.score = 0;
        RemotePlayer.score = 0;
        RefreshPlayerLabels();
    }
}
