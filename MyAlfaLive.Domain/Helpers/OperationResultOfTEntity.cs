using System.Collections.Generic;

namespace MyAlfaLive.Domain
{
    public class OperationResult<TEntity> : OperationResult
    {
        public OperationResult(bool succeeded)
            : base(succeeded) { }

        public OperationResult(bool succeeded, string message)
            : base(succeeded, message) { }

        public OperationResult(bool succeeded, TEntity entity)
            : base(succeeded)
        {
            Entity = entity;
        }

        public OperationResult(bool succeeded, TEntity entity, string message)
            : base(succeeded, message)
        {
            Entity = entity;
        }

        public OperationResult(bool succeeded, TEntity entity, string message, string error)
            : base(succeeded, message, error)
        {
            Entity = entity;
        }

        public OperationResult(bool succeeded, TEntity entity, string message, List<KeyValuePair<string, string>> errors)
            : base(succeeded, message, errors)
        {
            Entity = entity;
        }

        public TEntity Entity { get; set; }
    }
}
