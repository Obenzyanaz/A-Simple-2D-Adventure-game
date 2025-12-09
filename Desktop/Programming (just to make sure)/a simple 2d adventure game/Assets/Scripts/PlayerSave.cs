using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
        public int level = 1;
        public int health = 20;

    public void ChangeLevel (int amount)
    {
        level += amount;
    }

    public void ChangeHealth (int amount)
    {
        health += amount;
    }
}
