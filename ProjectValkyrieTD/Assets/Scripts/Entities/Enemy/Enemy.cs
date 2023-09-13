using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform shootingPosition = default;
    public Transform ShootingPosition => shootingPosition;

    [SerializeField] private int expDrop = default;
    public int ExpDrop => expDrop;
}