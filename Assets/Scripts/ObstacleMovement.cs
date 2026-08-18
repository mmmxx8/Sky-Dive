using Unity.VisualScripting;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float ychange;

    void Update()
    {

        transform.position += new Vector3(0, ychange*Time.deltaTime, 0);
        if (transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }
}
