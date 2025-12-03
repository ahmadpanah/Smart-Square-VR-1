using UnityEngine;

public class groundSpawner : MonoBehaviour
{
    public float maxTime = 1;
    private float timer = 0;
    public GameObject[] ground;
    void Start()
    {
        GameObject newground = Instantiate(ground[UnityEngine.Random.Range(0, ground.Length)]);
        newground.transform.position = transform.position + new Vector3(0, 0, 0);
    }

    
    void Update()
    {
        //Creating ground over time
        if (timer > maxTime)
        {
            GameObject newground = Instantiate((ground[UnityEngine.Random.Range(0, ground.Length)]));
            newground.transform.position = transform.position + new Vector3(0, 0, 0);
            Destroy(newground, 18);
            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
