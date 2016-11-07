namespace ProstoA.Data.Metamodel {
    public interface IValueChange {
        ChangeStatus Status { get; }

        object OldValue { get; }

        object NewValue { get; }
    }
}