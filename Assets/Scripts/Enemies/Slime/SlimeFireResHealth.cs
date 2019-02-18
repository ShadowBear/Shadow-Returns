using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFireResHealth : EnemyHealth {


    PlayerAttack playerAttack;
	// Use this for initialization
	new void Start () {
        base.Start();
        playerAttack = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerAttack>();
	}

    new void Update()
    {
        base.Update();
    }

    public override void TakeDamage(float damage)
    {
        
    }

    public override void TakeDamage(float damage, bool melee)
    {
        if (!isDead && !isShielded)
        {
            Weapon.WeaponEffect weaponEffect = playerAttack.GetEquipedWeapon().GetWeaponEffect();
            hitable = false;
            hitDelay = startHitDelay;
            TakeHit(melee);

            if (Weapon.WeaponEffect.fire == weaponEffect)
            {
                health -= damage;
                if (healthbar != null) healthbar.fillAmount = (float)health / maxHealth;
                if (anim != null) anim.SetTrigger("damaged");
                if (damage > 1) GameManager.control.ShowDmgText(damage, transform);

                if (health <= 0 && !isDead)
                {
                    Dying();
                }
            }            
        }
    }

}
