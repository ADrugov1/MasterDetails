namespace MasterDetails
{
    using NHibernate;
    using NHibernate.Cfg;
    using Models;
    public class HibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var cfg = new Configuration();
                    cfg.Configure();
                    cfg.AddAssembly(typeof(Book).Assembly);
                    _sessionFactory = cfg.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}