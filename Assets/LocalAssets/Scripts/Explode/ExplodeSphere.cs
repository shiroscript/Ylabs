using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeSphere : MonoBehaviour
{
    public float radius = 5f;
    public float power = 400f;

    Collider[] players;

    private void Start()
    {
        Explode();
    }

    public void Explode()
    {
        players = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider c in players)
        {
            if (c.tag.Equals("NPC"))
            {
                c.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius);
            }
        }
    }

}
