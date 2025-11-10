# Axy the Barbarian

Hola profe! Somos el grupo 3 de Intro. al Desarrollo de Videojuegos 202520.

## Detalles generales de la Implementación

* Desarrollado en Unity Engine 6.1 (6000.1.15f1).

* Tenemos un .gitignore bien extenso (robado descaradamente de [este repo oficial de github](https://github.com/github/gitignore/blob/main/Unity.gitignore)), ¡así que el tamaño de este proyecto es cercano a 5 MB!

    * Si usted prefiere que subamos el .zip con todas las texturas y demases (lo equivalente a 1 GB y medio), ¡entonces lo haremos de tal manera!

## Entregas

### Entrega 6

Lo siguiente sirve a modo de solucionar **posibles dudas** que puedan surgir al momento de la corrección.

#### Desviaciones Hechas Respecto al Enunciado de la Entrega

* Se decidió hacer que el radio de ataque (que es en verdad un simple Seek) y flee de la rata sean el mismo, en orden de simplificar la toma de decisiones en el Decision Tree de la rata.

    * Por lo tanto, dicho árbol se ve así:

        1. Root: Player se encuentra dentro del radio definido por ```fleeOrAttackRadius``` (anteriomente *fleeDistanceRadius*, y modificable en el editor)?
            * No? Entonces se Wander-ea.
        2. Yes? Entonces, es de noche?
            * No? Entonces se Flee-ea.
            * Yes? Entonces se Seek-ea (ataca).

* Aunque obtuvimos un 7 en la entrega de Pathfinding, se comentó correctamente que no separamos el cómputo del Pathfinding por frames; es decir, se hace todo en una sola frame, aunque se calcula en intervalos de un segundo.
    * Dada la nota, consideramos que el Pathfinding para el zombie se terminó definitivamente en la entrega pasada, por lo que no sentimos muy necesario implementar la sugerencia por el momento.

    * Además, aunque nos gustaría implementar la sugerencia, el tiempo simplemente no nos dió xd. 

#### Implementación de *Decision Tree*

* Todo lo relacionado al *Decision Tree*, y evaluadores de condiciones, se encuentra dentro de ```Assets/Scripts/Decision Making/```.
    * A resumidas cuentas, y como se puede ver en los pseudocódigos de los nodos en el PDF de la doceava cátedra, solo se pueden construir objetos tipo ActionNode y aquellos que heredan de BinaryDecisionNode (es decir, BoolDecisionNode, DoubleDecisionNode y ObjectDecisionNode); todos los otros nodos son de clases abstractas!

    * Los *Evaluators* son aquellos que heredan de ```BehaviorEvaluator```, los cuales devuelven bools según el *obj* que se le haya pasado al árbol de decision al ser inicializado.

* Se creó un script (Assets/Scripts/Lighting/```DayNightCycle.cs```) para oscurecer el background de noche y globalizar un booleano que indique si es de noche!! Este es implementado como un componente del objeto ```LightingManagerObject``` en la jerarquía de la escena.

* La rata inicializa (```InitializeDecisionTree()```) y resuelve (```EvaluateDecisionTree()```) su *Decision Tree* dentro del *CowardRatInputComponent*.
    * Lo único que puede causar duda es que, en el Init del árbol, los *DecisionNode*s son *ObjectDecisionNode*. Esto se debe a que la rata debe hacer dos evaluaciones para saber que acción ejecutar:

        1. checkear si el jugador se encuentra dentro de ```fleeOrAttackRadius```, siendo esta decisión evaluada según el GameObject *obj* (es decir, el GameObject de la rata).

        1. checkear si es de noche, siendo esta decisión evaluada según el GameObject *world* (el *LightingManagerObject*).

    Por lo tanto, resultó tremendamente conveniente realizar un evaluador separado para cada uno (*Evaluators/CowardRat/IsPlayerInsideRadiusEvaluator.cs* y *Evaluators/Global/IsItNightEvaluator.cs*), obligando a los *DecisionNode*s a ser *ObjectDecisionNode*s!!!

#### Implementación de *Obstacle Avoidance* en la Rata

* También implementado dentro del InputComponent de la rata, el obstacle avoidance se utiliza, mediante la función ```ApplyObstacleAvoidance``` para modificar la orientación de la rata en caso de una colision a 5 unidades de la orientación actual (modificable en el editor mediante la variable *avoidanceRayLength*).

    * Cuando lo implementamos, nos costó harto saber visualmente si es que funcionaba xddd, asi que pusimos un booleano ```ShouldAvoidObstacles``` expuesto en el editor, en orden de activarlo y desactivarlo en la instancia de la rata, y así contrastar con el comportamiento antiguo; para la conveniencia del corrector, la rata es uno de los últimos objetos creados por el LevelLoader, por lo que es bastante rápido de encontrar en la jerarquía de la escena.

    * Opcionalmente, se puede modificar el peso que se le da a la dirección computada de avoidance (```AvoidanceForce```), pero parece que 2 unidades es suficiente para hacer que la rata no choque con las murallas.

* Se pusieron hartas murallas de dos unidades de largo en el Level04.json, en orden de que visualizar de mejor manera el Obstacle Avoidance de la rata.