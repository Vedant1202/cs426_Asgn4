using TMPro;
using UnityEngine;

public class HardwareManager : MonoBehaviour
{
    
    public GameObject alu;
    public float timeRemaining = 10;
    public bool timerRunning = false;
    [SerializeField] private TMP_Text aluHealthDisplay;
    [SerializeField] private TMP_Text timerDisplay;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    public void damageALU() {
        

    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager != null && gameManager.gameStarted) {
            aluHealthDisplay.gameObject.SetActive(true);
            timerDisplay.gameObject.SetActive(true);
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                if (timeRemaining < 0) {
                    timeRemaining = 0;
                }
                timerDisplay.text = "Time left: " + Mathf.FloorToInt(timeRemaining);
            } else {
                timerDisplay.text = "Time is up!";
            }
        }

        if (alu.GetComponent<ALU>().health > 0) {
            aluHealthDisplay.text = "ALU HP = " + alu.GetComponent<ALU>().health;
        } else {
            aluHealthDisplay.text = "ALU HACKED!";
            
        }
        
    }
}
