using System;
using System.Collections.Generic;

using ProstoA.Data.Object.Abstractions;

namespace ProstoA.Data.Object {
    public class ObjectRevision<T> : IObjectRevision<T>, IObjectIdentity<IObjectRevision<T>> where T : IObjectIdentity<T> {
        private readonly IRevisionObjectIdentity<T> _identity;

        public ObjectRevision(IRevisionObjectIdentity<T> identity, IObjectDiff<T> diff, params IObjectIdentity<IObjectRevision<T>>[] parents) {
            _identity = identity;
            Diff = diff;
            Parents = Array.AsReadOnly(parents);
        }

        public string Name => _identity.Revision.Key;

        public IReadOnlyCollection<IObjectIdentity<IObjectRevision<T>>> Parents { get; }

        public IObjectDiff<T> Diff { get; }

        string IObjectIdentity<IObjectRevision<T>>.Type => GetType().AssemblyQualifiedName;
        string IObjectIdentity<IObjectRevision<T>>.Key => Name;
    }
}