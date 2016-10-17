using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

    public Mesh mesh;
    public Material material;
    public GameObject something;
    public int maxDepth;
    public float childScale;
    public float generateSpeed;

    private int depth;
    private static Vector3[] growingDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };
    private static Quaternion[] growingRotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f)
    };

	// Use this for initialization
	void Start () {
      //  gameObject.AddComponent<MeshFilter>().mesh = GetComponent<MeshFilter>().mesh;
      //  gameObject.AddComponent<MeshRenderer>().material = GetComponent<MeshRenderer>().material;
      //  GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.blue, (float)depth / maxDepth);

		if (depth < maxDepth) {
            StartCoroutine(createChildren(something));
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator createChildren(GameObject parent)
    {
        int random = (int)Random.Range(0f, 4.99f);
        for(int i=0; i<5; i++)
        {
            random = (random + 1) % 5;
            yield return new WaitForSeconds(parent.GetComponent<Fractal>().generateSpeed);
            //new GameObject("Fractal Child").AddComponent<Fractal>().initialize(this, growingDirections[random], growingRotations[random]);
            GameObject newChild = GameObject.Instantiate(parent, parent.transform.position + growingDirections[random], growingRotations[random]) as GameObject;
            newChild.transform.SetParent(this.transform);
        }
    }

	private void initialize (Fractal parent, Vector3 direction, Quaternion orientation) {
		mesh = parent.mesh;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		childScale = parent.childScale;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = direction * (0.5f + 0.5f * childScale);
        transform.localRotation = orientation;
	}
}
