using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GirisKontrol : MonoBehaviour
{
    [SerializeField] private GameObject anaMenuPanel;
    [SerializeField] private GameObject ayarlarPanel;
    [SerializeField] private TextMeshProUGUI sesButonText;
    [SerializeField] private TextMeshProUGUI tamEkranButonText; 
    [SerializeField] private GameObject hakkindaPanel;
    [SerializeField] private GameObject Can;


    private bool sesDurumu = true;
    private bool tamEkranDurumu = true; 

    private void Awake()
    {
        CikisButonuOlustur();
    }

    private void CikisButonuOlustur()
    {
        GameObject oyunaBasla = GameObject.Find("OyunaBaslaButton");
        if (oyunaBasla == null || GameObject.Find("GirisCikisButton") != null) return;

        GameObject cikis = Instantiate(oyunaBasla, oyunaBasla.transform.parent);
        cikis.name = "GirisCikisButton";

        RectTransform cikisTransform = cikis.GetComponent<RectTransform>();
        RectTransform kaynakTransform = oyunaBasla.GetComponent<RectTransform>();
        cikisTransform.anchoredPosition = new Vector2(
            kaynakTransform.anchoredPosition.x,
            kaynakTransform.anchoredPosition.y - 175f);

        Button cikisButton = cikis.GetComponent<Button>();
        cikisButton.onClick.RemoveAllListeners();
        cikisButton.onClick.AddListener(OyundanCik);

        TextMeshProUGUI cikisYazisi = cikis.GetComponentInChildren<TextMeshProUGUI>(true);
        if (cikisYazisi != null) cikisYazisi.text = "Çıkış";
    }

    public void OyunaBasla()
    {
        SceneManager.LoadScene("OyunSahnesi");
    }

    public void AyarlarAc()
    {
        if (ayarlarPanel != null) ayarlarPanel.SetActive(true);
        if (anaMenuPanel != null) anaMenuPanel.SetActive(false);
    }

    public void AyarlarKapat()
    {
        if (ayarlarPanel != null) ayarlarPanel.SetActive(false);
        if (anaMenuPanel != null) anaMenuPanel.SetActive(true);
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

    // Tam Ekran Aç / Kapat Fonksiyonu
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


    public void HakkindaAc()
    {
        if (hakkindaPanel != null) hakkindaPanel.SetActive(true);
        if (anaMenuPanel != null) anaMenuPanel.SetActive(false);
    }

    public void HakkindaKapat()
    {
        if (hakkindaPanel != null) hakkindaPanel.SetActive(false);
        if (anaMenuPanel != null) anaMenuPanel.SetActive(true);
    }

    
    public void OyundanCik()
    {
        Debug.Log("Oyun kapatıldı!");

        #if UNITY_EDITOR
                // Eğer Unity editörünün içindeysek Play modunu durdur
                EditorApplication.isPlaying = false;
        #else
                // Eğer oyunun build (.exe) halindeyse oyunu tamamen kapat
                Application.Quit();
        #endif

    }

}