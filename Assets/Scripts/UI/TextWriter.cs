using UnityEngine;
using TMPro;


// usado para mostrar los mensajes de arriba del jugador
public class TextWriter : MonoBehaviour
{
    [SerializeField] PlayerPhysicsComponent physics;
    [SerializeField] private TMP_Text textMesh;

    private void Start()
    {
        SetNewText("Presiona 'espacio' para\ncambiar el color!");

        GlobalListener.Instance.OnGameWonGlobal += GameWonText;
        GlobalListener.Instance.OnGameLostGlobal += GameLostText;
    }

    private void GameWonText()
    {
        string text = "Felicitaciones, Ganaste!";
        SetNewText(text);
    }

    private void GameLostText()
    {
        string text = "Perdiste!";
        SetNewText(text);
    }

    private void SetNewText(string text)
    {
        Debug.Log(text);
        textMesh.text = text;
    }
}