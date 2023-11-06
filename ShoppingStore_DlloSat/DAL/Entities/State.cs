using System.ComponentModel.DataAnnotations;

namespace ShoppingStore_DlloSat.DAL.Entities
{
    public class State : Entity
    {
        [Display(Name = "Estado/Departamento")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]//Controlar el máximo de caracteres

        [Required(ErrorMessage = "¡El campo {0} es requerido! ")]
        public string Name { get; set; }

        //Relación 1-N State to Country
        public Country Country { get; set; }

        //Relación 1-N City to State
        public ICollection<City> Cities { get; set; }

        [Display(Name = "Ciudades")]
        //Esto es una propiedad de lectura que sirve para contar las ciuadades de un estado y se valida que no sean nulos con if ternario
        public int CityNumber => (Cities == null ? 0 : Cities.Count);
    }
}
