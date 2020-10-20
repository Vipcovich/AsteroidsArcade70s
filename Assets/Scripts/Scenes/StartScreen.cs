using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private KeyCode nextScreen = KeyCode.Return;

    private void Update()
    {
        if (Input.GetKey(nextScreen))
        {
            GameState.instance.SetState(GameState.State.Gameplay);
        }
    }
}
