﻿using Lab1.Model;
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
        public ICommand TextoBuscarCommand { get; set; }

        public ICommand BorrarPersonaCommand { get; set; }

        //private List<PersonModel> _lstPersonList = new List<PersonModel>();  // Este tipo de Lista no es Observable

        private ObservableCollection<PersonModel> _lstPersonList = new ObservableCollection<PersonModel>();  // Este tipo de Lista no es Observable

       // private ObservableCollection<PersonModel> _lstPersonListCopy = new ObservableCollection<PersonModel>();  // Este tipo de Lista no es Observable

        // No se necesita en UI entonces no observable
        private List<PersonModel> _lstPersonPivot = new List<PersonModel>();  // Este tipo de Lista no es Observable y va a ser Pivot



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

        // Para no usar el boton en el filtro
        private string _TextoBuscar = String.Empty;

        public String TextoBuscar
        {
            get { return _TextoBuscar; }
            set {
                _TextoBuscar = value;
                onPropertyChanged("TextoBuscar");
                FiltrarPersonaOnTextChanged(_TextoBuscar);
            }
        }


        #endregion

        public PersonViewModel(){
            InitClass();
            InitCommands();
        }

        private void InitClass() {
            lstPersonList = PersonModel.ObtenerPersonas();
            _lstPersonPivot = lstPersonList.ToList();  // Asignar la lista Observable a la lista PIVOT
            Console.WriteLine("******** Cantidad de Datos en Lista -> Pivot="+_lstPersonPivot.Count()+",Observable="+lstPersonList.Count());
        }

        private void InitCommands() {
            AgregarPersonaCommand = new Command(AgregarPersona);
            BorrarPersonaCommand = new Command<int>(BorrarPersona);  // se debe indicar que se espera un int
            // Con nuevo metodo sin boton, usando Entry
        }

        private void FiltrarPersonaOnTextChanged(string textoBuscar)
        {

            lstPersonList.Clear(); // Esta sería la observable, la borro ya que solo necesito lo filtrado (Antes de esto debe guardarse los datos originales en la PIVOT)
            _lstPersonPivot.Where(x => x.nombre.ToLower().Contains(textoBuscar.ToLower())).ToList().ForEach(x=> lstPersonList.Add(x));
            
        }

        // El método que va a ir como objeto en el Command de arriba
        private void AgregarPersona() {
            
            _lstPersonList.Add(new PersonModel(1,"José Pablo", "Chaves","Alumno de curso Xamarin"));
            _lstPersonList.Add(new PersonModel(2,"Pablo", "Rivera", "Alumno no matriculado de curso Xamarin"));
            _lstPersonList.Add(new PersonModel(3,"Mónica", "Bermúdez", "Alumna de curso Xamarin"));
            _lstPersonList.Add(new PersonModel(4,"Carlos", "Méndez", "Profesor de curso Xamarin"));
            _lstPersonList.Add(new PersonModel(5,"Federico", "Brenes", "Alumno de curso VMWare"));

           // _lstPersonListCopy = _lstPersonList;
           // _lstPersonList.Add(new PersonModel{ nombre = NuevoIngreso, descripcion = "Alumno de Xamarin" });
            Console.WriteLine("Agregadas ->"+_lstPersonList.Count);            
        }

        private void BorrarPersona(int idPersona) {
            Console.WriteLine("Eliminando al usuario con ID-> "+idPersona);
            _lstPersonPivot.RemoveAll(x => x.id == idPersona);
            lstPersonList.Clear();
            lstPersonList = new ObservableCollection<PersonModel>(_lstPersonPivot);
            Console.WriteLine("*** Validacion Count [Lista Pivot="+_lstPersonPivot.Count()+",Lista Observable="+lstPersonList.Count()+"]");
        }

        /*
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

    */

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
