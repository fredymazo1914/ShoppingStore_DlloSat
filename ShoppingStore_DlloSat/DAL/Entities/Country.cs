using System.ComponentModel.DataAnnotations;

namespace ShoppingStore_DlloSat.DAL.Entities
{
    //Primera entidad que se convertirá en tabla de la BD
    public class Country : Entity //Hereda de Entity
    {
        [Display(Name = "País")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]//Controlar el máximo de caracteres

        [Required(ErrorMessage = "¡El campo {0} es requerido! ")]
        public string Name { get; set; }

        //Relación 1-N State to Country
        public ICollection<State> States { get; set;}

        [Display(Name = "Estados/Departamentos")]
        //Esto es una propiedad de lectura que sirve para contar los estados de un país y se valida que no sean nulos con if ternario
        public int StateNumber => (States == null ? 0 : States.Count);
    }
}
