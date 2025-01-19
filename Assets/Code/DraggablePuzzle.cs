using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DraggablePuzzle : MonoBehaviour
{
    [HideInInspector] public PuzzleTile[] tiles;

    private Vector3 startPos;


    private void Awake()
    {
        tiles = GetComponentsInChildren<PuzzleTile>();
        startPos = transform.position;

    }
    
    public void CheckHit()
    {
        bool canPlace = true;

        foreach (PuzzleTile tile in tiles)
        {
            Collider2D hit = Physics2D.OverlapPoint(tile.transform.position, 1 << LayerMask.NameToLayer("Cell"));

            if (hit == null || hit.GetComponent<Cell>().isFilled == true)
            {
                canPlace = false;
                break;

            }






        }

        if (canPlace)
        {
            foreach (PuzzleTile tile in tiles)
            {


                Collider2D hit = Physics2D.OverlapPoint(tile.transform.position, 1 << LayerMask.NameToLayer("Cell"));

                if(hit != null || hit.GetComponent<Cell>().isFilled == false)
                {
                    tile.gameObject.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y, tile.startPos.z + 3);
                    tile.isBlocking = true;

                    hit.GetComponent<Cell>().isFilled = true;
                    LevelController.instance.filledCells++;
                }


                


            }
        }
        else
        {
            foreach (PuzzleTile tile in tiles)
            {


                if(!tile.isBlocking)
                {
                    transform.position = startPos;
                }





            }
        }

       

    }
}
