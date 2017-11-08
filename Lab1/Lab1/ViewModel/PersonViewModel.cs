using Lab1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Lab1.ViewModel
{
    // El INotifyPropertyChanged le notifica a la capa VIEW que algo ha cambiado
    public class PersonViewModel : INotifyPropertyChanged
    {
        #region Instances

        // Un Interface Command -> Para poder hacer el Bind del componente UI den MainPage.xaml
        public ICommand AgregarPersonaCommand { get; set; }
        public ICommand FiltrarPersonaCommand { get; set; }

        public ICommand EliminarPersonaCommand { get; set; }

        //private List<PersonModel> _lstPersonList = new List<PersonModel>();  // Este tipo de Lista no es Observable

        private ObservableCollection<PersonModel> _lstPersonList = new ObservableCollection<PersonModel>();  // Este tipo de Lista no es Observable

        private ObservableCollection<PersonModel> _lstPersonListCopy = new ObservableCollection<PersonModel>();  // Este tipo de Lista no es Observable


        // Se va a utilizar en el VIEW
        public ObservableCollection<PersonModel> lstPersonList
        {
            get
            {
                return _lstPersonList;
            }

            set
            {
                _lstPersonList = value;
                onPropertyChanged("lstPersonList"); // Notificar
            }
        }


        private string _NuevoIngreso = String.Empty;

        public String NuevoIngreso {
            get {
                return _NuevoIngreso;
            }

            set {
                _NuevoIngreso = value;
            }
        }

        private string _Filtro = String.Empty;
        
        public String Filtro {
            get { return _Filtro; }
            set { _Filtro = value; }
        }


        #endregion

        public PersonViewModel(){
            AgregarPersonaCommand = new Command(AgregarPersona);
            FiltrarPersonaCommand = new Command(FiltrarPersona);
            EliminarPersonaCommand = new Command(EliminarPersona);
        }

        // El método que va a ir como objeto en el Command de arriba
        private void AgregarPersona() {
            
            _lstPersonList.Add(new PersonModel("José Pablo", "Chaves","Alumno de curso Xamarin"));
            _lstPersonList.Add(new PersonModel("Pablo", "Rivera", "Alumno no matriculado de curso Xamarin"));
            _lstPersonList.Add(new PersonModel("Mónica", "Bermúdez", "Alumna de curso Xamarin"));
            _lstPersonList.Add(new PersonModel("Carlos", "Méndez", "Profesor de curso Xamarin"));
            _lstPersonList.Add(new PersonModel("Federico", "Brenes", "Alumno de curso VMWare"));

           // _lstPersonListCopy = _lstPersonList;
           // _lstPersonList.Add(new PersonModel{ nombre = NuevoIngreso, descripcion = "Alumno de Xamarin" });
            Console.WriteLine("Agregadas ->"+_lstPersonList.Count);            
        }

        private void EliminarPersona() {
            Console.WriteLine("*** INGRESANDO EN ELIMINAR! ***");
        }

        private void FiltrarPersona()
        {
            string filterWord = Filtro.ToUpper();
            if (filterWord.Length < 1)
            { // Se debe usar la lista inicial
                _lstPersonList = _lstPersonListCopy;
                onPropertyChanged("lstPersonList");
            }
            else {
                _lstPersonListCopy = _lstPersonList;
            }
            /*
            Console.WriteLine("Cantidad de datos en Lista ->" + _lstPersonList.Count);
            Console.WriteLine("Filtrando con: " +  filterWord);
            _lstPersonList.Clear();
            Console.WriteLine("Cantidad de datos en Lista después de borrar ->" + _lstPersonList.Count);
            */
            //ObservableCollection<PersonModel> lstBusqueda = 
            ObservableCollection<PersonModel> lstTmp = new ObservableCollection<PersonModel>();
            var query = _lstPersonList.Where(x => x.nombre.ToUpper().Contains(filterWord));
            foreach (PersonModel p in query) {
                if (p == null) break;
                Console.WriteLine("Busqueda: [" + p.nombre + " " + p.apellido + "," + p.descripcion + "]");
                lstTmp.Add(p);
            }

            if (lstTmp != null) {
                _lstPersonList = lstTmp;
                onPropertyChanged("lstPersonList");
            }

            Console.WriteLine("Cantidad de datos en copia: "+lstTmp.Count);
        }



        #region Notified Interface
        public event PropertyChangedEventHandler PropertyChanged;

        protected void onPropertyChanged(string PropertyName) {
            if (PropertyChanged != null) {
                // Indica que algo en View cambio 
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        #endregion

    }
}
