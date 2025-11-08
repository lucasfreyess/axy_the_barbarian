# Axy the Barbarian

Hola profe! Somos el grupo 3 de Intro. al Desarrollo de Videojuegos 202520.

## Detalles generales de la Implementación

* Desarrollado en Unity Engine 6.1 (6000.1.15f1).

* Tenemos un .gitignore bien extenso (robado descaradamente de [este repo oficial de github](https://github.com/github/gitignore/blob/main/Unity.gitignore)), ¡así que el tamaño de este proyecto es cercano a 5 MB!

    * Si usted prefiere que subamos el .zip con todas las texturas y demases (lo equivalente a 1 GB y medio), ¡entonces lo haremos de tal manera!

## Entregas

### Entrega 5

Lo siguiente sirve a modo de solucionar **posibles dudas** que puedan surgir al momento de la corrección.

#### Sobre la rata maldita

* Se creo el nivel *Level04.json* para acomodar un espacio grande para la rata.
    * Este se encuentra inmediatamente por abajo de la room donde spawnea el jugador.
    * Asi se puede correr harto para probar que el flee y el wander de la rata funcionan!

* La rata (de color marron claro) posee velocidades diferentes si esta Flee-eando o Wander-eando; se hizo tal que la velocidad en Flee fuera mayor a la de wander y a la del jugador, para asi simular comportamiento de rata real!!
    * Ambas velocidades mencionadas pueden ser modificadas en el ```InputComponent``` de la rata, el cual funge como la IA de esta.

#### Sobre el Grafo para Pathfinding y el Zombie

* Los scripts creados para realizar Pathfinding se encuentran dentro de ```Assets/Scripts/Pathfinding/```.

* *GraphNode.cs* es la representacion de los nodos del grid, donde cada nodo tiene dimensiones de una unidad real del mapa (es decir, (1,1)).

* *PathfindingGrid.cs* vive dentro del ```PathfindingObject``` en la jerarquia.

    * Las dimensiones del mapa pueden ser alteradas en en el inspector del objeto.
        * es importante que el origen sea el limite inferior izquierdo del mapa!!

    * Su logica es la siguiente:
        * El script crea el grid (o grafo), añadiendo todos los nodos del mapa.
        * Luego, marca todas las grids en las que exista una muralla, preocupandose de marcar dos nodos "radialmente" hacia afuera de esta, en orden de que el zombie no se quede pegado al rodear una muralla cualquiera
            * dado esto, el zombie solo puede pasar entre dos murallas si hay por lo menos un espacio de 5 nodos entre estas.
        * Todos los nodos son inicializados con peso infinito, y las heuristicas se van calculando en el script del A*.

* *AStarAlgorithm* tambien vive dentro del objeto anteriormente mencionado!!
    
    * Evidentemente, implementa A* xddd.. la verdad es que es bastante calcado del pseudocodigo de la clase, asi que no creemos necesario explicarlo.
    * Lo que si puede resultar curioso es que decidimos por calcular el peso G de cada neighbor con la heuristica (es decir, la distancia euclidiana). Esto se debe a dos cosas:
        * Dado que los nodos son de 1x1, entonces la distancia entre cada un nodo y otro siempre va a ser la distancia euclidiana entre ellos.
        * Si haciamos que G=1 siempre, entonces la heuristica seria mayor para nodos conectados diagonalmente (h=1.41), por lo que haciendo que G=distancia euclidiana, entonces la heuristica siempre va a ser menor o igual que G.

* El zombie sigue el Component Pattern, por lo que:
    * el ratio de recalculacion de su path (medido en segundos) se encuentra en su InputComponent, dentro del inspector en su prefab (ya que el InputComponent funge como componente de IA).
    * La velocidad se encuentra en su PhysicsComponent.

* Finalmente, se creo un maze super simple para el zombie en ```Level02.json```, en orden de demostrar que esta caga de pathfinding efectivamente encuentra el path!!
    * Recomendamos situar al jugador en la sala del maze, para poder ver como el zombie lo resuelve y como llega hacia uno.
