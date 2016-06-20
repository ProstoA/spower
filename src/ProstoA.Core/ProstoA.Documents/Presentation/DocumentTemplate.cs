using System;

using ProstoA.Data.Model.Abstractions;
using ProstoA.Data.Presentation;
using ProstoA.Documents.Model;

namespace ProstoA.Documents.Presentation {
    public abstract class DocumentTemplate<TDocument, TFormItem> : ITemplate<TDocument, IForm<TDocument>> where TFormItem : IFormItem {
        public abstract IView Apply(TDocument document, DocumentForm<TDocument, TFormItem> form);

        public IView Apply(TDocument data, IForm<TDocument> form) {
            var documentForm = form as DocumentForm<TDocument, TFormItem>;
            if(documentForm == null) {
                throw new ArgumentException(
                    $"Form wasn't DocumentForm<{typeof (TDocument)}, {typeof (TFormItem)}> instance.",
                    nameof(form)
                );
            }

            return Apply(data, documentForm);
        }
    }
}