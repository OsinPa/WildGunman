using System;
using System.Text;
using UnityEngine;

namespace WildGunman.Logger.Unity
{
    public class UnityLog : ILog
    {
        private const string WarningPrefix = "WARNING";
        private readonly Type _type;
        private readonly StringBuilder _builder = new();

        public UnityLog(Type type)
        {
            _type = type;
        }

        public ILog AddField(string fieldName, object value)
        {
            if (_builder.Length > 0)
            {
                _builder.Append(", ");
            }
            _builder.Append($"{fieldName} = {value}");

            return this;
        }

        public void Warn(object message)
        {
            Debug.LogWarning(GetMessage(WarningPrefix, message));
        }

        private string GetMessage(string prefix, object message)
        {
            var fields = "";
            if (_builder.Length > 0)
            {
                fields = $" Fields: {_builder}";
                _builder.Clear();
            }
            return $"[{prefix}][{_type}] Message: {message} {fields}";
        }
    }
}