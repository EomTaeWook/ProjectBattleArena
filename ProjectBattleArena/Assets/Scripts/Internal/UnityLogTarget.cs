using Kosher.Log.LogForm.Renderer;
using Kosher.Log.LogTarget.Interface;
using Kosher.Log.Model;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Internal
{
    internal class UnityLogTarget : ILogTarget
    {
        LogFormRenderer _renderer;
        public UnityLogTarget()
        {
            _renderer = new LogFormRenderer();
            _renderer.SetLogFormText("${date} | ${level} | ${message} | ${callerFileName} : ${callerLineNumber}");
        }
        public Task Complete()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
        
        public Task WriteAsync(LogMessageModel logMessage)
        {
#if UNITY_EDITOR
            if(logMessage.LogLevel == Kosher.Log.LogLevel.Debug)
            {
                Debug.Log(_renderer.GetRenderString(logMessage));
            }
            else if(logMessage.LogLevel == Kosher.Log.LogLevel.Info)
            {
                Debug.LogWarning(_renderer.GetRenderString(logMessage));
            }
            else if(logMessage.LogLevel == Kosher.Log.LogLevel.Error)
            {
                Debug.LogError(_renderer.GetRenderString(logMessage));
            }
#else
            if (logMessage.LogLevel == Kosher.Log.LogLevel.Error)
            {
                Debug.LogError(_renderer.GetRenderString(logMessage));
            }
#endif

            return Task.CompletedTask;
        }
    }
}
