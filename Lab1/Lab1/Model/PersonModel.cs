using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.Model
{
   public class PersonModel
    {

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string descripcion { get; set; }

        public PersonModel() {                
        }

        public PersonModel(string _nom, string _ape, string _desc) {
            nombre = _nom;
            apellido = _ape;
            descripcion = _desc;
        }

    }

}
