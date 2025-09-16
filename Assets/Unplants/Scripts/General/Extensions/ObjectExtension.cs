using System;

namespace Unplants.Scripts.General.Extensions
{
    public static class ObjectExtension
    {
        public static void ThrowIfNull(this object obj)
        {
            if (obj == null) throw new NullReferenceException();
        }
        
        public static void ThrowIfNull(this object obj, string message)
        {
            if (obj == null) throw new NullReferenceException(message);
        }
    }
}