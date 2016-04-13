using System;
using System.Collections.Generic;

namespace ProstoA.Data.Object.Abstractions {
    public interface IObjectDiff<T> : IDictionary<IObjectIdentity<Model.Abstractions.IDataItem<T>>, IValueChange> {
        ChangeStatus Status { get; }
    }

    public interface IExpire {
        Period Validity { get; }

        bool ValidOn(DateTimeOffset date);
    }

    public interface IDataObject<T> : IDictionary<IObjectIdentity<Model.Abstractions.IDataItem<T>>, object>, IObjectIdentity<T> {

    }

    public interface IObjectSnapshot<T> : IDataObject<T>, IRevisionObjectIdentity<T> {
        IObjectSnapshot<T> ApplyChanges(IObjectIdentity<IObjectRevision<T>> revision, params IObjectDiff<T>[] diffs);
    }

    public interface IObjectChangelog<T> : IRevisionObjectIdentity<T> {

    }
}