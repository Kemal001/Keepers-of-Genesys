using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiver : MonoBehaviour
{
    public int numberOfArrowsToDrop;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            numberOfArrowsToDrop = Random.Range(1, 5);

            if (ArrowShooting.instance.currentNumberOfArrows < 10)
            {
                ArrowShooting.instance.currentNumberOfArrows += numberOfArrowsToDrop;
            }
            
            Destroy(gameObject);
        }
    }
}
