using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Instanciator : MonoBehaviour
{
    // Start is called before the first frame update

    public static Instanciator Instance { get; private set; }

    public GameObject[] enemies;
    public Transform[] spawnPoints;

    public Text wave;
    public int waveCount;
    public Text count;


    void Start()
    {
        waveCount = 1;
        StartCoroutine(countDownForNextWave());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            waveCount++;
            StartCoroutine(countDownForNextWave());
        }
    }

    IEnumerator countDownForNextWave()
    {
        wave.text = "Next Wave Starts in...";
        count.text = "5";
        yield return new WaitForSeconds(1);
        count.text = "4";
        yield return new WaitForSeconds(1);
        count.text = "3";
        yield return new WaitForSeconds(1);
        count.text = "2";
        yield return new WaitForSeconds(1);
        count.text = "1";
        yield return new WaitForSeconds(1);
        wave.text = "Wave";
        count.text = waveCount.ToString();
        for (int i = 0; i < enemies.Length; i++) 
        {
            Instantiate(enemies[i], spawnPoints[i].position, Quaternion.identity);
        }
    }

}
