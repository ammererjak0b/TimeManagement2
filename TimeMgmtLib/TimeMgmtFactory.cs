using TimeMgmtLib.Models;
using Autofac;
using TimeMgmtLib.Appconfig;
using TimeMgmtLib.DataAccess;
using System;

namespace TimeMgmtLib
{
    /// <summary>
    /// This singleton&factory class is providing a container, which allows other classes to resolve objects with.
    /// Because the class is a singleton, there is only one instance while running.
    /// </summary>
    public class TimeMgmtFactory
    {
        private IUser _user;

        private static TimeMgmtFactory _instance;

        private IContainer _container;
        private ContainerBuilder _builder;

        /// <summary>
        /// The contructor is private, because there is only one way to get a instance of this class
        /// </summary>
        private TimeMgmtFactory()
        {

        }

        /// <summary>
        /// This field stores an instance of the TimeMgmtFactory class.
        /// </summary>
        public static TimeMgmtFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimeMgmtFactory();
                }
                return _instance;
            }
        }

        /// <summary>
        /// This field stores an instance of the user that is logged in.
        /// Through out the whole application it is possible to access the user.
        /// </summary>
        public IUser UserInstance
        {
            get
            {
                if (_user != null)
                {
                    return _user;
                }
                else
                {
                    //TODO: make warning or something like that: throw new Exception("user is not set");
                    return new User();
                }
            }
            set
            {
                _user = value;
            }
        }

        /// <summary>
        /// This method initialises the container.
        /// Every implementation of an interface can be registered in this method.
        /// </summary>
        public void Initialise()
        {
            _builder = new ContainerBuilder();

            _builder.RegisterType<TimestampHandler>().As<ITimestampHandler>().SingleInstance();
            _builder.RegisterType<AdoHandler>().As<IDBAccessable>().SingleInstance();
            _builder.RegisterType<DBDataHandler>().As<IDataAccessable>().SingleInstance();
            _builder.RegisterType<AppConfigHandler>().As<IAppConfigHandler>().SingleInstance();

            _container = _builder.Build();
        }

        /// <summary>
        /// This method returns the class that implements an interface that is initialised
        /// </summary>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }


        if(Person.GetAction() == Action.DrinkBenzin){
            Person.Die(new DeathException("Jo wonnst benzin drinkst bist eh hi so"));
        }
    }
}
