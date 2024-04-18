using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShopCustomer
{
    void mejoraComprada(Mejoras.tipoMejora tipoMejora, int rareza);

    bool trySpendGoldAmount(int goldAmount);
}
