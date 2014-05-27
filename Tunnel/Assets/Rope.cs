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

	public float ropeSpeed = 1.0f;

	void Start() {

		CreateRopeSegment();

	}

	void CreateRopeSegment() {
		// Create a rope segment for the gnome
		
		GameObject segment = (GameObject)Instantiate(ropeSegmentPrefab,  this.transform.position, Quaternion.identity);
		
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
		ropeSegments.Insert(0, segment);
		
		// If this is the FIRST segment, it needs to be connected to the gnome
		
		if (ropeSegments.Count == 1) {
			// Connect the joint on the connected object to the segment
			DistanceJoint2D connectedObjectJoint = connectedObject.GetComponent<DistanceJoint2D>();
			
			connectedObjectJoint.connectedBody = segmentBody;
			connectedObjectJoint.distance = maxRopeSegmentLength;

			// Set this joint to already be at the max length
			segmentJoint.distance = maxRopeSegmentLength;
		} else {
			// we now need to connect the former top segment to this one
			GameObject nextSegment = ropeSegments[1];
			DistanceJoint2D nextSegmentJoint = nextSegment.GetComponent<DistanceJoint2D>();

			nextSegmentJoint.connectedBody = segmentBody;

			segmentJoint.distance = 0.0f;
		}

		// Connect the segment to the rope anchor (ie this object)
		segmentJoint.connectedBody = this.rigidbody2D;
		


	}


	void RemoveRopeSegment() {
		// if we only have one segment, abort
		if (ropeSegments.Count <= 1)
			return;

		GameObject topSegment = ropeSegments[0];
		GameObject nextSegment = ropeSegments[1];

		DistanceJoint2D nextSegmentJoint = nextSegment.GetComponent<DistanceJoint2D>();

		nextSegmentJoint.connectedBody = this.rigidbody2D;

		ropeSegments.RemoveAt(0);

		Destroy (topSegment);

	}

	void Update() {

		GameObject topSegment = ropeSegments[0];
		
		DistanceJoint2D topSegmentJoint = topSegment.GetComponent<DistanceJoint2D>();
		
		if (isIncreasing) {

			if (topSegmentJoint.distance >= maxRopeSegmentLength) {
				CreateRopeSegment();
			} else {
				topSegmentJoint.distance += ropeSpeed * Time.deltaTime;
			}

		}

		if (isDecreasing) {

			if (topSegmentJoint.distance <= 0.005f) {
				RemoveRopeSegment();
			} else {
				topSegmentJoint.distance -= ropeSpeed * Time.deltaTime;
			}

		}
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
