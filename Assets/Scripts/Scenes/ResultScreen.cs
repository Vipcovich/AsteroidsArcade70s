using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultScreen : MonoBehaviour
{
    [SerializeField] private KeyCode nextScreen = KeyCode.KeypadEnter;
    [SerializeField] private Text score;

    private void OnEnable()
    {
        score.text = string.Format("Result: {0}", GameState.instance.Score.ToString());
    }

    private void Update()
    {
        if (Input.GetKey(nextScreen))
        {
            GameState.instance.SetState(GameState.State.OnStart);
        }
    }
}
