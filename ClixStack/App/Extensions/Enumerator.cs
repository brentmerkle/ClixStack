using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AppFramework
{
    public interface IEnumeration<E, T> where E : IEnumeration<E, T>
    {
        T Value { get; }
        string Name { get; }
    }

    public abstract class Enumeration<E, T> : IEnumeration<Enumeration<E, T>, T> where E : Enumeration<E, T>
    {
        #region >>> Instance Code <<<
        public T Value { get; private set; }
        public string Name { get; private set; }

        protected Enumeration(string Name, T EnumValue)
        {
            Value = EnumValue;
            this.Name = Name;
            mapping.Add(Name, this);
            vmapping.Add(Value, this);
        }

        public override string ToString() { return Name; }
        #endregion

        #region >>> Static Tools <<<

        static private readonly Dictionary<string, Enumeration<E, T>> mapping;
        static private readonly Dictionary<T, Enumeration<E, T>> vmapping;

        static Enumeration()
        {
            mapping = new Dictionary<string, Enumeration<E, T>>();
            vmapping = new Dictionary<T, Enumeration<E, T>>();
        }

        protected static E ParseByName(string name)
        {
            try
            {
                Enumeration<E, T> result;
                if (mapping.TryGetValue(name, out result))
                {
                    return (E)result;
                }

                throw new InvalidCastException();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected static E ParseByValue(T value)
        {
            try
            {
                Enumeration<E, T> result;
                if (vmapping.TryGetValue(value, out result))
                {
                    return (E)result;
                }

                throw new InvalidCastException();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected static IEnumerable<E> All { get { return mapping.Values.AsEnumerable().Cast<E>(); } }
        #endregion
    }

}

