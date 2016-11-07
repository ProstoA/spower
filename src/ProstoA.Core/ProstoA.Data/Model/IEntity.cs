using System;

namespace ProstoA.Data.Model {
    /// <summary>
    /// Сущность
    /// </summary>
    public interface IEntity<T> : IReference<T>, IEquatable<IEntity<T>> where T: IEntity<T> {

    }
}