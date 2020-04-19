using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string GetEnemy();
    void OnActivate();
    string GetDesc();
}
