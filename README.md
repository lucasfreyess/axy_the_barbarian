# Axy the Barbarian

Hola profe! Somos el grupo 3 de Intro. al Desarrollo de Videojuegos 202520.

## Detalles generales de la Implementación

* Desarrollado en Unity Engine 6.1 (6000.1.15f1).

* Tenemos un .gitignore bien extenso (robado descaradamente de [este repo oficial de github](https://github.com/github/gitignore/blob/main/Unity.gitignore)), ¡así que el tamaño de este proyecto es cercano a 5 MB!

    * Si usted prefiere que subamos el .zip con todas las texturas y demases (lo equivalente a 1 GB y medio), ¡entonces lo haremos de tal manera!

## Entregas

### Entrega 4

Lo siguiente sirve a modo de solucionar **posibles dudas** que puedan surgir al momento de la corrección.

* La creación de *Game Objects* a partir de archivos .json se encuentra en *Assets/Scripts/Levels/LevelManager.cs*

    * Se encuentra como componente en el objeto *LevelLoader* en la jerarquía de la escena.

    * A resumidas cuentas, se tiene un *booleano* que por defecto se encuentra activado, el cual ordena a *LevelManager* crear todos los niveles .json dentro de la carpeta *Assets/Resources/Levels/*, dado los prefabs de murallas y enemigos enlazados en el editor.

    * El comportamiento detallado de creación se encuentra en la carpeta anteriormente mencionada.

* Lamentablemente, y como se podra haber visto al jugar, no implementamos poder cambiar el timing del esqueleto y la velocidad del gazer, por lo que el gameplay esta bien aburrido xdd 

    * pero de que hay tres salas con *hallways* entre ellas, no se puede negar!!