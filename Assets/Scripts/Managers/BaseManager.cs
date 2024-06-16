using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    protected CharacterObject Player;
    protected List<CharacterObject> Enemies;

    protected RuntimeDeckManager runtimeDeckManager;

    public virtual void Initialize(CharacterObject player, List<CharacterObject> enemies, RuntimeDeckManager deck)
    {
        Player = player;
        Enemies = enemies;
        runtimeDeckManager = deck;
    }

    public virtual void Initialize(CharacterObject player, List<CharacterObject> enemies)
    {
        Player = player;
        Enemies = enemies;
    }
}
