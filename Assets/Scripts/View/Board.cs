﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using TicTacToe;

public class Board : MonoBehaviour, IPointerClickHandler
{
	#region Notifications
	public const string SquareClickedNotification = "Board.SquareClickedNotification";
	#endregion

	#region Fields
	[SerializeField] SetPooler xPooler;
	[SerializeField] SetPooler oPooler;
	#endregion

	#region Public
	public void Show(int index, Mark mark)
	{
		var pooler = mark == Mark.X ? xPooler : oPooler;
		var instance = pooler.Dequeue().gameObject;

		int x = index % 3;
		int z = index / 3;

		instance.transform.localPosition = new Vector3(x + 0.5f, 0, z + 0.5f );
		instance.SetActive(true);
	}

	public void Clear()
	{
		xPooler.EnqueueAll();
		oPooler.EnqueueAll();
	}
    #endregion

    #region Event Handlers
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        var pos = eventData.pointerCurrentRaycast.worldPosition;
        var x = Mathf.FloorToInt(pos.x);
        var z = Mathf.FloorToInt(pos.z);

        if (x < 0 || z < 0 || x > 2 || z > 2)
            return;

        var index = z * 3 + x;
        this.PostNotification(SquareClickedNotification, index);
    }
    #endregion
}