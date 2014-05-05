using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;

namespace SilverlightContrib.Sample.Web
{
  /// <summary>
  /// Sample non-DB Data Service to allow for simple demonstration of the code.
  /// </summary>
  public class SampleDataService : DataService<SampleDataServiceContext>
  {
    // This method is called only once to initialize service-wide policies.
    public static void InitializeService(IDataServiceConfiguration config)
    {
      config.SetEntitySetAccessRule("Customers", EntitySetRights.AllRead);
      config.SetServiceOperationAccessRule("CustomerNames", ServiceOperationRights.AllRead);
    }

    /// <summary>
    /// Service Operation that returns an array of strings (instead of entities) to show the 
    /// Data Service Extensions
    /// </summary>
    /// <returns></returns>
    [WebGet]
    public IEnumerable<string> CustomerNames()
    {
      return this.CurrentDataSource.Customers.Select(c => c.Name);
    }
  }

  /// <summary>
  /// Data Service Context without any data store back end to simply showing off the
  /// Data Service extensions.
  /// </summary>
  public class SampleDataServiceContext
  {
    public IQueryable<Customer> Customers
    {
      get
      {
        return new List<Customer>() 
        { 
          new Customer() { CustomerID = 1, Name = "Bob Smith", BirthDate = new DateTime(1969, 4, 24) },
          new Customer() { CustomerID = 2, Name = "Jack Horner", BirthDate = new DateTime(1960, 6, 3) },
          new Customer() { CustomerID = 3, Name = "Hank Jones", BirthDate = new DateTime(1947, 6, 27) }
        }.AsQueryable();
      }
    }
  }

  /// <summary>
  /// Sample entity class for use in our samples.
  /// </summary>
  public class Customer
  {
    public int CustomerID { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
  }
}
