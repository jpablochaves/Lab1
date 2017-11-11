using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Lab1.Model
{
   public class PersonModel
    {

        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string descripcion { get; set; }

        public PersonModel() {                
        }


        public PersonModel(int _id, string _nom, string _ape, string _desc)
        {
            id = _id;
            nombre = _nom;
            apellido = _ape;
            descripcion = _desc;
        }

        public static ObservableCollection<PersonModel> ObtenerPersonas()
        {
            ObservableCollection<PersonModel> lst = new ObservableCollection<PersonModel>();

            lst.Add(new PersonModel(1,"José Pablo", "Chaves", "Alumno de curso Xamarin"));
            lst.Add(new PersonModel(2,"Pablo", "Rivera", "Alumno no matriculado de curso Xamarin"));
            lst.Add(new PersonModel(3,"Mónica", "Bermúdez", "Alumna de curso Xamarin"));
            lst.Add(new PersonModel(4,"Carlos", "Méndez", "Profesor de curso Xamarin"));
            lst.Add(new PersonModel(5,"Federico", "Brenes", "Alumno de curso VMWare"));

            return lst;

        }


       
    }

}
