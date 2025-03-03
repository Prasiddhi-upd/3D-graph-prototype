using UnityEngine;

public class GraphWithCameraControl : MonoBehaviour
{
    public int numberOfNodes = 10;      // Number of nodes to generate
    public float radius = 5f;           // Radius of the area to spawn nodes
    public float edgeThickness = 0.1f;  // Thickness of edges

    private Vector3[] nodes;           // Array to store node positions
    private LineRenderer[] edges;      // Array to store line renderers (edges)

    // Camera control variables
    public float rotationSpeed = 50f;   // Speed of camera rotation
    public float zoomSpeed = 10f;       // Speed of camera zoom

    void Start()
    {
        // Initialize arrays for nodes and edges
        nodes = new Vector3[numberOfNodes];
        edges = new LineRenderer[numberOfNodes];

        // Generate random nodes and edges
        for (int i = 0; i < numberOfNodes; i++)
        {
            // Random position in 3D space within the radius
            float angle = i * Mathf.PI * 2f / numberOfNodes;
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);
            float y = Random.Range(-2f, 2f);  // Random height

            nodes[i] = new Vector3(x, y, z);

            // Create sphere object for each node
            GameObject nodeObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            nodeObject.transform.position = nodes[i];
            nodeObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);  // Smaller spheres
            nodeObject.GetComponent<Renderer>().material.color = Color.green;  // Color for nodes

            // Create edges to connect nodes
            if (i < numberOfNodes - 1)
            {
                GameObject edgeObject = new GameObject("Edge_" + i);
                LineRenderer lr = edgeObject.AddComponent<LineRenderer>();
                lr.positionCount = 2;  // Each edge connects two nodes
                lr.SetPosition(0, nodes[i]);
                lr.SetPosition(1, nodes[i + 1]);
                lr.startWidth = edgeThickness;
                lr.endWidth = edgeThickness;
                lr.material = new Material(Shader.Find("Sprites/Default"));
                lr.startColor = Color.red;
                lr.endColor = Color.blue;
                edges[i] = lr;
            }
            else
            {
                GameObject edgeObject = new GameObject("Edge_" + (numberOfNodes - 1));
                LineRenderer lr = edgeObject.AddComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, nodes[i]);
                lr.SetPosition(1, nodes[0]);  // Loop to the first node to close the cycle
                lr.startWidth = edgeThickness;
                lr.endWidth = edgeThickness;
                lr.material = new Material(Shader.Find("Sprites/Default"));
                lr.startColor = Color.red;
                lr.endColor = Color.blue;
                edges[i] = lr;
            }
        }
    }

    void Update()
    {
        // Camera rotation (mouse input)
        float horizontal = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Rotate the camera around the center (0,0,0) to look at the graph
        transform.RotateAround(Vector3.zero, Vector3.up, horizontal);
        transform.RotateAround(Vector3.zero, transform.right, vertical);

        // Camera zoom (scroll wheel)
        float scroll = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        Camera.main.fieldOfView -= scroll * 100f;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 10f, 60f);
    }
}
