using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Core
{
    public abstract class BaseController<T> : MonoBehaviour where T : class
    {
        [ShowInInspector]
        protected T UIInstance { get; set; }
        public void Awake()
        {
            Debug.Log($"Controller: {this.GetType().Name}");
            UIInstance = UIManager.Instance.GetInstanceUI<T>();
        }
    }
}
