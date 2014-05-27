using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rope : MonoBehaviour {

	List<GameObject> ropeSegments = new List<GameObject>();

	bool isIncreasing = false;
	bool isDecreasing = false;

	// the gnome
	public Rigidbody2D connectedObject;

	public float maxRopeSegmentLength = 1.0f;

	public GameObject ropeSegmentPrefab;

	void Start() {

		CreateRopeSegment();

	}

	void CreateRopeSegment() {
		// Create a rope segment for the gnome
		
		GameObject segment = (GameObject)Instantiate(ropeSegmentPrefab, connectedObject.transform.position, Quaternion.identity);
		
		// Get the rigidbody from the segment
		Rigidbody2D segmentBody = segment.GetComponent<Rigidbody2D>();
		
		// Get the distance joint from the segment
		DistanceJoint2D segmentJoint = segment.GetComponent<DistanceJoint2D>();
		
		// Error if it doesn't have a rigidbody or joint
		if (segmentBody == null || segmentJoint == null) {
			Debug.LogError("Rope segment body prefab has no Rigidbody2D and/or joint!");
			return;
		}
		
		// now that it's checked, add it to the list of rope segments
		ropeSegments.Add(segment);
		
		// If this is the FIRST segment, it needs to be connected to the gnome
		
		if (ropeSegments.Count == 1) {
			// Connect the joint on the connected object to the segment
			DistanceJoint2D connectedObjectJoint = connectedObject.GetComponent<DistanceJoint2D>();
			
			connectedObjectJoint.connectedBody = segmentBody;
		}
		
		// Connect the segment to the rope anchor (ie this object)
		segmentJoint.connectedBody = this.rigidbody2D;
		
		// FIXME: temporarily using default segment length
		segmentJoint.distance = maxRopeSegmentLength;
	}

	public void StartIncreasingLength() {
		isIncreasing = true;
		Debug.Log("Starting increasing length");
	}

	public void StopIncreasingLength() {
		isIncreasing = false;
		Debug.Log("Stopping increasing length");
	}

	public void StartDecreasingLength() {
		isDecreasing = true;
		Debug.Log("Starting decreasing length");
	}

	public void StopDecreasingLength() {
		isDecreasing = false;
		Debug.Log("Stopping decreasing length");

	}




}
