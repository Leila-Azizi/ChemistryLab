﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    private bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;
	public GameObject []options;
	//public GameObject option2;
    
	void Update ()
	{
	    if (HasInput)
	    {
	        DragOrPickUp();
	    }
	    else
	    {
	        if (draggingItem)
	            DropItem();
	    }
	}
    Vector2 CurrentTouchPosition
    {
        get
        {
           return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;
        if (draggingItem)
        {
            draggedObject.transform.position = inputPosition + touchOffset;
        }
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null)
                {
                    draggingItem = true;
					if (hit.transform.gameObject.tag != "Image") {
						draggedObject = hit.transform.gameObject;
						touchOffset = (Vector2)hit.transform.position - inputPosition;
						draggedObject.transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
					}
					foreach (Transform child in options[GameCtrl.index].transform) {
					child.gameObject.GetComponent<CircularMovement> ().enabled = false;
				}
			

                }
            }
        }
    }
    private bool HasInput
    {
        get
        {
            // returns true if either the mouse button is down or at least one touch is felt on the screen
            return Input.GetMouseButton(0);
        }
    }
    void DropItem()
    {
        draggingItem = false;
        draggedObject.transform.localScale = new Vector3(1, 1, 1);
    }
}
