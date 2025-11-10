using UnityEngine;


public class IsPlayerInsideRadiusEvaluator : BehaviorEvaluator
{
    // evaluador para saber si el jugador se encuentra dentro del fleeDistanceRadius (actualmente igual a 5f) de la rata

    public override bool Evaluate(GameObject obj, GameObject world)
    {
        // codigo original para flee y wander
        // si jugador esta dentro del radio de flee de la rata, se flee-ea; e.o.c. (idly), se wander-ea
        //if (distanceToPlayer <= fleeDistanceRadius)
        //    targetMoveDirection = ProcessFleeMovement();
        //else
        //    targetMoveDirection = ProcessWanderMovement();

        // get-eo de propiedades de rata y jugador
        CowardRatInputComponent ratInputComponent = obj.GetComponent<CowardRatInputComponent>();
        float fleeOrAttackRadius = ratInputComponent.fleeOrAttackRadius;

        Transform playerTransform = ratInputComponent.playerTransform;
        float distanceToPlayer = Vector2.Distance(playerTransform.position, obj.transform.position);

        // evaluacion; si jugador esta dentro del radio, se flee-ea o ataca; e.o.c. (idly), se wander-ea
        if (distanceToPlayer <= fleeOrAttackRadius) return true;
        else return false;
    }
}
