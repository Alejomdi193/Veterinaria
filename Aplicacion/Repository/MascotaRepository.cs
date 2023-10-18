using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interface;
using Persistencia;

namespace Aplicacion.Repository
{
    public class MascotaRepository : GenericRepository<Mascota>, IMascota
    {
        public MascotaRepository(VeterinariaContext context) : base(context)
        {
        }
    }
}