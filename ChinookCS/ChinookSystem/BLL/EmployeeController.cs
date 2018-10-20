using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Entities;
using ChinookSystem.DAL;
using System.ComponentModel;  //ODS
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class EmployeeController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<SupportEmployee> Employee_GetPlayListCustomers()
        {
            using (var context = new ChinookContext())
            {
                var employeelist = from x in context.Employees
                                   where x.Title.Contains("Support")
                                   orderby x.LastName, x.FirstName
                                   select new SupportEmployee
                                   {
                                       Name = x.LastName + ", " + x.FirstName,
                                       ClientCount = x.Customers.Count(),
                                       ClientList = (from y in x.Customers
                                                     orderby y.LastName, y.FirstName
                                                     select new PlayListCustomer
                                                     {
                                                         lastname = y.LastName,
                                                         firstname = y.FirstName,
                                                         phone = y.Phone
                                                     }).ToList()
                                   };
                return employeelist.ToList();
            }
        }
    }
}
