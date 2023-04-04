using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastDamage : MonoBehaviour
{
    private List<GameObject> explosionRadius = new List<GameObject>();
    [SerializeField] private GameObject rocket;
    private bool isExploding;

    private void Start()
    {
        isExploding = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.name.StartsWith("Hitbox") || collision.name.StartsWith("Crate")) && !isExploding)
        {
            foreach (GameObject objectInRadius in explosionRadius)
            {
                if (objectInRadius == collision.gameObject)
                {
                    explosionRadius.Remove(collision.gameObject);
                    break;
                }
            }

            explosionRadius.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.name.StartsWith("Hitbox") || collision.name.StartsWith("Crate")) && !isExploding)
        {
            foreach (GameObject objectInRadius in explosionRadius)
            {
                if (objectInRadius == collision.gameObject)
                {
                    explosionRadius.Remove(collision.gameObject);
                    break;
                }
            }
        }
    }

    public void Explode(int dmg)
    {
        isExploding = true;

        foreach (GameObject objectInRadius in explosionRadius)
        {
            if (objectInRadius == null)
                explosionRadius.Remove(objectInRadius);
        }

        foreach (GameObject objectInRadius in explosionRadius)
        {
            if (objectInRadius.name.StartsWith("Hitbox"))
            {
                objectInRadius.gameObject.GetComponentInParent<HealthComponent>().TakeDamage(dmg);
            }

            if (objectInRadius.name.StartsWith("Crate"))
            {
                objectInRadius.gameObject.GetComponent<Crate>().TakeDamage(dmg);
            }
        }

        Destroy(rocket);
    }
}
