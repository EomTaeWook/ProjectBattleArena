using Assets.Scripts.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    internal class UIElement : MonoBehaviour
    {
        public void Dispose()
        {
            UIManager.Instance.CloseUI(this);
        }
    }
}
