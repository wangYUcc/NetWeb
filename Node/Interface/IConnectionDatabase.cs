using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Service.Interface
{
  public interface IConnectionDatabase<T>
  {
  

    void CloseConnect();
    T GetConnect();
  }
}
