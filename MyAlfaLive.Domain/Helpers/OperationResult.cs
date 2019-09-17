using System.Collections.Generic;

namespace MyAlfaLive.Domain
{
    public class OperationResult
    {
        public List<KeyValuePair<string, string>> Errors { get; }

        public OperationResult(bool succeeded)
        {
            Succeeded = succeeded;
            Errors = new List<KeyValuePair<string, string>>();
        }

        public OperationResult(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = new List<KeyValuePair<string, string>>();
        }

        public OperationResult(bool succeeded, string message, string error)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = new List<KeyValuePair<string, string>>();
            AddError("", error);
        }

        public OperationResult(bool succeeded, string message, List<KeyValuePair<string, string>> errors)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = new List<KeyValuePair<string, string>>();
            AddErrors(errors);
        }

        public bool Succeeded { get; private set; }
        public bool HasMessage { get; private set; }

        private string _message;

        public string Message { 
            get { return _message;}
            set
            {
                _message = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                  HasMessage = true;
                }
                else
                {
                    HasMessage = false;
                }
            
            }
        }

        /// <summary>
        /// Add error description to result
        /// WARNING: When an error is added, Succeeded is set to false
        /// </summary>
        /// <param name="error">Error description</param>
        /// /// <param name="key">Key</param>
        public void AddError(string key, string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                Errors.Add(new KeyValuePair<string, string>(key, error));
                Succeeded = false;
            }
        }

        /// <summary>
        /// Add errors description to result
        /// WARNING: When an error is added, Succeeded is set to false
        /// </summary>
        /// <param name="error"></param>
        public void AddErrors(List<KeyValuePair<string,string>> errors)
        {
            foreach (var error in errors)
            {
                AddError(error.Key, error.Value);
            }
        }
    }
}
