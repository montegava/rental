using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace DAL
{
    class WcfOperationContext : IExtension<OperationContext>
    {
        private rentalEntities _context;

        private WcfOperationContext()
        {
            
        }

        /// <summary>
        /// Gets the database context for the request.
        /// </summary>
        public rentalEntities Context
        {
            get
            {
              //  var c = new rentalEntities();
                                               
                //ConnectionManager.ConnectionStringEntity)
                return _context ?? (_context = new rentalEntities());
            }
        }

    
        /// <summary>
        /// Gets the current request context.
        /// </summary>
        public static WcfOperationContext Current
        {
            get
            {
                if (System.ServiceModel.OperationContext.Current == null)
                {
                    return new WcfOperationContext();
                }
                WcfOperationContext context = OperationContext.Current.Extensions.Find<WcfOperationContext>();
                if (context == null)
                {
                    context = new WcfOperationContext();
                    OperationContext.Current.Extensions.Add(context);
                }
                return context;
            }
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public void Attach(OperationContext owner) { }

        /// <summary>
        /// Does nothing.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public void Detach(OperationContext owner) { }
    }
}