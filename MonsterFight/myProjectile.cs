using UnityEngine;

public class myProjectile : MonoBehaviour
{
    Vector3 target;
    public GameObject projectileExplosion;
    float timeLeft = 1.2f;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("morko").transform.position;
        target.y += 0.5f;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target, 3 * Time.deltaTime);
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            GameObject explosion = Instantiate(projectileExplosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.5f);
        }
    }
}
