using System;

using ProstoA.Data.Model.Abstractions;

namespace ProstoA.Data.Model {
    [Serializable]
    public class SimpleObjectIdentity : IObjectIdentity {
        public SimpleObjectIdentity(string name) {
            Name = name;
        }

        public IObjectClass Class => null;

        public string Name { get; }

        public SemanticVersion Version => null;
    }
}