namespace USTAPG.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using ViewModels;

    class InstanceLocator
    {
        #region Propiedades
        public MainViewModel Main { get; set; }
        #endregion

        #region Constructor
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
