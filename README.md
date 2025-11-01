# Axy the Barbarian

Hola profe! Somos el grupo 3 de Intro. al Desarrollo de Videojuegos 202520.

## Detalles generales de la Implementación

* Desarrollado en Unity Engine 6.1 (6000.1.15f1).

* Tenemos un .gitignore bien extenso (robado descaradamente de [este repo oficial de github](https://github.com/github/gitignore/blob/main/Unity.gitignore)), ¡así que el tamaño de este proyecto es cercano a 5 MB!

    * Si usted prefiere que subamos el .zip con todas las texturas y demases (lo equivalente a 1 GB y medio), ¡entonces lo haremos de tal manera!

## Entregas

### Entrega 4

Lo siguiente sirve a modo de solucionar **posibles dudas** que puedan surgir al momento de la corrección.

* Ya que el jugador es medio chico, decidimos que la camara lo siga en todo momento, por lo que no se puede ver todas las rooms con la camara en un momento dado.

    * Sin embargo, si se cambia la *view* del juego a la *scene* mientras se juega, entonces se puede ver a todas las rooms al mismo tiempo. 

    * Más aún, se puede sacar la Main Camera al root de la jerarquía de la escena (ya que actualmente es "hijo" del Game Object del jugador) y decrecerle el zoom para poder ver todo el nivel al mismo tiempo, pero no lo recomendamos pq es medio incomodo xd

* Dado como se hizo la clase base de entidades (*Assets/Scripts/Entitites/GameEntity.cs*) en la entrega pasada, entonces todas las entidades (jugador, gazer y esqueleto) tienen posiciones iniciales modificables desde el editor (*StartingX* y *StartingY*)

* Dadas previas implementaciones, el gazer ya se movia a *speed* unidades-por-segundo. Esta variable es modificable desde el inspector en el prefab del Gazer (Assets/Prefabs/GazerEnemy.prefab), en el componente de Movimiento de este.

    * Por default, *speed* equivale a 3.65, por lo que el gazer se mueve 3.65 unidades por segundo.

    * Sin embargo, no se implemento el protip de *Velocity is calculated in the controller, and movement in the Physics*, ya que (por ahora) ningún otro componente del Gazer utiliza *speed*, por lo que preferimos dejar el cálculo de velocidad dentro del componente de Movimiento del Gazer.

        * Cabe mencionar que, por la implementacion anterior, Gazer no tiene un Physics Component, ya que en estricto rigor no tiene fisicas! aunque si tiene colisiones! Dado esto, si tiene un MovementComponent

* El esqueleto tiene tanto el área de disparo de sus flechas, como el intervalo de tiempo entre disparos, disponibles para editar en el inspector de su prefab (Assets/Prefabs/SkeletonArcherEnemy.prefab)!

    * El *arrowAreaRadius* se puede modificar en la vista del componente de Shooting del esqueleto, dentro del inspector del prefab de este último.

    * De manera similar, el *shootInterval* entre cada flecha se puede modificar en el componente de Timing de este.

* La creación de *Game Objects* a partir de archivos .json se encuentra en *Assets/Scripts/Levels/LevelManager.cs*

    * Se encuentra como componente en el objeto *LevelLoader* en la jerarquía de la escena.

    * A resumidas cuentas, se tiene un *booleano* que por defecto se encuentra activado, el cual ordena a *LevelManager* crear todos los niveles .json dentro de la carpeta *Assets/Resources/Levels/*, dado los prefabs de murallas y enemigos enlazados en el editor.

    * El comportamiento detallado de creación se encuentra en la carpeta anteriormente mencionada.

* Lamentablemente, y como se podra haber visto al jugar, no implementamos poder cambiar el timing del esqueleto y la velocidad del gazer, por lo que el gameplay esta bien aburrido xdd 

    * pero de que hay tres salas con *hallways* entre ellas, no se puede negar!!