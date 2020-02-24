using System;

namespace Lavyn.Domain.Entities
{
    public interface Identifiable<T>
        where T : IComparable, IConvertible, IComparable<T>, IEquatable<T>
    {
        T Id { get; set; }
    }
}
