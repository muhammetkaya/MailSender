using System;
using System.Collections.Generic;
using System.Text;

namespace MailSender.Contracts.Types
{
    public abstract class Singleton<T> where T: new()
    {

        #region ..Constructor..

        protected Singleton() { }

        #endregion

        #region ..Fields..

        private static T instance;

        #endregion

        #region ..Properties..

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }

        #endregion

    }
}
