using RhythmGame.Model;
using RhythmGame.UI;
using UnityEngine;
namespace RhythmGame.Controller
{
    public class NoteController : MonoBehaviour, IScore
    {
        public KeyCode KeyId;
        public Rigidbody2D Rigid2D;

        [SerializeField] NoteUI _ui;
        void Start()
        {
            Rigid2D.AddRelativeForce(500 * Time.fixedDeltaTime * Vector2.down, ForceMode2D.Impulse);
        }
        public void SetNote(Color note)
        {
            _ui.SetNote(note);
        }
        public void AdjustScore()
        {
            GameManage.Instance.AdjustScore(20);
            gameObject.SetActive(false);
        }
    }
}
