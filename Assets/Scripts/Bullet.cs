using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 2f;

    Renderer b_renderer;
    void Start()
    {
        b_renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(0f, 1f);
        transform.Translate(bulletSpeed * Time.deltaTime * direction);
        if (!b_renderer.isVisible)
        {
            Destroy(gameObject);
        }
    }


}
