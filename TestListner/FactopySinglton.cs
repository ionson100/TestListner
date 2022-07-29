using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;
using StackExchange.Redis;

namespace TestListener
{
    static class  FactorySingleton
    {
        public static bool IsInit { get; set; }
        private static Dictionary<Type, WrapperFactory> hostSinletons=new Dictionary<Type, WrapperFactory>();

        public static void AddSingleton<TI,TO>()
        {
            hostSinletons.Add(typeof(TI),new WrapperFactory(typeof(TO)));
        }

        public static T GetSingleton<T>()
        {
            var wrapperFactory = hostSinletons[typeof(T)];
            return (T)  wrapperFactory.Instansce;
        }

       
    }

    class WrapperFactory
    {
        public object Instansce { get; }

        public WrapperFactory(Type type)
        {
            InnerType = type;
            try
            {
                Instansce= Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public Type InnerType { get; set; }
    }

   
}


