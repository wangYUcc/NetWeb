using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Tools
{
  class CovertTools
  {
    /// <summary>
    /// 通过属性名（字符串）获取属性值 obj 的属性值
    /// </summary>
    /// <returns>返回obj的属性值</returns>
    public static object GetObjValByAttrName(object obj, string attr)
    {
      var propertyInfo = obj.GetType().GetProperty(attr);   // 通过属性名获取值
      var value = propertyInfo.GetValue(obj, null);
      return value;
    }

    /// <summary>
    /// 动态转换指定类型，将source 变量转换为target 变量类型
    /// </summary>
    /// <returns>返回target变量类型的变量</returns>
    public static object DynamicConversionType(object source, object target)
    {
      var types = Type.GetTypeCode(target.GetType());
      var changeTypeValue = Convert.ChangeType(source, types);
      return changeTypeValue;
    }

  }
}
