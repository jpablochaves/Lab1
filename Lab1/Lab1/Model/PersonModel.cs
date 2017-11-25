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

            lst.Add(new PersonModel { id = 1, nombre = "Federico", apellido1 = "Brenes", apellido2 = "Rueda", telefono = "88995656", fechaNacimiento = new DateTime(1970, 1, 1), direccion = "Cartago, Paraíso", sexo = "M", observaciones = "Alumno de VMWare" });
            lst.Add(new PersonModel { id = 1, nombre = "José Pablo", apellido1 = "Chaves", apellido2 = "Arias", telefono = "87069998", fechaNacimiento = new DateTime(1987, 8, 26), direccion = "San José, Coronado", sexo = "M", observaciones = "Alumno de Xamarin" });
            lst.Add(new PersonModel { id = 1, nombre = "Mónica", apellido1 = "Bermúdez", apellido2 = "García", telefono = "88995656", fechaNacimiento = new DateTime(1985, 1, 1), direccion = "Heredia, San Francisco", sexo = "F", observaciones = "Alumna de Xamarin" });


            return lst;

        }


       
    }

}
