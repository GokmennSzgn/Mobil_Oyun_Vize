using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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