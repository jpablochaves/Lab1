using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Lab1.Model
{
   public class PersonModel
    {
        public int id { get; set; }
        public string foto { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string telefono { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string direccion { get; set; }
        public string sexo { get; set; }
        public string observaciones { get; set; }
        public string nombreCompleto {
            get {
                return nombre + " " + apellido1 + " " + apellido2;
            }
            private set { }
        }
        public ObservableCollection<VentasModel> lstVentas { get; set; }


        public PersonModel() {                
        }

        

        public static ObservableCollection<PersonModel> ObtenerPersonas()
        {
            ObservableCollection<PersonModel> lst = new ObservableCollection<PersonModel>();

            lst.Add(new PersonModel { id = 1, nombre = "Carlos" });
            lst.Add(new PersonModel { id = 2, nombre = "Pablo" });
            lst.Add(new PersonModel { id = 3, nombre = "Mónica" });

            return lst;

        }


       
    }

}
