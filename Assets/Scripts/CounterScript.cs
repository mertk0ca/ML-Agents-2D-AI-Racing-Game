using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class CounterScript : MonoBehaviour
{
    public static int succeed = 0;
    public static int failed = 0;
    public static int overall = 0;
    public static int percent = 0;

    public Text SucceedText;
    public Text FailedText;
    public Text OverallText;
    public Text PercentText;
    public Text CheckpointText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Target target))//agent odule ulasýrsa
        {
            succeed += 1;
        }

        else if (collision.TryGetComponent(out Wall wall))//agent cezaya ulasýrsa
        {
            failed += 1;
        }

        overallTextFunction();
    }

    public void overallTextFunction()
    {
        overall = succeed - failed;

        percent = (succeed * 100) / (failed+succeed) ;

        SucceedText.text = succeed.ToString();
        FailedText.text = failed.ToString();
        PercentText.text = "%" + percent.ToString();
        OverallText.text = overall.ToString();

        if (overall > 0)
        {
            OverallText.color = Color.green;
        }
        else
        {
            OverallText.color = Color.red;
        }
    }
}
