using Lab1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Lab1.View;

namespace Lab1.ViewModel
{
    // El INotifyPropertyChanged le notifica a la capa VIEW que algo ha cambiado
    public class PersonViewModel : INotifyPropertyChanged
    {
        #region Singleton
        private static PersonViewModel instance = null;

        public static PersonViewModel GetInstance() {
            if (instance == null)
            {
                instance = new PersonViewModel();
            }
            return instance;              
        }

        public static void DeleteInstance() {
            if (instance != null) {
                instance = null;
            } 
        }

        #endregion Singleton

        private PersonViewModel()
        {
            InitClass();
            InitCommands();
        }


        #region Instances

        private PersonModel _PersonaActual { get; set; }

        public PersonModel PersonaActual
        {
            get
            {
                return _PersonaActual;
            }
            set
            {
                _PersonaActual = value;
                onPropertyChanged("PersonaActual");
            }
        }



        // Un Interface Command -> Para poder hacer el Bind del componente UI den MainPage.xaml
        public ICommand AgregarPersonaCommand { get; set; }
        public ICommand FiltrarPersonaCommand { get; set; }
        public ICommand TextoBuscarCommand { get; set; }

        public ICommand BorrarPersonaCommand { get; set; }

        public ICommand VerPersonaCommand { get; set; }


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


        private void InitClass() {
            lstPersonList = PersonModel.ObtenerPersonas();
            _lstPersonPivot = lstPersonList.ToList();  // Asignar la lista Observable a la lista PIVOT
            Console.WriteLine("******** Cantidad de Datos en Lista -> Pivot="+_lstPersonPivot.Count()+",Observable="+lstPersonList.Count());
        }

        private void InitCommands() {
            AgregarPersonaCommand = new Command(AgregarPersona);
            BorrarPersonaCommand = new Command<int>(BorrarPersona);  // se debe indicar que se espera un int
            VerPersonaCommand = new Command<int>(VerPersona);  // se debe indicar que se espera un int
            // Con nuevo metodo sin boton, usando Entry
        }

        private void FiltrarPersonaOnTextChanged(string textoBuscar)
        {

            lstPersonList.Clear(); // Esta sería la observable, la borro ya que solo necesito lo filtrado (Antes de esto debe guardarse los datos originales en la PIVOT)
            _lstPersonPivot.Where(x => x.nombre.ToLower().Contains(textoBuscar.ToLower())).ToList().ForEach(x=> lstPersonList.Add(x));
            
        }

        // El método que va a ir como objeto en el Command de arriba
        private void AgregarPersona() {
            
            _lstPersonList.Add(new PersonModel { id = 1, nombre = "Federico" , apellido1 = "Brenes", apellido2 = "Rueda", telefono = "88995656", fechaNacimiento = new DateTime(1970,1,1), direccion = "Cartago, Paraíso", sexo = "M", observaciones="Alumno de VMWare"});
            _lstPersonList.Add(new PersonModel { id = 1, nombre = "José Pablo", apellido1 = "Chaves", apellido2 = "Arias", telefono = "87069998", fechaNacimiento = new DateTime(1987, 8, 26), direccion = "San José, Coronado", sexo = "M", observaciones = "Alumno de Xamarin" });
            _lstPersonList.Add(new PersonModel { id = 1, nombre = "Mónica", apellido1 = "Bermúdez", apellido2 = "García", telefono = "88995656", fechaNacimiento = new DateTime(1985, 1, 1), direccion = "Heredia, San Francisco", sexo = "F", observaciones = "Alumna de Xamarin" });
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

        private void VerPersona(int idPersona)
        {
            PersonaActual = _lstPersonPivot.Where(x => x.id == idPersona).FirstOrDefault();
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new FormPage());
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
