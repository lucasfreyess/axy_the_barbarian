# Creación de Niveles

## Comportamiento de las walls

**Todo está en base al origen 0,0.**

* Cada wall mide 0.94x20 bloques.
  * Sin rotar, dicha wall se encuentra en vertical. 
  * Se rota en 90 grados para que esté en horizontal.

* Para hacer más pequeño o grande a la wall, modificar el valor de scaleY calculandolo con: Bloques_actuales/20.

* Las walls aparecen con su centro como punto de origen, por lo que tendrán Bloques_actuales/40 bloques a cada lado.

* Todo lo anterior tambien aplica para los exitWall.

* Es muy importante actualizar el Starting_X y Starting_Y para el player al cargar otro nivel**

## Ejemplo de un nivel en formato .json

```json
{
  "levelName": "Level 01",
  "wall": [
    { "posX": -15, "posY": 0, "rotation": 0, "scaleY": 1.0 },
    { "posX": 0, "posY": -10, "rotation": 90, "scaleY": 1.5 },
    { "posX": 10, "posY": -5, "rotation": 90, "scaleY": 1.5 }
  ],
  "enemies": [
    { "type": "GazerEnemy", "startingX": 0, "startingY": 0 },
    { "type": "SkeletonArcherEnemy", "startingX": 5, "startingY": 0 }
  ],
  "exitWall": { "posX": 15, "posY": 0, "rotation": 0, "scaleY": 1.0  }
}
```

### Sobre las walls normales
- `{ "posX": -15, "posY": 0, "rotation": 0, "scaleY": 1.0 }`: Muralla vertical en la posición X = -15. Ocupa 10 bloques hacía arriba y 10 hacía abajo.
- `{ "posX": 0, "posY": -10, "rotation": 90, "scaleY": 1.5 }`: Muralla horizontal en la posición y = -10. Multiplicado por 1.5, es decir, con Bloques_actuales = 30. Con lo que ocupa 15 bloques a la izquierda y 15 a la derecha, con centro en x = 0.
- `{ "posX": 0, "posY": 10, "rotation": 90, "scaleY": 1.5 }`: Muralla horizontal en la posición y = 10. Multiplicado por 1.5, es decir, con Bloques_actuales = 30. Con lo que ocupa 15 bloques a la izquierda y 15 a la derecha, con centro en x = 0.

### Sobre exitWall
- `"exitWall": { "posX": 15, "posY": 0, "rotation": 0 }`: Muralla de salida vertical en x = 15 y centro en y = 0. Tamaño de 20 bloques (10 por lado).
