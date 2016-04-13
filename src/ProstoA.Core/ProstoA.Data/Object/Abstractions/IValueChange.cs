namespace ProstoA.Data.Object.Abstractions {
    public interface IValueChange {
        ChangeStatus Status { get; }

        object OldValue { get; }

        object NewValue { get; }
    }
}