using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SilverlightContrib.Sample.SampleDataServices;
using SilverlightContrib.Data.Services.Client;
using System.Data.Services.Client;
using SilverlightContrib.Data.Services.Extensions;

namespace SilverlightContrib.Sample
{
  public partial class DataServiceSample : UserControl
  {
    SampleDataServiceContext ctx = 
      new SampleDataServiceContext(new Uri("./SampleDataService.svc", UriKind.Relative)); 

    public DataServiceSample()
    {
      InitializeComponent();

      getCustomerButton.Click += (s, e) =>
        {
          var qry = from c in ctx.Customers
                    orderby c.Name
                    select c;

          qry.BeginExecute<Customer>(new AsyncCallback(a => customerBox.ItemsSource = qry.EndExecute(a)), null); 

       };

      getCustomerNamesButton.Click += (s, e) =>
        {
          ctx.BeginExecuteNonEntityOperation<string>(new Uri("CustomerNames", UriKind.Relative),
            new AsyncCallback(a =>
              {
                Dispatcher.BeginInvoke(() => customerNamesBox.ItemsSource = ctx.EndExecuteNonEntityOperation<string>(a) );
              }), null);
        };

    }
  }
}
