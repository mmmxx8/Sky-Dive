using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float ychange;
    void Update()
    {
        transform.position += new Vector3(0, ychange * Time.deltaTime, 0);
        if (transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }
    public void Init(float speed)
    {
        ychange = speed;
    }
}
