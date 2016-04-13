using System;
using System.Web.UI;

using Microsoft.SharePoint.Utilities;

namespace ProstoA.Spower.TestFixture.ControlTemplates.ProstoA.Spower.TestFixture {
    public partial class DependencyInjectionControl : UserControl {
        private readonly IServiceProvider _serviceProvider;

        public DependencyInjectionControl() { }

        public DependencyInjectionControl(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        protected void Page_Load(object sender, EventArgs e) {
            using(new SPMonitoredScope("DependencyInjectionControl")) {
                LabelError.Text = _serviceProvider.GetType().Assembly.Location;
                LabelError2.Text = _serviceProvider.GetType().Assembly.CodeBase;
            }
        }
    }
}