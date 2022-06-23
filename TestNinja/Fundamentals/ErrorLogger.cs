using System;

namespace TestNinja.Fundamentals
{
    public class ErrorLogger
    {
        public string LastError { get; set; }
        public event EventHandler<Guid> ErrorLogged;
        private Guid _guid;

        public void Log(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
                throw new ArgumentNullException();

            LastError = error;

            // Write the log to a storage
            // ...
            _guid = Guid.NewGuid();
            OnErrorLogged();
        }

        protected virtual void OnErrorLogged()
        {
            ErrorLogged?.Invoke(this, _guid);
        }
    }
}