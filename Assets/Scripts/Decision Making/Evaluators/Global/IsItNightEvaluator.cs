using UnityEngine;


// en realidad solo sirve para los Decision Trees que tengan world=lightingManagerObject (es decir, la rata), 
// asi que no es tan global como lo insinua el nombre de la carpeta pero era
public class IsItNightEvaluator : BehaviorEvaluator
{
    public override bool Evaluate(GameObject obj, GameObject world)
    {
        //world = lightingManagerObject
        bool isItNight = world.GetComponent<DayNightCycle>().isItNight;

        if (isItNight) return true;
        else return false;
    }
}
