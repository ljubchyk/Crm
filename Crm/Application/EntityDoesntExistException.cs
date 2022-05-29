namespace Crm.Application
{
    [Serializable]
    public class EntityDoesntExistException : Exception
    {
        public EntityDoesntExistException() { }
        public EntityDoesntExistException(string message) : base(message) { }
        public EntityDoesntExistException(string message, Exception inner) : base(message, inner) { }
        protected EntityDoesntExistException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
