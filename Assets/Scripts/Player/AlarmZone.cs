using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AlarmZone : MonoBehaviour
{
    public event Action<bool> PlayerIsNoticed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out Player player))
        {
            PlayerIsNoticed?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent(out Player player))
        {
            PlayerIsNoticed?.Invoke(false);
        }
    }
}
