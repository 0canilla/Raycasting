using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    private float distance = 5f;
    [SerializeField] private GameObject shootOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastCannon();
    }

    private void RaycastCannon()
    {
        RaycastHit hit;

        if (Physics.Raycast(shootOrigin.transform.position, shootOrigin.transform.TransformDirection(Vector3.forward), out hit, distance))
        {
            GameMananger.instance.AddScore();
            Debug.Log(GameMananger.GetScore());
        }
;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(shootOrigin.transform.position, shootOrigin.transform.TransformDirection(Vector3.forward) * distance);
    }
}
