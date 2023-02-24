using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSystem : MonoSingleton<FinishSystem>
{
    [Header("Finish_Field")]
    [Space(10)]

    private int freefield;

    public void FinishCheck()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && Check())
            FinishTime();
    }

    public void FinishTime()
    {
        GameManager gameManager = GameManager.Instance;
        Buttons buttons = Buttons.Instance;
        MoneySystem moneySystem = MoneySystem.Instance;
        if (gameManager.level % 10 == 0)
            StartCoroutine(BarSystem.Instance.BarImageFillAmountIenum());
        LevelManager.Instance.LevelCheck();
        buttons.winPanel.SetActive(true);
        if (gameManager.level % 10 == 0)
            buttons.barPanel.SetActive(true);
        buttons.finishGameMoneyText.text = moneySystem.NumberTextRevork(gameManager.addedMoney);
        gameManager.gameStat = GameManager.GameStat.finish;
        moneySystem.MoneyTextRevork(gameManager.addedMoney);
    }

    private bool Check()
    {
        bool isFinish = true;

        for (int i1 = 0; i1 < 6; i1++)
            for (int i2 = 0; i2 < 6; i2++)
                if (!GridSystem.Instance.gridSystemField.gridBool[i1, i2]) isFinish = false;
        return isFinish;
    }
}
