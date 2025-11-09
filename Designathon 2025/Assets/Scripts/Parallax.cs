using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector2 speed = new Vector2(1f, 1f);
    public float x;
    public float y;
    public float xMove;
    public float yMove;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime);

        Vector3 pos = transform.position;
        if (pos.x > x) pos.x = xMove;
        if (pos.y > y) pos.y = yMove;
        transform.position = pos;
    }
}
