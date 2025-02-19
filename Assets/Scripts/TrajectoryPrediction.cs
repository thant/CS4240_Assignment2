using UnityEngine;
using System.Collections.Generic;

public class TrajectoryPrediction : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform launchPoint;  // The point from where the rock will be shot
    public int predictionSteps = 30;  // Number of points in trajectory
    public float timeStep = 0.05f;  // Simulation time step
    public float initialVelocity = 10f; // Initial speed for trajectory prediction

    private void Update()
    {
        ShowTrajectory();
    }

    void ShowTrajectory()
    {
        List<Vector3> trajectoryPoints = new List<Vector3>();
        Vector3 currentPosition = launchPoint.position;
        Vector3 currentVelocity = launchPoint.forward * initialVelocity; // Use initial velocity for prediction

        for (int i = 0; i < predictionSteps; i++)
        {
            trajectoryPoints.Add(currentPosition);
            currentVelocity += Physics.gravity * timeStep; // Apply gravity
            currentPosition += currentVelocity * timeStep; // Update position
        }

        lineRenderer.positionCount = trajectoryPoints.Count; // Set the number of points in the line renderer
        lineRenderer.SetPositions(trajectoryPoints.ToArray()); // Set the calculated trajectory points
    }
}
