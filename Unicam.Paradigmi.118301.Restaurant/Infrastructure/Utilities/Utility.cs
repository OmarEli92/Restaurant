using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    /** The purpose of this class is to provide a method that can be used to verify if a certain entity has a certain attribute**/
    public static class Utility
    {
        public static bool isAttributeValid(Type type,string attribute)
        {
            return type.GetProperty(attribute) != null;
        }
    }
}
