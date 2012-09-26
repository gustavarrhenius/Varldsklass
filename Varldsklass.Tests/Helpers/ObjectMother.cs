using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Varldsklass.Domain.Entities;

namespace Varldsklass.Tests.Helpers
{
    public class ObjectMother
    {
        public static Category Test1Category
        {
            get 
            {
                return new Category { ID = 1, Name = "Test1" };
            }
        }

        public static Category Test2Category
        {
            get
            {
                return new Category { ID = 2, Name = "Test2" };
            }
        }

        public static Category Test3Category
        {
            get
            {
                return new Category { ID = 3, Name = "Test3" };
            }
        }

        public static Post Test1Product
        {
            get
            {
                return new Post { ID = 1, Title = "Product1", Body = "Desc1", Created = new DateTime(2012, 9, 18), Category = Test1Category };
            }
        }

        public static Post Test2Product
        {
            get
            {
                return new Post { ID = 2, Title = "Product2", Body = "Desc2", Created = new DateTime(2012, 9, 19), Category = Test2Category};
            }
        }

        public static Post Test3Product
        {
            get
            {
                return new Post { ID = 3, Title = "Product3", Body = "Desc3", Created = new DateTime(2012, 9, 20), Category = Test1Category};
            }
        }

        public static Post Test4Product
        {
            get
            {
                return new Post { ID = 4, Title = "Product4", Body = "Desc4", Created = new DateTime(2012, 9, 21), Category = Test2Category };
            }
        }

        public static Post Test5Product
        {
            get
            {
                return new Post { ID = 5, Title = "Product5", Body = "Desc5", Created = new DateTime(2012, 9, 2), Category = Test1Category };
            }
        }

        public static List<Post> ProductList_5Products_Test1AndTest2Categories
        {
            get
            {
                return new List<Post> {
                    Test1Product,
                    Test2Product,
                    Test3Product,
                    Test4Product,
                    Test5Product,
                };
            }
        }
    }
}
