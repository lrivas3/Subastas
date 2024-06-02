using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Subastas.Domain
{
    public partial class Subasta
    {
        public int IdSubasta { get; set; }

        [Display(Name = "Título de la Subasta")]
        [Required(ErrorMessage = "El título es obligatorio")]
        public string TituloSubasta { get; set; } = null!;

        [Display(Name = "Monto Inicial")]
        [Range(0, double.MaxValue, ErrorMessage = "El monto inicial debe ser un número positivo")]
        [Required(ErrorMessage = "El monto inicial es obligatorio")]
        public decimal MontoInicial { get; set; }

        [Display(Name = "Fecha de la Subasta")]
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha no válido")]
        [Required(ErrorMessage = "La fecha de la subasta es obligatoria")]
        public DateTime FechaSubasta { get; set; }

        [Display(Name = "Fecha de Finalización de la Subasta")]
        [DataType(DataType.DateTime, ErrorMessage = "Formato de fecha no válido")]
        [Required(ErrorMessage = "La fecha de finalización de la subasta es obligatoria")]
        public DateTime FechaSubastaFin { get; set; }

        [Display(Name = "Subasta finalizada")]
        public bool Finalizada { get; set; }

        [Display(Name = "Subasta activa")]
        public bool EstaActivo { get; set; }

        public int? IdUsuario { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El producto es obligatorio")]
        public int IdProducto { get; set; }

        public virtual Producto? IdProductoNavigation { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }

        public virtual ICollection<ParticipantesSubasta> ParticipantesSubasta { get; set; } = new List<ParticipantesSubasta>();

        public virtual ICollection<Puja> Pujas { get; set; } = new List<Puja>();
    }
}
