using UnityEngine;
using System.Collections.Generic;

public class Menu : SingletonMonoBehaviour<Menu>
{
    [SerializeField] private List<GameObject> screens = new List<GameObject>();

    private void Awake()
    {
        HideAll();
    }

    public void HideAll()
    {
        screens.ForEach(_ => _.SetActive(false));
    }

    public void ShowScreen<T>(bool popup = false) where T : Component
    {
        var screen = screens.Find(_ => _.GetComponent<T>());
        if (screen)
        {
            if (popup)
            {
                screen.SetActive(true);
            }
            else
            {
                for (int i = 0; i < screens.Count; i++)
                {
                    var scr = screens[i];
                    scr.SetActive(scr == screen);
                }
            }
        }
        else
        {
            Debug.LogErrorFormat("Can't find screen = {0}!", typeof(T));
        }
    }
}
