using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : HealthScript{

    public GameObject spawnItem;
    private float explosionRadius = 5;
    private int explosionForce = 250;

    // Use this for initialization
    new void Start () {
        SetHealth(1);
	}

    protected override void Dying()
    {
        if (spawnItem)
        {
            GameObject item = Instantiate(spawnItem, transform.position + new Vector3(0, 1, 0), transform.rotation);
            if (item.GetComponent<Rigidbody>())
            {
                Vector3 randomExplosionVector = new Vector3(0, 0.9f, 0.5f);
                item.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position + randomExplosionVector, explosionRadius);
            }
        }
        Destroy(gameObject);
    }
}
