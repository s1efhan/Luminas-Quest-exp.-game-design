using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int collectedCount = 0;
public TMPro.TextMeshProUGUI countText;

public void IncreaseCount()
{
    collectedCount++;
    UpdateCountText();
}

void UpdateCountText()
{
    countText.text = "Collected: " + collectedCount.ToString();
}

}
