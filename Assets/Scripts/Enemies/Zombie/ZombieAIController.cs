using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAIController : EnemyAIController {

    new void Start()
    {
        base.Start();
        ID = 5;
        meeleDMG = (int)(enemy.Golem[0] + enemy.Golem[2] * (0.5 * enemy.Golem[0]) - (0.5 * enemy.Golem[0]));
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

    }

}
