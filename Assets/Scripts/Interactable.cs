using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public float tileSize = 1f;

	void Start () 
    {
		
	}

    public bool InPoint(Vector3 worldPos)
    {
        return // Check x and y are close enough
            (Mathf.Abs(transform.position.x - worldPos.x) < tileSize/2 &&
            (Mathf.Abs(transform.position.y - worldPos.y) < tileSize/2));
    }

    public virtual void Interact()
    {
        
    }
}
