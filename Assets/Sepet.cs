using UnityEngine;
using UnityEngine.InputSystem;

public class Sepet : MonoBehaviour
{
    [SerializeField] private float hiz = 5f;
    [SerializeField] private int Can= 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float hedefX = transform.position.x; 

        
        var imlec = Mouse.current;
        if (imlec != null && imlec.leftButton.isPressed)
        {
            
            Vector2 ekranNoktasi = imlec.position.ReadValue();
            Vector3 dunyaNoktasi = Camera.main.ScreenToWorldPoint(ekranNoktasi);

            
            hedefX = dunyaNoktasi.x;
        }
        else
        {
            
            float yatayGirdi = Input.GetAxis("Horizontal");
            if (yatayGirdi != 0f)
            {
                hedefX = transform.position.x + (yatayGirdi * hiz * Time.deltaTime);
            }
        }

        
        float yeniX = Mathf.MoveTowards(transform.position.x, hedefX, hiz * Time.deltaTime);

        
        yeniX = Mathf.Clamp(yeniX, -8f, 8f);

        
        transform.position = new Vector3(yeniX, transform.position.y, 0f);

    }
}
