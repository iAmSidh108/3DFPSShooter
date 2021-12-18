using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles the movement of the player with given input from the input manager
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The speed at which player moves")]
    public float moveSpeed = 2f;
    [Tooltip("The speed at which player looks around(Calculated in degrees)")]
    public float lookSpeed = 60f;
    [Tooltip("The power with which player jumps")]
    public float jumpPower=8f;
    [Tooltip("The strength of gravity")]
    public float gravity = 9.81f;

    [Header("Required Preferences")]
    [Tooltip("The player shooter script that fires projectiles.")]
    public Shooter playerShooter;

    /// <summary>
    /// Description:
    /// Standard Unity function called once before the first Update call
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Description:
    /// Standard Unity function called once every frame
    /// Input:
    /// none
    /// Return:
    /// void (no return)
    /// </summary>
    void Update()
    {

    }
}
