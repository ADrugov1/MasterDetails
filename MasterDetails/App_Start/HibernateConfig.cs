namespace MasterDetails
{
    using Models;
    using NHibernate.Cfg;
    using NHibernate.Tool.hbm2ddl;

    public class HibernateConfig
    {
        public static void RegisterHibernate()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Book).Assembly);
            new SchemaExport(cfg).Execute(true, true, false);
        }
    }
}