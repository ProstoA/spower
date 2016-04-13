using System;

using Microsoft.SharePoint.WebControls;

namespace ProstoA.Spower.TestFixture.Layouts.ProstoA.Spower.TestFixture {
    public partial class DependencyInjectionPage : LayoutsPageBase {
        private readonly IServiceProvider _serviceProvider;

        public DependencyInjectionPage() {}

        public DependencyInjectionPage(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        protected void Page_Load(object sender, EventArgs e) {
            Test.Text = _serviceProvider.GetType().AssemblyQualifiedName;
        }
    }
}