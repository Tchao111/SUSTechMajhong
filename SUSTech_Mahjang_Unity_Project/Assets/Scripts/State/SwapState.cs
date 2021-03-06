﻿using UnityEngine;
using System.Collections;

public class SwapState : State
{

    private bool choosed;
    public SwapState(Vector3 position) : base(position)
    {
        choosed = false;
    }

    public override void OnMouseDown(GameObject tile)
    {
        if (choosed)
        {
            choosed = false;
            GameObject.Find("HandTile").GetComponent<HandTile>().ChoosedTiles.Remove(tile.GetComponent<TileScript>().tile);
        }
        else
        {
            choosed = true;
            GameObject.Find("HandTile").GetComponent<HandTile>().ChoosedTiles.Add(tile.GetComponent<TileScript>().tile);
        }
    }
    public override void OnMouseEnter(GameObject tile)
    {
        if (!choosed) {
            upPosition = tile.transform.position + upDistance;
            downPosition = tile.transform.position;
            tile.transform.position = upPosition;
            GameObject.Find("Background Camera").GetComponent<AudioManager>().PlayTileEnterSound();
        }
            
    }
    public override void OnMouseExit(GameObject tile)
    {
        if (!choosed)
            tile.transform.position = downPosition;
    }
    

}
