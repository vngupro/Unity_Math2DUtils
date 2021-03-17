using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TriangleMeshGenerator : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public Transform C;
	
	public Color color;

    void Start() {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = new Vector3[3];
        mesh.triangles = new int[] { 0, 1, 2 };
        SetPointPositions(A.position, B.position, C.position);
        SetPointColors(color, color, color);
	}

	public void SetPointPositions(Vector3 a, Vector3 b, Vector3 c) {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        mesh.vertices = new Vector3[] {
			a - transform.position,
			b - transform.position,
			c - transform.position
		};
    }

    public void SetPointColors(Color a, Color b, Color c) {
        Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
        mesh.colors = new Color[] { a, b, c };
    }
}