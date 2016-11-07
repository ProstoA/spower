
namespace ProstoA.Spower.Properties  {

    public partial class Resources {
    


        private static System.Resources.ResourceManager _resourceManager;

        ///<summary>
        /// Get the ResourceManager
        ///</summary>
        private static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                return _resourceManager ?? (_resourceManager = new System.Resources.ResourceManager("ProstoA.Spower.Properties.Resources", typeof(Resources).Assembly));
            }
        }


        
        ///<summary>
        ///    <list type='bullet'>
        ///        <item>
        ///            <description>https://github.com/ProstoA/spower/wiki/Dependency-Injection</description>
        ///        </item>
        ///        <item>
        ///            <description></description>
        ///        </item>
        ///    </list>
        ///</summary>
        public static string Error_MustBeOneConstructorWithParameters_HelpLink { get { return ResourceManager.GetString("Error_MustBeOneConstructorWithParameters_HelpLink"); } }

            }
}

namespace ProstoA.Spower.Properties  {

    public partial class Resources {
    
        ///<summary>
        ///    <list type='bullet'>
        ///        <item>
        ///            <description>Невозможно инициализировать страницу '{page}'. Допустимо не более одного DI конструктора.</description>
        ///        </item>
        ///        <item>
        ///            <description></description>
        ///        </item>
        ///    </list>
        ///</summary>
        public static string Error_MustBeOneConstructorWithParameters_Message(object page) { return ResourceManager.GetString("Error_MustBeOneConstructorWithParameters_Message").Replace("{page}", page.ToString()); }
            }
}

namespace ProstoA.Spower.Properties  {

    public partial class Resources {
    
        ///<summary>
        ///    <list type='bullet'>
        ///        <item>
        ///            <description>No service for type '{ServiceType}' has been registered.</description>
        ///        </item>
        ///        <item>
        ///            <description></description>
        ///        </item>
        ///    </list>
        ///</summary>
        public static string Error_NoServiceRegistered_Message(object serviceType) { return ResourceManager.GetString("Error_NoServiceRegistered_Message").Replace("{ServiceType}", serviceType.ToString()); }
            }
}

namespace ProstoA.Spower.Properties  {

    public partial class Resources {
    
        ///<summary>
        ///    <list type='bullet'>
        ///        <item>
        ///            <description>Невозможно инициализировать страницу '{page}'. Возникла ошибка: {message}</description>
        ///        </item>
        ///        <item>
        ///            <description></description>
        ///        </item>
        ///    </list>
        ///</summary>
        public static string Error_PageInitializationFail_Message(object page, object message) { return ResourceManager.GetString("Error_PageInitializationFail_Message").Replace("{page}", page.ToString()).Replace("{message}", message.ToString()); }
            }
}

namespace ProstoA.Spower.Properties  {

    public partial class Resources {
    
        ///<summary>
        ///    <list type='bullet'>
        ///        <item>
        ///            <description>https://github.com/ProstoA/spower/wiki/Dependency-Injection</description>
        ///        </item>
        ///        <item>
        ///            <description></description>
        ///        </item>
        ///    </list>
        ///</summary>
        public static string Error_RequireDefaultConstructor_HelpLink { get { return ResourceManager.GetString("Error_RequireDefaultConstructor_HelpLink"); } }

            }
}

namespace ProstoA.Spower.Properties  {

    public partial class Resources {
    
        ///<summary>
        ///    <list type='bullet'>
        ///        <item>
        ///            <description>Невозможно инициализировать страницу '{page}'. Требуется конструктор по умолчанию.</description>
        ///        </item>
        ///        <item>
        ///            <description></description>
        ///        </item>
        ///    </list>
        ///</summary>
        public static string Error_RequireDefaultConstructor_Message(object page) { return ResourceManager.GetString("Error_RequireDefaultConstructor_Message").Replace("{page}", page.ToString()); }
            }
}
