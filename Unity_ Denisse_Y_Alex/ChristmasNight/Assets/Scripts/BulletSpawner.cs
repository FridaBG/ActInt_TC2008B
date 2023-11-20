using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    // Define los tipos de spawner: Disparo directo o giratorio
    enum SpawnerType { Straight, Spin }

    [Header("Bullet Attributes")]
    public GameObject bullet; // Prefab de la bala
    public float bulletLife = 1f; // Tiempo de vida de cada bala
    public float speed = 1f; // Velocidad de las balas

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType; // Tipo actual del spawner
    [SerializeField] private float firingRate = 1f; // Tasa de disparo (balas por segundo)

    private GameObject spawnedBullet; // Referencia a la última bala generada
    private float timer = 0f; // Temporizador para controlar la tasa de disparo

    // Temporizador y configuración para el cambio de patrones de disparo
    private float patternChangeTimer = 0f;
    private float patternChangeInterval = 15f; // Intervalo para cambiar los patrones de disparo
    private int currentPatternIndex = 0; // Índice del patrón actual
    private BulletPattern[] availablePatterns = {BulletPattern.Coreo3, BulletPattern.Coreo2, BulletPattern.Coreo1 };

    void Start()
    {
        // Aquí puedes inicializar cualquier configuración si es necesario
    }

    // Update se llama una vez por frame
    void Update()
    {
        // Incrementa el temporizador en cada frame
        timer += Time.deltaTime;

        // Si el spawner está configurado para girar, rota el spawner
        if(spawnerType == SpawnerType.Spin) 
            transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 1f);

        // Comprueba si es momento de disparar una nueva bala
        if(timer >= firingRate) 
        {
            Fire();
            timer = 0;
        }

        // Maneja el cambio de patrones de disparo
        patternChangeTimer += Time.deltaTime;
        if (patternChangeTimer >= patternChangeInterval) 
        {
            patternChangeTimer = 0f;
            currentPatternIndex = (currentPatternIndex + 1) % availablePatterns.Length;
        }
    }

   // Método para disparar una nueva bala
   private void Fire() 
   {
        if(bullet) 
        {
            // Crea una nueva bala y configura sus atributos
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<Bullet>().speed = speed;
            spawnedBullet.GetComponent<Bullet>().bulletLife = bulletLife;
            spawnedBullet.GetComponent<Bullet>().pattern = availablePatterns[currentPatternIndex];
        }
    }
}
