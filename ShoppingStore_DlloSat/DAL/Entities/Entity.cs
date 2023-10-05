using System.ComponentModel.DataAnnotations;

namespace ShoppingStore_DlloSat.DAL.Entities
{
    public class Entity
    {
        [Required]//DataAnnotations que signica que el campo es obligatorio
        public Guid Id { get; set; }// Primary Key

        [Display(Name = "Fecha de Creación")]//Display es para mostrar el nombre que quiero pintar en las vistas
        public DateTime? CreateDate { get; set; }//Signo Elvis (?) campo es Nuleable

        [Display(Name = "Fecha de Modificación")]
        public DateTime? ModifieDate { get; set; }
                
    }
}
