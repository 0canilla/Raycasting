using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public float Force;
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.forward * Force * 100, ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnDestroy()
    {
        Gamemanager.instance.AddScore();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Robot")
        {
            Destroy(gameObject);
        }
    }
}
