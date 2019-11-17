using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    [SerializeField] private Button _reload;
    // Start is called before the first frame update
    void Start()
    {
        _reload.onClick.AddListener(Reload);
    }

    private void Reload()
    {
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    private void OnDestroy()
    {
        _reload.onClick.AddListener(Reload);
    }
}
