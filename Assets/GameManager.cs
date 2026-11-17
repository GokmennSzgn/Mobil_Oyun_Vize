using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TextMeshProUGUI skorText;
    [SerializeField] private TextMeshProUGUI canText; 
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject oyunAyarlarPanel;
    [SerializeField] private TextMeshProUGUI sesButonText;
    private bool oyunBitti = false;
    private TextMeshProUGUI tamEkranButonText;

    private bool sesDurumu = true;
    private bool tamEkranDurumu = true;
    private int skor = 0;
    private int can = 3; 
    //private bool oyunBitti = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SkoruGuncelle();
        CanGuncelle();

        CikisButonuOlustur();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false); 
    }

    private void CikisButonuOlustur()
    {
        if (gameOverPanel == null || gameOverPanel.transform.Find("OyunCikisButton") != null) return;

        Button kaynakButton = null;
        foreach (Button button in gameOverPanel.GetComponentsInChildren<Button>(true))
        {
            if (button.gameObject.name == "YenidenBaslatButton")
            {
                kaynakButton = button;
                break;
            }
        }

        if (kaynakButton == null) return;

        GameObject cikis = Instantiate(kaynakButton.gameObject, kaynakButton.transform.parent);
        cikis.name = "OyunCikisButton";

        RectTransform cikisTransform = cikis.GetComponent<RectTransform>();
        RectTransform kaynakTransform = kaynakButton.GetComponent<RectTransform>();
        cikisTransform.anchoredPosition = new Vector2(
            kaynakTransform.anchoredPosition.x,
            kaynakTransform.anchoredPosition.y - 48f);

        Button cikisButton = cikis.GetComponent<Button>();
        cikisButton.onClick.RemoveAllListeners();
        cikisButton.onClick.AddListener(OyundanCik);

        TextMeshProUGUI cikisYazisi = cikis.GetComponentInChildren<TextMeshProUGUI>(true);
        if (cikisYazisi != null) cikisYazisi.text = "Çıkış";
    }

    public void PuanArttir()
    {
        if (oyunBitti) return;

        skor += 10;
        SkoruGuncelle();
    }

    
    public void CanAzalt()
    {
        if (oyunBitti) return;

        can--;
        CanGuncelle();

        if (can <= 0)
        {
            OyunBitti();
        }
    }

    void SkoruGuncelle()
    {
        if (skorText != null)
            skorText.text = "Skor: " + skor;
    }

    void CanGuncelle()
    {
        if (canText != null)
            canText.text = "Can: " + can;
    }

    void OyunBitti()
    {
        oyunBitti = true;

        // Üretimi durdur
        YildizUretici.oyunBitti = true;

        // Ekrandaki tüm yıldız ve düşmanları yok et:
        YildizDusur[] aktifNesneler = FindObjectsByType<YildizDusur>(FindObjectsSortMode.None);
        foreach (YildizDusur nesne in aktifNesneler)
        {
            Destroy(nesne.gameObject);
        }

        // --- SEPETİ (OYUNCUYU) GİZLE ---
        GameObject sepet = GameObject.Find("Sepet");
        if (sepet != null)
        {
            sepet.SetActive(false); // Oyun bittiğinde sepet görünmez olur
        }

        Debug.Log("Oyun Bitti!");

        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void OyunuYenidenBaslat()
    {
        
        Time.timeScale = 1f;

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AnaMenuyeDon()
    {
        // Zamanı normal hızına getir (oyun durdurulduysa düzelmesi için)
        Time.timeScale = 1f;

        // Build Settings'e eklediğin ana menü sahnesinin tam adı
        SceneManager.LoadScene("GirisSahnesi");
    }

    public void OyundanCik()
    {
        Debug.Log("Oyun kapatıldı!");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void AyarlarAc()
    {
        if (oyunAyarlarPanel != null) oyunAyarlarPanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false); // İstersen gameover panelini gizleyebilirsin
    }

    public void AyarlarKapat()
    {
        if (oyunAyarlarPanel != null) oyunAyarlarPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(true); // Geri dönünce gameover paneli tekrar görünsün
    }

    public void SesDegistir()
    {
        sesDurumu = !sesDurumu;
        if (sesDurumu)
        {
            AudioListener.volume = 1f;
            if (sesButonText != null) sesButonText.text = "Ses: Açık";
        }
        else
        {
            AudioListener.volume = 0f;
            if (sesButonText != null) sesButonText.text = "Ses: Kapalı";
        }
    }

    public void TamEkranDegistir()
    {


        
        tamEkranDurumu = !tamEkranDurumu;
        Screen.fullScreen = tamEkranDurumu;

        
        if (tamEkranButonText != null)
        {
            if (tamEkranDurumu)
            {
                tamEkranButonText.text = "Tam Ekran: Açık";
            }
            else
            {
                tamEkranButonText.text = "Tam Ekran: Kapalı";
            }
        }

    }
}