**Cada wall mide 0.94x20 bloques. Sin rotar está en vertical. Rotar por 90 para que esté en horizontal**
**Para hacer más pequeño o grande modificar el valor de scaleY calculandolo con: Bloques_actuales/20**
**Las wall aparecen con su centro como punto de origen, por lo que tendrán Bloques_actuales/40 bloques a cada lado**

**Lo mismo aplica para los endWall**

**Es muy importante actualizar el Starting_X y Starting_Y para el player al cargar otro nivel**

**Todo está en base al origen 0,0. En el editor es donde está el GlobalVolume**



ej.
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
  "endWall": { "posX": 15, "posY": 0, "rotation": 0, "scaleY": 1.0  }
}
```

Sobre las walls:
- `{ "posX": -15, "posY": 0, "rotation": 0, "scaleY": 1.0 }`: Muralla vertical en la posición X = -15. Ocupa 10 bloques hacía arriba y 10 hacía abajo.
- `{ "posX": 0, "posY": -10, "rotation": 90, "scaleY": 1.5 }`: Muralla horizontal en la posición y = -10. Multiplicado por 1.5, es decir, con Bloques_actuales = 30. Con lo que ocupa 15 bloques a la izquierda y 15 a la derecha, con centro en x = 0.
- `{ "posX": 0, "posY": 10, "rotation": 90, "scaleY": 1.5 }`: Muralla horizontal en la posición y = 10. Multiplicado por 1.5, es decir, con Bloques_actuales = 30. Con lo que ocupa 15 bloques a la izquierda y 15 a la derecha, con centro en x = 0.


Sobre endWall:
- `"endWall": { "posX": 15, "posY": 0, "rotation": 0 }`: Muralla de salida vertical en x = 15 y centro en y = 0. Tamaño de 20 bloques (10 por lado).
