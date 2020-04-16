using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashController : MonoBehaviour
{
    private void FinishAnim()
    {
        Destroy(gameObject);
    }
}
