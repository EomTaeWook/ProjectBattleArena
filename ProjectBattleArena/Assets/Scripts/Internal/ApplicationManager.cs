using Assets.Scripts.Models;
using Kosher.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class ApplicationManager : Singleton<ApplicationManager>
    {

        private const string LocalUrl = @"http://localhost:10000";

        public string CurrentServerUrl { get; private set; }

        public void Init()
        {
            CurrentServerUrl = LocalUrl;
        }
        public void SetResolution(int width, int height)
        {
            Screen.SetResolution(width, height, true);
        }
    }
}
