using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionItems : HealthScript {

    // Use this for initialization

    [SerializeField]
    private int explosionDMG = 25;
    public LayerMask explosionMask;
    public float explosionForce = 1000f;
    public float explosionRadius = 5f;
    public bool isExploding = false;

    public float m_MaxLifeTime = 2f;
    public ParticleSystem explosionParticles;
    public AudioSource m_ExplosionAudio;

    new void Start () {
        base.Start();
	}

    new void Update()
    {
        base.Update();
    }

    // Update is called once per frame
    protected override void Dying()
    {
        isExploding = true;
        Collider[] colliders;
        colliders = Physics.OverlapSphere(transform.position, explosionRadius, explosionMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (!colliders[i].isTrigger)
            {
                if (colliders[i].CompareTag("Enemy") || colliders[i].CompareTag("Destroyable"))
                {
                    //Apply Damage
                    if (colliders[i].GetComponent<EnemyHealth>()) colliders[i].GetComponent<EnemyHealth>().TakeDamage(explosionDMG);
                    else if (colliders[i].GetComponent<HealthScript>()) colliders[i].GetComponent<HealthScript>().TakeDamage(explosionDMG);
                }
                else if (colliders[i].CompareTag("Boss")) colliders[i].GetComponent<LibaryGhostBossHealth>().TakeDamage(explosionDMG);
                else if (colliders[i].CompareTag("Slime")) colliders[i].GetComponent<SlimeHealthChild>().TakeDamage(explosionDMG);
                else if (colliders[i].CompareTag("Player")) colliders[i].GetComponent<PlayerHealth>().TakeDamage(explosionDMG);
                else if (colliders[i].CompareTag("Explosion") && !colliders[i].GetComponent<ExplosionItems>().isExploding) colliders[i].GetComponent<ExplosionItems>().TakeDamage(explosionDMG);
                
                Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();
                if (!targetRigidbody) continue;
                targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, -50f);
                
            }            
        }
        explosionParticles.Play();
        Destroy(explosionParticles.transform.parent.gameObject, 1.5f);
        Destroy(gameObject);

    }

    
}
