# Axy the Barbarian

Hola profe! Somos el grupo 3 de Intro. al Desarrollo de Videojuegos 202520.

## Detalles generales de la Implementación

* Desarrollado en Unity Engine 6.1 (6000.1.15f1).

* Tenemos un .gitignore bien extenso (robado descaradamente de [este repo oficial de github](https://github.com/github/gitignore/blob/main/Unity.gitignore)), ¡así que el tamaño de este proyecto es cercano a 5 MB!

    * Si usted prefiere que subamos el .zip con todas las texturas y demases (lo equivalente a 1 GB y medio), ¡entonces lo haremos de tal manera!

## Entregas

### Entrega 3

Lo siguiente sirve para responder posibles dudas que puedan ocurrir a lo largo de la corrección de esta entrega:

* El StateController se encuentra implementado para los GameObjects, aunque no le dimos dicho nombre:

    * Si entendimos bien su propósito (ya que no se encuentra descrito en [la pagina maestra oficial del Component Pattern](https://gameprogrammingpatterns.com/component.html)), entonces componentes que heredan de *MovementComponent* y *PhysicsComponent* hacen la misma pega; actualizan el estado de las entidades (jugador y enemigos), que frecuentemente es solo movimiento.

        * Sin embargo, en el caso del Esqueleto, el unico updateo de "estado' que experimenta es el aumento de su timer interno del intervalo de tiempo necesario para generar otra flecha. Aunque entendemos que esto no es estrictamente estado, sentimos necesario declararlo.

* Aunque gran parte de los componentes de *GameEntity* no son usados por todas las entidades (como el Audio, Physics y Graphics Component), sentimos necesario dejar el GameEntity de esta manera, ya que no existe documento oficial (que pudieramos encontrar, por lo menos) donde la entidad base tenga solo el MovementComponent, por ejemplo.

    * Aun asi, entendemos que parte de la gracia es dejar a la entidad base con lo minimamente necesario para que los que heredan de este funcionen, pero tambien imaginamos que la gracia es que se puedan extender facilmente los enemigos en un posible futuro (e.g., que el Gazer y Esqueleto tengan graficas, audio, etc) sin tener que modificar a Player.