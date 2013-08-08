using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easyfy.CHS.Model.Extension
{
  public class FacetFilter
  {
    private string _key;
    public string Key
    {
      get
      {
        if (string.IsNullOrEmpty(_key))
          return string.Empty;
        // convert to char array of the string
        char[] letters = _key.ToCharArray();
        // upper case the first char
        letters[0] = char.ToUpper(letters[0]);
        // return the array made of the new char array
        return new string(letters);
      }
      set { _key = value; }
    }

    public string Value { get; set; }
  }
}
