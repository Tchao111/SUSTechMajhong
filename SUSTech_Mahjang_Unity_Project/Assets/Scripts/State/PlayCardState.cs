﻿using UnityEngine;
using System.Collections;

public class PlayCardState : State
{

    public PlayCardState(Vector3 position) : base(position)
    {

    }

    public override void OnMouseDown(GameObject tile)
    {
        tile.GetComponentInParent<HandTile>().RemoveSingleTile(tile.GetComponent<TileScript>().tile);
    }
    public override void OnMouseEnter(GameObject tile)
    {
        upPosition = tile.transform.position + upDistance;
        downPosition = tile.transform.position;
        tile.transform.position = upPosition;
    }
    public override void OnMouseExit(GameObject tile)
    {
        tile.transform.position = downPosition;
    }

    public override void Lightup(GameObject tile)
    {
        tile.GetComponentInParent<Lightuptile>().SendMessage("lightup", 1);
    }
}
