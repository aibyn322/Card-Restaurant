using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform[] seats;                 // empty objects marking where customers stand
    public float interval = 8f;

    Customer[] seated;

    void Start()
    {
        seated = new Customer[seats.Length];
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            Spawn();
        }
    }

    void Spawn()
    {
        for (int i = 0; i < seats.Length; i++)
        {
            if (seated[i] == null)            // an empty seat (destroyed customers count as null)
            {   
               var go = Instantiate(customerPrefab, seats[i]);
                go.transform.localPosition = Vector3.zero;

                var c = go.GetComponent<Customer>();
                if (c == null)
                {
                    Debug.LogError("The Customer Prefab has no Customer script on its root object!", customerPrefab);
                    Destroy(go);
                    return;
                }

                var orders = GameManager.Instance.UnlockedOrders;
                if (orders.Count == 0) return;
                c.Init(orders[Random.Range(0, orders.Count)]);

                seated[i] = c;
                return;
            }
        }
    }
}