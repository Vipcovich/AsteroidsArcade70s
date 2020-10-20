using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HudScreen : MonoBehaviour
{
    [SerializeField] private Text score;
    [SerializeField] private HorizontalLayoutGroup lifes;
    [Space]
    [SerializeField] private KeyCode    pauseKeyCode = KeyCode.Escape;
    [SerializeField] private GameObject pauseFrame;

    private void OnEnable()
    {
        pauseFrame.SetActive(false); 
    }

    private void Update()
    {
        score.text = GameState.instance.Score.ToString("D5");
        int lifesVal = GameState.instance.Lifes;

        for (int i = 0; i < Mathf.Max(lifesVal, lifes.transform.childCount); i++)
        {
            if (i >= lifes.transform.childCount)
            {
                var prefab = lifes.transform.GetChild(0);
                GameObject.Instantiate(prefab, lifes.transform);
            }

            lifes.transform.GetChild(i).gameObject.SetActive(i < lifesVal);
        }

        if (Input.GetKey(pauseKeyCode))
        {
            Time.timeScale = (Time.timeScale > 0f) ? 0f : 1f;
            pauseFrame.SetActive(!(Time.timeScale > 0f));
        }
    }
}
