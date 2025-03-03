using UnityEngine;

public class GraphVisualizer : MonoBehaviour
{
    // Set number of points to visualize
    public int numberOfPoints = 10;
    public float radius = 5f;

    // Store points' positions
    private Vector3[] points;

    // LineRenderer to connect the points
    private LineRenderer lineRenderer;

    void Start()
    {
        points = new Vector3[numberOfPoints];
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfPoints;

        // Set LineRenderer properties
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.blue;

        // Generate 3D points in a circular pattern
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfPoints;
            float x = radius * Mathf.Cos(angle);
            float z = radius * Mathf.Sin(angle);
            points[i] = new Vector3(x, Random.Range(0f, 5f), z); // Random Y for height
            lineRenderer.SetPosition(i, points[i]);

            // Create a small sphere at each point
            GameObject pointObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointObject.transform.position = points[i];
            pointObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); // Smaller spheres
            pointObject.GetComponent<Renderer>().material.color = Color.green; // Color for points
        }

        // Optionally connect the first and last points to close the loop
        lineRenderer.SetPosition(numberOfPoints - 1, points[0]);
    }
}
