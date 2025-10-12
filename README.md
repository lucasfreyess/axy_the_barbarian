# Axy the Barbarian

Hola profe! Somos el grupo 3 de Intro. al Desarrollo de Videojuegos 202520.

## Detalles generales de la Implementación

* Desarrollado en Unity Engine 6.1 (6000.1.15f1).

* Tenemos un .gitignore bien extenso (robado descaradamente de [este repo oficial de github](https://github.com/github/gitignore/blob/main/Unity.gitignore)), ¡así que el tamaño de este proyecto es cercano a 5 MB!

    * Si usted prefiere que subamos el .zip con todas las texturas y demases (lo equivalente a 1 GB y medio), ¡entonces lo haremos de tal manera!

## Entregas

### Entrega 1

* La escena del juego se encuentra en *Assets/Scenes/SampleScene.unity*.

Los siguientes incisos sirven a modo de resolver posibles dudas que pueden surgir en el proceso de corrección:

#### Parte 1

* ¡Se puede cambiar el color del ***personaje*** (cuadradito blanco) con la barra espaciadora!
* El script que controla al personaje está en *Assets/Scripts/PlayerMovement.cs*

#### Parte 2

* El sprite del ***Gazer*** es un cuadrado de color rojo.
* El script del Gazer se encuentra en *Assets/Scripts/GazerMovement.cs*

#### Parte 3

* El sprite del ***Drunk-Skeleton-Archer Hiper-Mega 3000*** es un cuadrado de color negro.
    * Las flechas que spawnea el Esqueleto son triangulares y amarillas.

* El script del Arquero-Esqueleto se encuentra en *Assets/Scripts/PlayerMovement.cs*

* Aunque tenemos claro que el Gazer no debería hacer daño al jugador en esta entrega, no estamos seguros de si las flechas del arquero deben hacerlo.

    * Dado que el propósito de esta entrega es de implementar el ***Game Loop Pattern*** con tal de *decouple-ear* el procesamiento de inputs y la actualización de estados cada frame, entonces pensamos que lo más probable es que no fuera necesario que el arquero hiciera daño, ya que esto escaparía un poco el propósito mencionado.

* A veces, la flecha se instanciará dentro del sprite del Arquero xddd; por ahora, no debería ser mucho problema, pero en el futuro tenemos pensado que aparezca más allá de dicho sprite, o por lo menos que aparezca en un layer más arriba de donde se encuentra el Arquero.

* Quizás más trivialmente, el intervalo de spawneo de las flechas es diferente a la velocidad del Gazer (1 segundo vs. 3.65 segundos).