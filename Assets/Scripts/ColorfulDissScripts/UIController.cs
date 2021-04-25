using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public Image[] HeartPoints;
    public TextMeshProUGUI ScoreText;

    [SerializeField] private Color[] m_HPColor;
    [SerializeField] private GameObject m_GameoverPanel, m_BlinkPanel;
    [SerializeField] private TextMeshProUGUI m_ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        m_GameoverPanel.SetActive(false);
        m_BlinkPanel.SetActive(false);
        SetHP();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "score: " + GameController.Instance.ScoreCount.ToString();
    }

    public void SetHP() 
    {
        for (int i = 0; i < HeartPoints.Length; i++)
        {
            HeartPoints[i].color = (i < GameController.Instance.HPRemaining) ? m_HPColor[0] : m_HPColor[1];
        }
    }

    internal void ShowGameoverPanelDelay()
    {
        Invoke("ShowGameoverPanel", 2.0f);
    }

    private void ShowGameoverPanel() 
    {
        m_ScoreText.text = "score: " + GameController.Instance.ScoreCount.ToString();
        SoundController.Instance.Play("GameEnds");
        m_GameoverPanel.SetActive(true);
    }

    public void BlinkThePanel() 
    {
        StartCoroutine(BlinkingThePanel());
    }

    public IEnumerator BlinkingThePanel() 
    {
        if (null !=m_BlinkPanel) m_BlinkPanel.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        if (null != m_BlinkPanel) m_BlinkPanel.SetActive(false);
    }
}
