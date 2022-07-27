using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    public event Action ServerOnDie;

    #region Server

    public override void OnStartServer()
    {
        currentHealth = maxHealth;
    }

    [Server]
    public void DealDamage(int damageAmount)
    {
        if (currentHealth == 0) { return; } // already dead

        // if health after taking damage is less than 0, than just return 0
        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);

        if (currentHealth != 0) { return; }

        ServerOnDie?.Invoke();

        Debug.Log("We Died");
    }

    #endregion

    #region Client

    #endregion
}