using UnityEngine;


namespace StaserSDK.Views
{
    public class View : MonoBehaviour
    {
        [SerializeField] protected GameObject Model;
        protected bool IsHidden => Model.activeSelf;
    
        public virtual void Show()
        {
            if(!Model.activeSelf) Model.SetActive(true);
        }

        public virtual void Hide()
        {
            Model.SetActive(false);
        } 
    }
}

