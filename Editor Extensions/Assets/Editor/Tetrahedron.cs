using UnityEngine;
using System.Collections;

// BEGIN tetrahedron_wizard
using UnityEditor;

public class Tetrahedron : ScriptableWizard {
    
    // BEGIN wizard_variable
	// This variable will appear just like it would in the inspector
    public Vector3 size = new Vector3(1,1,1);
	// END wizard_variable


    // BEGIN wizard_show
	// This method can be called anything - the important thing
    // is that it's static and has the MenuItem attribute
    [MenuItem("GameObject/3D Object/Tetrahedron")]
	static void ShowWizard() {
		// First parameter is title, second is the label on the Create button
		ScriptableWizard.DisplayWizard<Tetrahedron>("Create Tetrahedron", "Create");
	}
	// END wizard_show

    // BEGIN wizard_create
	// Called when the user clicks the Create button
    void OnWizardCreate() {

		// Create a mesh 
		var mesh = new Mesh();

		// Create the four points
		Vector3 p0 = new Vector3(0,0,0);
		Vector3 p1 = new Vector3(1,0,0);
		Vector3 p2 = new Vector3(0.5f,0,Mathf.Sqrt(0.75f));
		Vector3 p3 = new Vector3(0.5f,Mathf.Sqrt(0.75f),Mathf.Sqrt(0.75f)/3);

		// Scale them based on size
		p0.Scale(size);
		p1.Scale(size);
		p2.Scale(size);
		p3.Scale(size);

		// Provide the the list of vertices
		mesh.vertices = new Vector3[] {p0,p1,p2,p3};

		// Provide the list of triangles that connect each of the vertices
		mesh.triangles = new int[] {
			0,1,2,
			0,2,3,
			2,1,3,
			0,3,1
		};

		// Update some additional data on the mesh, using this data
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		;

		// Create a game object that uses this mesh
		var gameObject = new GameObject("Tetrahedron");
		var meshFilter = gameObject.AddComponent<MeshFilter>();
		meshFilter.mesh = mesh;

		var meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.material = new Material(Shader.Find("Standard"));

	}
	// END wizard_create

    // BEGIN wizard_update
	// Called whenever the user changes anything in the wizard
    void OnWizardUpdate() {

		// Check to make sure that the values provided are valid
		if (this.size.x <= 0 || 
			this.size.y <= 0 || 
			this.size.z <= 0) {

			// When isValid is true, the Create button can be clicked
			this.isValid = false;

			// Explain why this is the case
			this.errorString = "Size cannot be less than zero";

		} else {

			// The user can click create, so enable it and clear
			// any error message
			this.errorString = null;
			this.isValid = true;
		}
	}
	// END wizard_update

}
// END tetrahedron_wizard
