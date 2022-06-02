using UnityEngine;

namespace Assets.Core
{
    public abstract class BaseUI : MonoBehaviour
    {
        protected virtual void Start()
        {
            Debug.Log($"Views: {this.GetType().Name}");
            UIManager.Instance.AddUIInScene(this);
        }
    }
}