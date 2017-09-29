﻿using UnityEngine;
using System.Collections;
using TicTacToe;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : StateMachine 
{
	#region Fields
	public Game game = new Game();
	public Board board;
    public Text localPlayerLabel;
    public Text remotePlayerLabel;
    public Text gameStateLabel;
    public MatchController matchController;
    #endregion

    #region MonoBehaviour
    void OnEnable()
	{
        this.AddObserver(OnMatchReady, MatchController.MatchReady);
        this.AddObserver(OnDidBeginGame, Game.DidBeginGameNotification);
        this.AddObserver(OnDidMarkSquare, Game.DidMarkSquareNotification);
        this.AddObserver(OnDidChangeControl, Game.DidChangeControlNotification);
        this.AddObserver(OnDidEndGame, Game.DidEndGameNotification);
        this.AddObserver(OnCoinToss, PlayerController.CoinToss);
        this.AddObserver(OnRequestMarkSquare, PlayerController.RequestMarkSquare);
    }

	void OnDisable()
	{
        this.RemoveObserver(OnMatchReady, MatchController.MatchReady);
        this.RemoveObserver(OnDidBeginGame, Game.DidBeginGameNotification);
        this.RemoveObserver(OnDidMarkSquare, Game.DidMarkSquareNotification);
        this.RemoveObserver(OnDidChangeControl, Game.DidChangeControlNotification);
        this.RemoveObserver(OnDidEndGame, Game.DidEndGameNotification);
        this.RemoveObserver(OnCoinToss, PlayerController.CoinToss);
        this.RemoveObserver(OnRequestMarkSquare, PlayerController.RequestMarkSquare);
    }

    void Awake()
    {
        CheckState();
    }
    #endregion

    #region Event Handlers
    void OnBoardSquareClicked(object sender, object args)
    {
        if (game.control == TicTacToe.Mark.None)
            game.Reset();
        else
            game.Place((int)args);
    }

    void OnDidBeginGame(object sender, object args)
    {
        board.Clear();
        CheckState();
    }

    void OnDidMarkSquare(object sender, object args)
    {
        var index = (int)args;
        var mark = game.board[index];
        board.Show(index, mark);
    }

    void OnMatchReady(object sender, object args)
    {
        if (matchController.clientPlayer.isLocalPlayer)
            matchController.clientPlayer.CmdCoinToss();
    }

    void OnCoinToss(object sender, object args)
    {
        bool coinToss = (bool)args;
        matchController.hostPlayer.mark = coinToss ? TicTacToe.Mark.X : TicTacToe.Mark.O;
        matchController.clientPlayer.mark = coinToss ? TicTacToe.Mark.O : TicTacToe.Mark.X;
        game.Reset();
    }

    void OnRequestMarkSquare(object sender, object args)
    {
        game.Place((int)args);
    }

    void OnDidChangeControl(object sender, object args)
    {
        CheckState();
    }
    void OnDidEndGame(object sender, object args)
    {
        CheckState();
    }

    void CheckState()
    {
        if (!matchController.IsReady)
            ChangeState<LoadGameState>();
        else if (game.control == TicTacToe.Mark.None)
            ChangeState<EndGameState>();
        else if (game.control == matchController.localPlayer.mark)
            ChangeState<ActiveGameState>();
        else
            ChangeState<PassiveGameState>();
    }

    #endregion
}
