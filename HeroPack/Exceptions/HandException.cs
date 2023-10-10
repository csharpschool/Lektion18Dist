using HeroPack.Interfaces;

namespace HeroPack.Exceptions
{
    public class HandException : ItemException
    {
        public HandException(string message, IItem? item = null)
            : base(message, item)
        {
        }
    }
}
