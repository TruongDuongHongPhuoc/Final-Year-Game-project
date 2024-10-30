using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour, I_Interact
{
    public void interact(GameObject other)
    {
       PlayerCondition player = other.GetComponent<PlayerController>().playerCondition;
        player.CurrentCharacter.useUnit.CurrentHeal = player.CurrentCharacter.useUnit.MaxHeal;
        player.CurrentCharacter.useUnit.Shield = player.CurrentCharacter.useUnit.MaxShield;
        player.CurrentCharacter.useUnit.currentEffect = null;
        Faded.Instance.FadedInTrue(0.5f);
        MainMenu.Instance.ActiveSaveMenu();
    }
}
