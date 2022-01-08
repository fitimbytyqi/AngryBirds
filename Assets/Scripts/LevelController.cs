using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Monster[] _monsters;

    [SerializeField] String _nextLevelName;

    void OnEnable()
    {
        _monsters = FindObjectsOfType<Monster>();    
    }

    void Update()
    {
        if (MonstersAreAllDead())
        {
            GoToNextLevel();
        }    
    }

    void GoToNextLevel()
    {
        SceneManager.LoadScene(_nextLevelName);
    }

    bool MonstersAreAllDead()
    {
        foreach (Monster monster in _monsters)
        {
            if (monster.gameObject.activeSelf)
                return false;
        }

        return true;
    }
}
