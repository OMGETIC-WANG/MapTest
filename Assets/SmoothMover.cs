using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmoothMover
{
    public Transform transform;
    public Queue<Vector3> destQue;
    public float speed;
    public SmoothMover(Transform transform, float speed)
    {
        this.transform = transform;
        destQue = new Queue<Vector3>();
        this.speed = speed;
    }
    public SmoothMover(Transform transform, float speed, IEnumerable<Vector3> dests)
    {
        this.transform = transform;
        destQue = new Queue<Vector3>(dests);
        this.speed = speed;
    }
    public void OnUpdate()
    {
        if (destQue.Count == 0)
        {
            return;
        }
        Vector3 dest = destQue.First();
        float dis = speed * Time.deltaTime;
        if (Vector3.Distance(dest, transform.position) <= dis)
        {
            transform.position = dest;
            destQue.Dequeue();
        }
        else
        {
            transform.position += Vector3.Normalize(dest - transform.position) * dis;
        }
    }
    public void AddDestination(Vector3 pos)
    {
        destQue.Enqueue(pos);
    }
}