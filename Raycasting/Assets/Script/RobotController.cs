using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    private float distance = 5f;
    bool Follow = false;
    [SerializeField] private GameObject shootOrigin;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastRobot();
        if (Follow == true)
        {
            LookAtPlayer(Player);
        }
    }

    private void RaycastRobot()
    {
        RaycastHit hit;

        if (Physics.Raycast(shootOrigin.transform.position, shootOrigin.transform.TransformDirection(Vector3.forward), out hit, distance))
        {
            Follow = true;
        }
        else
        {
            Follow = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootOrigin.transform.position, shootOrigin.transform.TransformDirection(Vector3.forward) * distance);
    }

    private void LookAtPlayer(GameObject lookObject)
    {
        Vector3 direction = lookObject.transform.position - transform.position;
        Quaternion newQuaternion = Quaternion.LookRotation(direction);
        transform.rotation = newQuaternion;
    }
}
