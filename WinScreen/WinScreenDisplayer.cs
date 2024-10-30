using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreenDisplayer : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI ExpText;
    [SerializeField] public TextMeshProUGUI MoneyText;
    [SerializeField] public TextMeshProUGUI CountDownText;
    [SerializeField] public GameObject container;
    [SerializeField] GameObject basicUIRewardItemPrefab;
    private List<ItemBase> ItemRewards;
    public void DisplayWinSceen(){
       StartIncreaseEXP();
       StartIncreaseMoney();
       StartCoroutine(Countdown());
       SetListReward();
    }

    public void StartIncreaseEXP(){
        StartCoroutine(StartIncrease(0,  Sender.enemyBattling.additionExp, ExpText));
    }
    public void StartIncreaseMoney(){
        StartCoroutine(StartIncrease(0,  Sender.enemyBattling.AdditionMoney, MoneyText));
    }
    IEnumerator Countdown(){
        int count = 5;
        while(count > 0)
        {
        yield return new WaitForSecondsRealtime(1f);
        count --;
        CountDownText.text = count.ToString();
        if(count == 0){
            this.gameObject.SetActive(false);
        }
        }
    }
    IEnumerator StartIncrease( int amount, int FinalInt, TextMeshProUGUI updateText){
        while(amount < FinalInt){
            yield return new WaitForSeconds(0.01f);
            amount++;
            updateText.text = amount.ToString();
        }
    }
    IEnumerator SpawnItemImage(List<ItemBase> itemBases){
        int count =0;
        while(count < itemBases.Count){
            yield return new WaitForSecondsRealtime(1f);
            // Instantiate(itemBases[count].displayImg,container.transform);
            GameObject spawned = Instantiate(basicUIRewardItemPrefab,container.transform);
            spawned.GetComponentInChildren<Image>().sprite = itemBases[count].displayImg;
            count++;
        }
    }
    public void SetListReward(){
        this.ItemRewards = Sender.enemyBattling.ItemReward;
       StartCoroutine(SpawnItemImage(ItemRewards));
    }
}
