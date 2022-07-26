using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    abstract public void onCast(Transform player);
}
