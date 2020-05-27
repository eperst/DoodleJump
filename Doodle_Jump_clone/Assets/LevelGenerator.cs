using System.CodeDom.Compiler;
using System.Numerics;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject movingplatformPrefab;
    [SerializeField] private GameObject displatformPrefab;
    [SerializeField] private GameObject springPrefab;
    [SerializeField] private GameObject jetpackPrefab;
    private float dY_Max;
    private float dY_Min;
    private float dX_Max;
    private float dX_Min;
    private float bHeight = 10.0f;
    private Score score;
    void Awake()
    {
        score = GameObject.Find("Text").GetComponent<Score>();
        generateNStretch(-2.0f, 8.0f);
        generateNStretch(5.0f, bHeight);
    }
    private void genDeltas()
    {
        dY_Min = ((score.S / 250) * 0.2f < 1.65f) ? (score.S / 250) * 0.2f + 0.5f : 2.15f;
        dX_Min = ((score.S / 250) * 0.2f < 3.55f) ? (score.S / 250) * 0.2f + 1.0f : 4.55f;
        dY_Max = (2.5f * dY_Min < 2.2f) ? 2.5f * dY_Min : 2.2f;
        dX_Max = (2.5f * dX_Min < 4.6f) ? 2.5f * dX_Min : 4.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= background.transform.position.y + 5.0f) {
            transform.position = new UnityEngine.Vector3(0.0f, transform.position.y + bHeight, 12.0f);
            if (Random.Range(0.0f, 1.0f) < 0.6f)
                generateNStretch(transform.position.y, bHeight);
            else
                generateDStretch(transform.position.y, bHeight);
        }
    }
    

    void generateDStretch(float start, float height)
    {
        genDeltas();
        float i = start;
       // Debug.Log("X range: " + (1.0f + checked_dX).ToString() + " " + (4.6f).ToString());
        //Debug.Log("Y range: " + (0.5f + checked_dY).ToString() + " " + (2.2).ToString());
        while (i < start + height) //step is 0.5 min
        {
            float offset = UnityEngine.Random.Range(0.0f, 4.6f);
            while (offset < 4.6f)
            {
                UnityEngine.Vector3 position = new UnityEngine.Vector3(-2.3f + offset, i, 0.0f);
                generateDisP(position);
                offset += UnityEngine.Random.Range(dX_Min, dX_Max);

            }
            i += UnityEngine.Random.Range(dY_Min, dY_Max);
        }
    }

    void generateNStretch(float start, float height)
    {
        genDeltas();
        //Debug.Log("X range: " + (1.0f + checked_dX).ToString() + " " + (4.6f).ToString());
        //Debug.Log("Y range: " + (0.5f + checked_dY).ToString() + " " + (2.2).ToString());
        float i = start;
        bool hasPlatform;
        while (i < start + height - 0.5f) //step is 0.5 min
        {
            hasPlatform = false;
            float offset = UnityEngine.Random.Range(0.0f, 4.6f);
            while (offset < 4.6f)
            {
                UnityEngine.Vector3 position = new UnityEngine.Vector3(-2.3f + offset, i, 0.0f);
                if (Random.Range(0.0f, 1.0f) < 0.8f)
                {
                    generateBasicP(position);
                    hasPlatform = true;
                    offset += UnityEngine.Random.Range(dX_Min, dX_Max);
                }
                else if (!hasPlatform)
                {
                    generateMovingP(position);
                    offset += 5.0f;
                }
            }
            i += UnityEngine.Random.Range(dY_Min, dY_Max);
        }
    }
    void generateExtra(GameObject p)
    {
        if (Random.Range(0.0f, 1.0f) < 0.2f)
        {
            UnityEngine.Vector3 springOffset = new UnityEngine.Vector3(0.0f, 0.2f, -2.0f); //Random?
            GameObject spring = Instantiate(springPrefab, p.transform.position + springOffset, UnityEngine.Quaternion.identity);
            spring.transform.parent = p.transform;
        }
        else if (Random.Range(0.0f, 1.0f) < 0.05f)
        {
            UnityEngine.Vector3 jetpackOffset = new UnityEngine.Vector3(0.0f, 0.4f, -2.0f); //Random?
            GameObject jetpack = Instantiate(jetpackPrefab, p.transform.position + jetpackOffset, UnityEngine.Quaternion.identity);
            jetpack.transform.parent = p.transform;
        }
    }

    void generateBasicP(UnityEngine.Vector3 position)
    {
        GameObject basic = Instantiate(platformPrefab, position, UnityEngine.Quaternion.identity);
        generateExtra(basic);
    }
    void generateMovingP(UnityEngine.Vector3 position)
    {
        GameObject moving = Instantiate(movingplatformPrefab, position, UnityEngine.Quaternion.identity);
        generateExtra(moving);

    }

    void generateDisP(UnityEngine.Vector3 position)
    {
        Instantiate(displatformPrefab, position, UnityEngine.Quaternion.identity);
    }
}