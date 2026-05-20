using System.Collections;
using UnityEngine;


public class CameraTransition : MonoBehaviour
{
    [SerializeField]
    Transform cam;
    [System.Serializable]
    public struct TransitionPoint
    {
        public Vector3 TransitPoint;
        public GameObject cameraPos;
        public float distanceToPlayer;
    }
    [SerializeField]
    TransitionPoint[] hubPoints, roomPoints;
    [SerializeField] GameObject player;
    [SerializeField] AnimationCurve transitionCurve;
    GameObject currentRoom, nextRoom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentRoom = hubPoints[0].cameraPos;

    }
    IEnumerator Transition()
    {
        var currentRoomPos = currentRoom.transform.position;
        var nextRoomPos = nextRoom.transform.position;
        float t = 0;
        while (t < 1)
        {
            Debug.Log("Moving");
            t += Time.deltaTime;
            cam.position = Vector3.Lerp(currentRoomPos, nextRoomPos, transitionCurve.Evaluate(t));
            yield return new WaitForFixedUpdate();
        }
        cam.position = nextRoomPos;


    }
    void GetTransition(TransitionPoint[] points)
    {
        foreach (TransitionPoint point in points)
        {
            if (Vector3.Distance(player.transform.position, point.TransitPoint) < point.distanceToPlayer)
            {
                nextRoom = point.cameraPos;
                //StopCoroutine(Transition());
                StartCoroutine(Transition());
                Debug.Log("Transitioning to " + nextRoom.name);
                currentRoom = point.cameraPos;
            }
        }
    }

    void Update()
    {
        if (currentRoom != hubPoints[0].cameraPos) GetTransition(hubPoints);
        else GetTransition(roomPoints);
    }

    //Gizmos
    void DrawPoints(TransitionPoint[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawWireSphere(points[i].TransitPoint, points[i].distanceToPlayer);
            Gizmos.DrawLine(points[i].TransitPoint, points[i].cameraPos.transform.position);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        DrawPoints(roomPoints);
        Gizmos.color = Color.blue;
        DrawPoints(hubPoints);
    }
}
