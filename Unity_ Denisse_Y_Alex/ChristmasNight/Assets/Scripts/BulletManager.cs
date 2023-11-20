using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importa el namespace para Text Mesh Pro

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance; // Singleton para acceder a la instancia desde otros scripts

    // Referencia al objeto de texto Mesh Pro para mostrar el conteo de balas
    public TextMeshPro bulletCountTextMesh;

    // Conjunto para almacenar las referencias a las balas visibles
    private HashSet<Bullet> visibleBullets = new HashSet<Bullet>();

    void Awake()
    {
        // Implementación del patrón Singleton
        if (Instance == null)
        {
            Instance = this; // Si no hay otra instancia, esta se convierte en la única
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruye esta para evitar duplicados
        }
    }

    // Método para añadir una bala visible al conjunto y actualizar el contador
    public void AddVisibleBullet(Bullet bullet)
    {
        visibleBullets.Add(bullet); // Añade la bala al conjunto
        UpdateBulletCountDisplay(); // Actualiza el texto que muestra el conteo
    }

    // Método para eliminar una bala visible del conjunto y actualizar el contador
    public void RemoveVisibleBullet(Bullet bullet)
    {
        visibleBullets.Remove(bullet); // Elimina la bala del conjunto
        UpdateBulletCountDisplay(); // Actualiza el texto que muestra el conteo
    }

    // Actualiza el texto en pantalla para mostrar el número actual de balas visibles
    private void UpdateBulletCountDisplay()
    {
        if (bulletCountTextMesh != null) // Verifica que haya una referencia al texto
        {
            bulletCountTextMesh.text = "Bullet Count: " + visibleBullets.Count; // Actualiza el texto
        }
    }
}
