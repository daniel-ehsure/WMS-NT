using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace UI
{
   public interface InterfaceSelect
    {
       void setMateriel(string name, string id);
       void setMateriel(string name, string id,  string standard);
       void setMaterielAndPlace(string mname, string mid, string standard, string pid, string tray, int count, string typeName);
       void setMateriel(  string name, string id, int thick, int single, string standard, int length, int width)  ;
       void setPlace( string name, string id, int length, int width );
       void setMaterielAndPlace(string mname, string mid, int thick, int single, string standard, int length, int width, string pname, string pid, int plength, int pwidth, int count);
    }
}
