using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameState : BaseGameStates
{
    public override void Enter()
    {
        base.Enter();
        GameStateLabel.text = "Your turn!";
        RefreshPlayerLabels();
    }

    protected override void AddListeners()
    {
        base.AddListeners();
        this.AddObserver(OnBoardSquareClicked, Board.SquareClickedNotification);
    }

    protected override void RemoveListeners()
    {
        base.RemoveListeners();
        this.RemoveObserver(OnBoardSquareClicked, Board.SquareClickedNotification);
    }

    void OnBoardSquareClicked (object sender, object args)
    {
        LocalPlayer.CmdMarkSquare((int)args);
    }
}
