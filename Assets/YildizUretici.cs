using UnityEngine;

public class YildizUretici : MonoBehaviour
{
    [SerializeField] private GameObject yildizPrefab;
    [SerializeField] private float uretimSikligi = 1.5f;
    [SerializeField] private float minX = -8f;
    [SerializeField] private float maxX = 8f;
    [SerializeField] private float spawnY = 6f;

    [Header("Farklı Görseller (Sprites)")]
    [SerializeField] private Sprite puanYildiziSprite; 
    [SerializeField] private Sprite dusmanSprite;       

    public static bool oyunBitti = false;

    void Start()
    {
        oyunBitti = false;
        InvokeRepeating("NesneOlustur", 1f, uretimSikligi);
    }

    void NesneOlustur()
    {
        if (oyunBitti) return; // Oyun bittiyse üretme

        if (yildizPrefab != null)
        {
            float rastgeleX = Random.Range(minX, maxX);
            Vector2 spawnPozisyonu = new Vector2(rastgeleX, spawnY);

            GameObject yeniNesne = Instantiate(yildizPrefab, spawnPozisyonu, Quaternion.identity);

            YildizDusur yildizScripti = yeniNesne.GetComponent<YildizDusur>();
            SpriteRenderer spriteRend = yeniNesne.GetComponent<SpriteRenderer>();

            int rastgeleSecim = Random.Range(0, 10);

            if (rastgeleSecim < 4) // Düşman / Engel
            {
                if (yildizScripti != null) yildizScripti.nesneTuru = YildizDusur.Tur.DusmanEngel;

                // Düşman görselini ve rengini ayarla
                if (spriteRend != null)
                {
                    if (dusmanSprite != null) spriteRend.sprite = dusmanSprite;
                    spriteRend.color = Color.white; // Görselin kendi renkleri bozulmasın diye beyaz yapıyoruz
                }
            }
            else // Normal Yıldız
            {
                if (yildizScripti != null) yildizScripti.nesneTuru = YildizDusur.Tur.PuanYildizi;

                // Yıldız görselini ve rengini ayarla
                if (spriteRend != null)
                {
                    if (puanYildiziSprite != null) spriteRend.sprite = puanYildiziSprite;
                    spriteRend.color = Color.white; // Görselin kendi renkleri bozulmasın diye beyaz yapıyoruz
                }
            }
        }
    }
}