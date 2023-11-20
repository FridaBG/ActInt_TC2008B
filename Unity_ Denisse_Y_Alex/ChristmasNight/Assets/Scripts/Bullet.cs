using System.Collections;
using UnityEngine;

public enum BulletPattern { Simple, Coreo1, Coreo2, Coreo3 }

public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f; // Tiempo de vida de la bala
    public float speed = 1f; // Velocidad de la bala
    private Vector3 spawnPoint; // Punto de origen de la bala
    private float timer = 0f; // Temporizador para rastrear la vida de la bala

    public BulletPattern pattern; // Patrón actual de movimiento de la bala

    void Start()
    {
        // Establece el punto de origen de la bala
        spawnPoint = transform.position;
    }

    // Función para el movimiento directo
    private Vector3 Movement(float timer) 
    {
        float forwardSpeed = 10f; // Velocidad de avance
        // Calcula la nueva posición basada en el tiempo y la velocidad de avance
        return spawnPoint + new Vector3(0, 0, timer * forwardSpeed);
    }

    // Función para el patrón de movimiento Lemniscate (figura de ocho)
    private Vector3 Coreo1 (float timer) 
    {
        float forwardSpeed = 3f;
        float scale = 4f;
        float frequency = 3f;
        float offsetRadians = Mathf.PI / 360;

        float angle = frequency * timer - offsetRadians;
        float lemniscate = Mathf.Cos(angle) / (1 + Mathf.Pow(Mathf.Sin(angle), 2));
        
        float x = scale * lemniscate * Mathf.Cos(angle);
        float y = scale * lemniscate * Mathf.Sin(angle);
        float z = timer * forwardSpeed;

        return spawnPoint + new Vector3(x, y, z);
    }

    // Función para el patrón de movimiento Lissajous 3D
    private Vector3 Coreo2 (float timer) 
    {
        float A = 2f; // Amplitud para el eje X
        float B = 3f; // Amplitud para el eje Y
        float forwardSpeed = 3f;

        float a = 1f; // Frecuencia para el eje X
        float b = 3f; // Frecuencia para el eje Y
        float delta = Mathf.PI / 2;

        float x = A * Mathf.Sin(a * timer + delta);
        float y = B * Mathf.Sin(b * timer);
        float z = timer * forwardSpeed + 1f * Mathf.Sin(3f * timer); // Agregado movimiento en Z

        return spawnPoint + new Vector3(x, y, z);
    }

    // Función para el patrón de movimiento en forma de corazón
    private Vector3 Coreo3 (float timer) 
    {
        float scale = 0.2f;
        float forwardSpeed = 2f;
        float t = timer * 5f;

        float x = scale * (16 * Mathf.Pow(Mathf.Sin(t), 3));
        float y = scale * (13 * Mathf.Cos(t) - 5 * Mathf.Cos(2 * t) - 2 * Mathf.Cos(3 * t) - Mathf.Cos(4 * t));
        float z = timer * forwardSpeed;

        return spawnPoint + new Vector3(x, y, z);
    }

    void Update() 
    {
        if (timer > bulletLife) 
        {
            Destroy(gameObject); // Destruye la bala cuando supera su tiempo de vida
        }
        timer += Time.deltaTime;

        // Actualiza la posición de la bala según el patrón de movimiento
        switch (pattern) 
        {
            case BulletPattern.Simple:
                transform.position = Movement(timer);
                break;
            case BulletPattern.Coreo3:
                transform.position = Coreo3 (timer);
                break;
            case BulletPattern.Coreo2:
                transform.position = Coreo2 (timer);
                break;
            case BulletPattern.Coreo1:
                transform.position = Coreo1 (timer);
                break;
        }

        // Gestiona la visibilidad de la bala en relación con la cámara principal
        if (IsVisibleFrom(Camera.main))
        {
            BulletManager.Instance.AddVisibleBullet(this);
        }
        else
        {
            BulletManager.Instance.RemoveVisibleBullet(this);
        }
    }

    // Verifica si la bala está dentro del frustum de la cámara
    bool IsVisibleFrom(Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, new Bounds(transform.position, Vector3.one * 0.1f));
    }
}