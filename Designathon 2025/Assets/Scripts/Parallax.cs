using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Vector2 speed = new Vector2(1f, 1f);

    void Update()
    {
        transform.Translate(speed * Time.deltaTime);

        Vector3 pos = transform.position;
        if (pos.x > 2.5f) pos.x = -2.5f;
        if (pos.y > 2.5f) pos.y = -2.5f;
        transform.position = pos;
    }
}
