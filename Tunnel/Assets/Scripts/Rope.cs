using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The connected rope.
public class Rope : MonoBehaviour {

	// The Rope Segment prefab to use.
	public GameObject ropeSegmentPrefab;
    
	// Contains a list of Rope Segment objects.
	List<GameObject> ropeSegments = new List<GameObject>();

	// Are we currently extending or retracting the rope?
	public bool isIncreasing { get; set; }
	public bool isDecreasing { get; set; }

	// The rigidbody object that the end of the rope should be attached to.
	public Rigidbody2D connectedObject;

	// The maximium length a rope segment should be (if we need to extend by more than this,
	// create a new rope segment.)
	public float maxRopeSegmentLength = 1.0f;

	// How quickly we should pay out new rope.
	public float ropeSpeed = 1.0f;

	// The LineRenderer that renders the actual rope.
	LineRenderer lineRenderer;

	void Start() {

		// Cache the line renderer, so we don't have to look it up every frame
		lineRenderer = GetComponent<LineRenderer>();

		// Reset the rope, so that we're ready to go.
		ResetLength();

	}

	// Remove all rope segments, and create a new one.
	public void ResetLength() {

		foreach (GameObject segment in ropeSegments) {
			Destroy (segment);

		}

		ropeSegments = new List<GameObject>();

		isDecreasing = false;
		isIncreasing = false;

		CreateRopeSegment();

	}

	// Attaches a new rope segment at the top of the rope.
	void CreateRopeSegment() {

		// Create the new rope segment.
		GameObject segment = (GameObject)Instantiate(ropeSegmentPrefab,  this.transform.position, Quaternion.identity);
		
		// Get the rigidbody from the segment
		Rigidbody2D segmentBody = segment.GetComponent<Rigidbody2D>();
		
		// Get the distance joint from the segment
		SpringJoint2D segmentJoint = segment.GetComponent<SpringJoint2D>();
		
		// Error if the segment prefab doesn't have a rigidbody or spring joint - we need both
		if (segmentBody == null || segmentJoint == null) {
			Debug.LogError("Rope segment body prefab has no Rigidbody2D and/or SpringJoint2D!");
			return;
		}
		
		// Now that it's checked, add it to the start of the list of rope segments
		ropeSegments.Insert(0, segment);
		
		// If this is the FIRST segment, it needs to be connected to the gnome
		
		if (ropeSegments.Count == 1) {
			// Connect the joint on the connected object to the segment
			SpringJoint2D connectedObjectJoint = connectedObject.GetComponent<SpringJoint2D>();
			
			connectedObjectJoint.connectedBody = segmentBody;
			connectedObjectJoint.distance = 0.1f;

			// Set this joint to already be at the max length
			segmentJoint.distance = maxRopeSegmentLength;
		} else {
			// This is an additional rope segment. We now need to connect the previous top 
			// segment to this one

			// Get the second segment
			GameObject nextSegment = ropeSegments[1];

			// Get the joint that we need to attach to
			SpringJoint2D nextSegmentJoint = nextSegment.GetComponent<SpringJoint2D>();

			// Make this joint connect to us
			nextSegmentJoint.connectedBody = segmentBody;

			// Make this segment start at a distance of 0 units away from the previous one - 
			// it will be extended.
			segmentJoint.distance = 0.0f;
		}

		// Connect the new segment to the rope anchor (i.e. this object)
		segmentJoint.connectedBody = this.GetComponent<Rigidbody2D>();
		


	}

	// Called when we've shrunk the rope, and we need to remove a segment.
	void RemoveRopeSegment() {

		// If we only have one segment, stop.
		if (ropeSegments.Count <= 1) {
			return;
		}

		// Get the top segment, and the segment under it.
		GameObject topSegment = ropeSegments[0];
		GameObject nextSegment = ropeSegments[1];

		// Connect the second segment to the rope's anchor.
		SpringJoint2D nextSegmentJoint = nextSegment.GetComponent<SpringJoint2D>();
		nextSegmentJoint.connectedBody = this.GetComponent<Rigidbody2D>();

		// Remove the top segment and destroy it.
		ropeSegments.RemoveAt(0);
		Destroy (topSegment);

	}

	// Every frame, increase or decrease the rope's length if neccessary
	void Update() {

		// Get the top segment and its joint.
		GameObject topSegment = ropeSegments[0];
		SpringJoint2D topSegmentJoint = topSegment.GetComponent<SpringJoint2D>();
		
		if (isIncreasing) {

			// We're increasing the rope. If it's at max length, add a new segment;
			// otherwise, increase the top rope segment's length.

			if (topSegmentJoint.distance >= maxRopeSegmentLength) {
				CreateRopeSegment();
			} else {
				topSegmentJoint.distance += ropeSpeed * Time.deltaTime;
			}

		}

		if (isDecreasing) {

			// We're decreasing the rope. If it's near zero length, remove the segment;
			// otherwise, decrease the top segment's length.

			if (topSegmentJoint.distance <= 0.005f) {
				RemoveRopeSegment();
			} else {
				topSegmentJoint.distance -= ropeSpeed * Time.deltaTime;
			}

		}

		// The line renderer draws lines from a collection of points. These points
		// need to be kept in sync with the positions of the rop segments.

		// The number of line renderer verticies = number of rope segments, plus a 
		// point at the top for the rope anchor, plus a point at the bottom for the gnome
		lineRenderer.SetVertexCount(ropeSegments.Count + 2);

		// Top vertex is always at the rope's location.
		lineRenderer.SetPosition(0, this.transform.position);

		// For every rope segment we have, make the corresponding line renderer vertex
		// be at its position.
		for (int i = 0; i < ropeSegments.Count; i++) {
			lineRenderer.SetPosition(i+1, ropeSegments[i].transform.position);
		}

		// Last point is at the connected object's anchor. 
		SpringJoint2D connectedObjectJoint = connectedObject.GetComponent<SpringJoint2D>();
		lineRenderer.SetPosition(ropeSegments.Count + 1, 
		                         connectedObject.transform.TransformPoint(connectedObjectJoint.anchor));

	}

}
