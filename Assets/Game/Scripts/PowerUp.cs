using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = 2.5f;

    [SerializeField]
    private int powerUpID;

    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        CheckPos();
    }

    //ko se box collider dotakne plajerja preveri če je to res player, pridobi skripto kjer je 
    //shranjen startpoweruptimer in ga pokliče
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            //če smo skripto/component sploh našli potem nadaljujemo drugače lahko pride do crasha
            if (player != null)
            {

                if (powerUpID == 0)
                {
                    player.StartPowerUpTimer(0);
                    //če bi zagnali corutine iz tukaj in potem destroyali object se korutina ne bi izvedla
                }
                else if (powerUpID == 1)
                {
                    player.StartPowerUpTimer(1);
                }
                else if (powerUpID == 2)
                {
                    player.StartPowerUpTimer(2);
                }
                
            }

            Destroy(gameObject);
        }

    }

    private void CheckPos()
    {
        if (this.gameObject.transform.position.y < -6.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
