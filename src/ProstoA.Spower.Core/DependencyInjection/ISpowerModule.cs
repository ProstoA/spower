namespace ProstoA.Spower.DependencyInjection {
    public interface ISpowerModule<T> where T : ISpowerModule<T> {
        void ConfigureServices(ISpowerServiceCollection services);
    }
}