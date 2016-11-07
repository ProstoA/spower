namespace ProstoA.Data.Metamodel {
    public class ValueChange<T> : IValueChange {
        public ValueChange(ChangeStatus status, T oldValue = default(T), T newValue = default(T)) {
            Status = status;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public ChangeStatus Status { get; }

        public T OldValue { get; }

        public T NewValue { get; }

        object IValueChange.OldValue => OldValue;

        object IValueChange.NewValue => NewValue;
    }

    public static class ValueChange {
        public static ValueChange<T> Add<T>(T value) {
            return new ValueChange<T>(ChangeStatus.Added, newValue: value);
        }

        public static ValueChange<T> Modify<T>(T oldValue, T newValue) {
            return new ValueChange<T>(ChangeStatus.Modified, oldValue, newValue);
        }

        public static ValueChange<T> Remove<T>(T value) {
            return new ValueChange<T>(ChangeStatus.Removed, value);
        }
    }
}