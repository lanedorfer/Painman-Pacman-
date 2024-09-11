using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPallet : Pallet
{
    public float duration = 8.0f;
    protected override void Eat()
    {
        FindObjectOfType<GameManager>().BigPalletEaten(this);
    }
}
