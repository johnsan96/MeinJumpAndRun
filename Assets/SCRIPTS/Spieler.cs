using TMPro;
using UnityEngine;

public class Spieler : MonoBehaviour
{

    public GameObject gewinn;
    public GameObject test;
    public TextMeshProUGUI punkteAnzeige;
    public TextMeshProUGUI lebensanzeige;
    public TextMeshProUGUI zeitAnzeige;

    readonly float eingabeFaktor = 10;

    int anzahlPunkte = 0;
    int anzahlLeben = 3;
    float zeitStart;
    bool spielGestartet = false;

    private void Update()
    {
        float xEingabe = Input.GetAxis("Horizontal");
        float yEingabe = Input.GetAxis("Vertical");

        float xNeu = transform.position.x +
            xEingabe * eingabeFaktor * Time.deltaTime;
        float yNeu = transform.position.y +                                            
            yEingabe *  eingabeFaktor * Time.deltaTime;
        transform.position = new Vector3(xNeu, yNeu, 0);

        if(!spielGestartet && (xEingabe != 0 || yEingabe != 0))
        {
            spielGestartet = true;
            zeitStart = Time.time;
        }

        if (spielGestartet)
            zeitAnzeige.text = string.Format("Zeit: {0,6:0.0} sec.", Time.time - zeitStart);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Gewinn"))
        {

            anzahlPunkte++;

            if(anzahlPunkte < 6)
            {
                punkteAnzeige.text = "Punkte: " + anzahlPunkte;
            } else
            {
                gameObject.SetActive(false);
                gewinn.SetActive(false);
                punkteAnzeige.text = "Gewonnen";
                spielGestartet = false;
            }


            float xNeu = Random.Range(-8.0f, 8.0f);
            float yNeu = -2.7f + anzahlPunkte;

            // Banane neu positionieren
            gewinn.transform.position = new Vector3(xNeu, yNeu, 0);

        } else if(collision.gameObject.CompareTag("Gefahr")) {
            
            anzahlLeben--;

            lebensanzeige.text = "Leben: " + anzahlLeben;
            gameObject.SetActive(false);

            if(anzahlLeben >0) {
                Invoke(nameof(NaechstesLeben), 2);
            } else {
                gewinn.SetActive(false);
                lebensanzeige.text = "Verloren";
                spielGestartet = false;
            }
        }
    }

    void NaechstesLeben()
    {
        transform.position = new Vector3(0, -4.4f, 0);
        gameObject.SetActive(true);
    }

    public void SpielNeuButton_Click()
    {
        if (spielGestartet)
            return;

        anzahlLeben = 3;
        anzahlPunkte = 0;
        

        punkteAnzeige.text = "Punkte: 0";
        lebensanzeige.text = "Leben: 3";
        zeitAnzeige.text = "Zeit:   0.0 sec.";

        transform.position = new Vector3(0, -4.4f, 0);
        gameObject.SetActive(true) ;

        gewinn.transform.position = new Vector3(4, -2.7f, 0);
        gewinn.SetActive(true);

    }
}
