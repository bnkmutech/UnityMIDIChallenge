using System;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

namespace RaindropGame
{
    public class RaindropKeyIndicator : MonoBehaviour
    {
        [Header("Setting")] [SerializeField] private Light2D LightEffect;
        [SerializeField] private TextMeshProUGUI text;
        
        private Image image;
        private Animator animator;
        private ParticleSystem ps;

        private void Awake()
        {
            image = GetComponent<Image>();
            animator = GetComponent<Animator>();
            ps = GetComponent<ParticleSystem>();
        }

        public void InitKeyIndicator(KeyCode keyCode, Color color)
        {
            //Set up key text
            text.text = keyCode.ToString();
            
            //Set up color
            image.color = new Color(color.r,color.g,color.b,0.8f);
            LightEffect.color = color;
            var psMain = ps.main;
            psMain.startColor = color;
        }

        public void OnKeyPressed()
        {
            animator.Play("KeyIndiactor_Pressed");
            animator.SetBool("Pressed",true);
        }

        public void OnKeyReleased()
        {
            animator.SetBool("Pressed",false);
        }

        public void PlayHitEffect()
        {
            ps.Play();
        }
    }
}