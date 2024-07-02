using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.UI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using JetBrains.Annotations;
using UnityEditor.Rendering;

public class MoveToTargetAgent : Agent
{
    public Text RewardText;

    public Text w;
    public Text a;
    public Text s;
    public Text d;

    [SerializeField] private Transform target; //private olmasina ragmen inspectordan degistirmek icin
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;

    public float distanceBetweenObjects;
    public float temp = 0;
    public float gettingCloser;

    public GameObject[] Targets;
    public static int i = 1;
    public static int counter = 1;

    public float timer;

    public float rewardCounter = 0;

    carController carController;//carController scriptini cagirmak icin carController adinda degisken atanir

    void Awake()
    {
        carController = GetComponent<carController>();//carController scriptine referans verme
    }

    public override void OnEpisodeBegin()//odul kazanmak veya kaybetmek icin episode baslatýlmalý odul alindiginda episode bitirilmeli
    {
        rewardCounter = 0;
    }
    public override void CollectObservations(VectorSensor sensor)//gozlem toplama
    {
        sensor.AddObservation((Vector2)transform.localPosition);//agent konumu
        sensor.AddObservation((Vector2)target.localPosition);//target konumu

        Vector3 dirToTarget = (target.transform.localPosition - transform.localPosition).normalized;
        sensor.AddObservation(dirToTarget);

    }

    public override void OnActionReceived(ActionBuffers actions)//agent hareketleri burada yapilir
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = actions.ContinuousActions[0];//ajanin x eksenindeki hareket inputVector un x bilesenine atanir
        inputVector.y = actions.ContinuousActions[1];//ajanin y eksenindeki hareket inputVector un y bilesenine atanir

        carController.SetInputVector(inputVector);//carController icindeki setInputVector fonksiyonuna yukarýda alinan inputlar gonderilir
    }

    public override void Heuristic(in ActionBuffers actionsOut)//agenti kontrol etmek icin
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;

        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Target target))//agent odule ulasýrsa
        {
            i++;
            target.transform.position = Targets[i - 1].transform.position;//targeti bir sonraki pozisyona tasima
            backgroundSpriteRenderer.color = Color.green;//arkaplani yesil yapar
            AddReward(100f);//odul kazanir
            rewardCounter += (100f);
            timer = 0;
            counter = 1;

            if (i == 333)
            {
                EndEpisode();
                i = 3;
                target.transform.position = Targets[i - 1].transform.position;
                transform.position = new Vector3(47, -53, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))//agent cezaya ulasýrsa
        {
            AddReward(-500f);//odul kaybeder
            rewardCounter -= 500f;
            backgroundSpriteRenderer.color = Color.red;//arkaplani kirmizi yapar
            if (i == 1)
            {
                //transform.localPosition = new Vector3(47f, -52f, 0f);
                //carController.carRigidbody2D.drag = 20;//agent hizini sifirlama
            }
            else
            {
                //transform.localPosition = Targets[i - 2].transform.position;
                //carController.carRigidbody2D.drag = 20;//agent hizini sifirlama
            }

        }

        transform.localPosition = Targets[i - 4].transform.position;
        carController.carRigidbody2D.drag = 20;
        AddReward(-3000f * counter);
        rewardCounter += (-3000f * counter);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Penalty penalty))//agent odule ulasýrsa
        {
            AddReward(-3f);
            rewardCounter -= (3f);
        }
    }

    public void Update()
    {
        distanceBetweenObjects = Vector3.Distance(transform.position, Targets[i - 1].transform.position);//agent ve target arasindaki mesafe
        Debug.DrawLine(transform.position, Targets[i - 1].transform.position, Color.green);//agent ve target arasina cizgi cekme
        //Debug.Log(distanceBetweenObjects);//target ajan arasindaki uzaklik
        gettingCloser = temp - distanceBetweenObjects;//agentin targete yaklasip yaklasmadigini olcer
        temp = distanceBetweenObjects;
        //Debug.Log(gettingCloser);//ajanin targeta yaklasip yaklasmadigi

        timer += Time.deltaTime;
        //Debug.Log(timer);//sure
        Debug.Log("Toplam Ödül: " + rewardCounter);

        if (timer > 20)
        {
            if (i == 1 && i == 2)
            {
                transform.localPosition = new Vector3(47f, -52f, 0f);
                carController.carRigidbody2D.drag = 20;
                AddReward(-1000f * counter);
                rewardCounter += (-1000f * counter);
                timer = 0;
                counter++;
                if (counter > 10)
                {
                    counter = 10;
                }
            }
            else
            {
                AddReward(-1000f * counter);
                rewardCounter += (-1000f * counter);
                timer = 0;
                transform.localPosition = Targets[i - 4].transform.position;
                counter++;
                if (counter > 10)
                {
                    counter = 10;
                }
            }
        }


        if (gettingCloser > 0)
        {
            AddReward(1f / distanceBetweenObjects);
            rewardCounter += (1f / distanceBetweenObjects);
        }
        else
        {
            AddReward(-3f * distanceBetweenObjects);
            rewardCounter -= (3f * distanceBetweenObjects);
        }


        /*if (carController.accelerationInput > 0)
        {
            AddReward(0.05f);
            rewardCounter += 0.05f;
        }
        else if (carController.velocityVsUp < 0)
        {
            AddReward(-0.1f);
            rewardCounter -= 0.1f;
        }*/


        /*if (Mathf.Abs(carController.turnInput) > 0)
        {
            AddReward(-0.05f);
            rewardCounter -= 0.05f;
        }*/

        AddReward(5f / distanceBetweenObjects);
        rewardCounter += (5f / distanceBetweenObjects);

        AddReward(-1f);
        rewardCounter -= 1f;

        /*if(gettingCloser < 0)
        {
            AddReward(-0.5f);
        }

        if (carController.velocityVsUp < 0)
        {
            AddReward(-0.1f * carController.velocityVsUp);
        }*/

        RewardText.text = "Reward:" + rewardCounter.ToString();

        showKeys();

    }

    public void showKeys()
    {
        if(carController.accelerationInput > 0)
        {
            w.color = Color.green;
        }
        else { w.color = Color.white; }

        if (carController.accelerationInput < 0)
        {
            s.color = Color.green;
        }
        else { s.color = Color.white; }

        if (carController.turnInput > 0)
        {
            d.color = Color.green;
        }
        else { d.color = Color.white; }

        if (carController.turnInput < 0)
        {
            a.color = Color.green;
        }
        else { a.color = Color.white; }
    }
}
