using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Submarine.Base
{

    public class BaseUtil
    {

        public static string EncryptPassword(string password)
        {
            string hash = Convert.ToBase64String(
              System.Security.Cryptography.MD5.Create()
              .ComputeHash(Encoding.UTF8.GetBytes(password))
            );
            return hash;
        }
    }
}
