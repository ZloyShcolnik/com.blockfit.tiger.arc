using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public Vector3 startPos;

    private DraggablePuzzle puzzleParent;
    private Collider2D objectCollider;

    private bool canMove;
    private float xOffset;
    private float yOffset;

    private void Awake()
    {
        puzzleParent = transform.parent.gameObject.GetComponent<DraggablePuzzle>();
        objectCollider = GetComponent<Collider2D>();
        startPos = transform.position;
    }

    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (objectCollider == Physics2D.OverlapPoint(mousePos, 1 << LayerMask.NameToLayer("PuzzleTile")))
            {
                LevelController.instance.audioSource.PlayOneShot(LevelController.instance.pickUpSound);

                if(isBlocking)
                {
                    foreach (PuzzleTile tile in puzzleParent.tiles)
                    {
                        Collider2D hit = Physics2D.OverlapPoint(tile.transform.position, 1 << LayerMask.NameToLayer("Cell"));

                        if(hit != null)
                        {
                            hit.GetComponent<Cell>().isFilled = false;
                            LevelController.instance.filledCells--;
                            tile.isBlocking = false;
                            tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y, tile.startPos.z);
                
                        }
                        


                    }
                }

                canMove = true;


            }
            else
            {
                canMove = false;
            }



        }
        if (canMove)
        {
            puzzleParent.transform.position = new Vector3(mousePos.x, mousePos.y, puzzleParent.transform.position.z);
        }
        if (Input.GetMouseButtonUp(0))
        {
            
            canMove = false;
            puzzleParent.CheckHit();
            LevelController.instance.audioSource.PlayOneShot(LevelController.instance.dropSound);

        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    if (objectCollider == Physics2D.OverlapPoint(touchPos))
                    {
                        LevelController.instance.audioSource.PlayOneShot(LevelController.instance.pickUpSound);
                        xOffset = touchPos.x - puzzleParent.transform.position.x;
                        yOffset = touchPos.y - puzzleParent.transform.position.y;

                        canMove = true;

                    }
                    break;

                case TouchPhase.Moved:
                    if (objectCollider == Physics2D.OverlapPoint(touchPos) || canMove)
                    {
                        puzzleParent.transform.position = new Vector2(touchPos.x - xOffset, touchPos.y - yOffset);
                    }
                    break;
                case TouchPhase.Ended:
                    
                    canMove = false;
                    puzzleParent.CheckHit();
                    LevelController.instance.audioSource.PlayOneShot(LevelController.instance.dropSound);
                    break;
            }
        }


    }
}
