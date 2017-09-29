using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveGameState : BaseGameStates
{
    public override void Enter()
    {
        base.Enter();
        GameStateLabel.text = "Opponent's Turn!";
        RefreshPlayerLabels();
    }

}
