using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
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
        public List<EnemyAI> enemyAI;
        
    }
    [SerializeField]
    TransitionPoint[] hubPoints, roomPoints;
    [SerializeField] GameObject player;
    [SerializeField] AnimationCurve transitionCurve;
    TransitionPoint currentRoom, nextRoom;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CameraTransition instance;
    void Start()
    {
        instance = this;
        currentRoom = hubPoints[0];

    }
    public void Reset()
    {
        Exit(currentRoom);
        currentRoom = hubPoints[0];
        Camera.main.transform.position = currentRoom.cameraPos.transform.position;
    }
    IEnumerator Transition()
    {
        Exit(currentRoom);
        Enter(nextRoom);
        var currentRoomPos = currentRoom;
        var nextRoomPos = nextRoom;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            cam.position = Vector3.Lerp(currentRoomPos.cameraPos.transform.position, nextRoomPos.cameraPos.transform.position, transitionCurve.Evaluate(t));
            yield return new WaitForFixedUpdate();
        }
        cam.position = nextRoomPos.cameraPos.transform.position;


    }
    public void Enter(TransitionPoint room)
    {
        foreach (EnemyAI enemy in room.enemyAI) 
            if(enemy)enemy.enabled = true;
    }
    public void Exit(TransitionPoint room)
    {
        foreach (EnemyAI enemy in room.enemyAI)
            if (enemy) enemy.enabled = false;
    }
    void GetTransition(TransitionPoint[] points)
    {
        foreach (TransitionPoint point in points)
        {
            if (Vector3.Distance(player.transform.position, point.TransitPoint) < point.distanceToPlayer)
            {
                nextRoom = point;
                //StopCoroutine(Transition());
                StartCoroutine(Transition());
                currentRoom = point;
            }
        }
    }

    void Update()
    {
        if (currentRoom.cameraPos != hubPoints[0].cameraPos) GetTransition(hubPoints);
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
