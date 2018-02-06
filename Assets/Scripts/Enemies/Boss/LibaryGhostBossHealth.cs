using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibaryGhostBossHealth : MonoBehaviour {

    [SerializeField]
    private int maxHealth = 3500;
    private float health;
    private GhostBossController bossControl;
    public Image healthbar;

    public bool useRelativeRotation = true;       // Use relative rotation should be used for this gameobject?
    public Transform relativeRotationTransform;          // The local rotatation at the start of the scene.
    public Quaternion relativeRotation;
    public GameObject ghost;
    //Percentage of a ghost is spawned by damage taken Value between 0-1, 1 = 100%; 0 = 0%
    public float ghostSpawnPercentage = 0.5f;

    private Vector3 maxScale;

    //Header Beispiel für bessere Übersicht im Inspector
    [Header("DMG")]
    public bool isDead = false;

    private Animator anim;


    void Start()
    {
        health = maxHealth;
        healthbar.fillAmount = health / maxHealth;
        relativeRotation = relativeRotationTransform.rotation;
        anim = GetComponent<Animator>();
        bossControl = GetComponent<GhostBossController>();
        isDead = false;
        maxScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && !isDead)
        {
            StartCoroutine(Die());
        }
        if (useRelativeRotation) relativeRotationTransform.rotation = relativeRotation;

    }

    public void SetHealth(int value)
    {
        maxHealth = value;
        Start();
    }

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            health -= damage;
            healthbar.fillAmount = (float)health / maxHealth;
            if (Random.Range(0f, 1f) < ghostSpawnPercentage) Instantiate(ghost, transform.position, ghost.transform.rotation);
            if (anim != null) anim.SetTrigger("damaged");
            //Scale of the Ghost is min 25% + Amount of Life left Full Life 75% halflife 37,5% ...
            transform.localScale = maxScale * (0.25f + (0.75f * (health / maxHealth)));
            SetRageMode();
        }
    }

    private void SetRageMode()
    {
        if (health / maxHealth < 0.25) bossControl.SetRageMode(true,(health/maxHealth));
    }

    IEnumerator Die()
    {
        isDead = true;
        anim.SetBool("die", true);
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
        yield return null;
    }

}
