using UnityEngine;

public class YildizDusur : MonoBehaviour
{
    [SerializeField] private float dusmeHizi = 3f;

    // Nesnenin türü: Puan yıldızı mı yoksa düşman/engel mi?
    public enum Tur { PuanYildizi, DusmanEngel }
    [Header("Nesne Türü")]
    public Tur nesneTuru = Tur.PuanYildizi;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * dusmeHizi * Time.deltaTime);

        // Ekranın altına düştüğünde
        if (transform.position.y < -6f)
        {
            // İSTERSEN DÜŞMANLARI KAÇIRINCA DA YOK ETMEK İÇİN BURAYA DOKUNMUYORUZ.
            // Yıldız kaçınca artık can azalmayacak, sadece objeyi siliyoruz.
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D diger)
    {
        // Temas ettiğimiz nesne "Sepet" ise
        if (diger.gameObject.name == "Sepet")
        {
            if (GameManager.Instance != null)
            {
                if (nesneTuru == Tur.PuanYildizi)
                {
                    GameManager.Instance.PuanArttir(); // Sadece yıldızı yakalarsan puan alırsın
                }
                else if (nesneTuru == Tur.DusmanEngel)
                {
                    GameManager.Instance.CanAzalt(); // Kötü nesneye (düşmana) çarparsan canın gider
                }
            }

            // Yıldızı/Düşmanı sahneden yok et
            Destroy(gameObject);
        }
    }
}