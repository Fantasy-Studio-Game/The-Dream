using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    [SerializeField] GameObject heartPrefab;
    [SerializeField] GameObject emptyHeartPrefab;
    [SerializeField] public static HeartSystem instance { get; set; }
    private void Awake()
    {
        instance = this;
    }
    public void SetValue(int hearts, int maxHearts)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < maxHearts; i++)
        {
            if (i + 1 <= hearts)
            {
                GameObject heart = Instantiate(heartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
            }
            else
            {
                GameObject heart = Instantiate(emptyHeartPrefab, transform.position, Quaternion.identity);
                heart.transform.SetParent(transform);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
