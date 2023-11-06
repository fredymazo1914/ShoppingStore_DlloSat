using System.ComponentModel.DataAnnotations;

namespace ShoppingStore_DlloSat.DAL.Entities
{
    public class City : Entity
    {
        [Display(Name = "Ciudad")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]//Controlar el máximo de caracteres

        [Required(ErrorMessage = "¡El campo {0} es requerido! ")]
        public string Name { get; set; }

        //Relación con State
        public State State { get; set; }
    }
}
