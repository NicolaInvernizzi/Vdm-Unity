using UnityEngine;

public class ProjectileDraw : MonoBehaviour
{
    public int steps;
    public float maxTime;
    [HideInInspector] public LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }
    public void DrawTrajectory(Vector3 start_Position, Vector3 start_Velocity)
    {
        /*
         * Es. 
         * maxTime = 5
         * steps = 10
         * i = 0 -> time = 0/10 * 5 = 0
         * i = 1 -> time = 1/10 * 5 = 5/10
         * i = 2 -> time = 2/10 * 5 = 5/5
         * i = 5 -> time = 5/10 * 5 = 5/2
         * i = 10 -> time = 10/10 * 5 = 5
         */
        float time;

        // set line renderer max vertexes
        lineRenderer.positionCount = steps;

        lineRenderer.enabled = true;

        for (int i = 0; i < steps; i++)
        {
            // take a maxTime %
            time = ((float)i / steps) * maxTime;

            Vector3 Vxz = start_Velocity;
            Vxz.y = 0f;

            Vector3 Position = start_Position + start_Velocity * time;
            float Position_Y = start_Position.y +
                (start_Velocity.y * time) +
                (0.5f * Physics.gravity.y * Mathf.Pow(time, 2));

            Position.y = Position_Y;

            // set line renderer vertex
            lineRenderer.SetPosition(i, Position);
        }
    }
    public void DisableLineRenderer()
    {
        lineRenderer.enabled = false;
    }
}
