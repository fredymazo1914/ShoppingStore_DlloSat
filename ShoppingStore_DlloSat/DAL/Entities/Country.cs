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
    }
}
