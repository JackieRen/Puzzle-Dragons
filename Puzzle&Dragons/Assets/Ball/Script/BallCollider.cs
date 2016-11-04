using UnityEngine;
using System.Collections;

public class BallCollider : MonoBehaviour {
    
    public int _ballId = 0;
    
	void Start () {
	   _ballId = this.transform.GetSiblingIndex();
	}
    
}
