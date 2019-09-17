using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAlfaLive.Domain.Enums
{
    public enum Roles
    {
        [Display (Name = "Super Administrador")]
        SuperAdministrador = 1,

        [Display (Name = "Administrador Alfa Careers")]
        AdministradorCareers = 9,

        [Display (Name = "Administrador Meet and Greet")]
        AdministradorMeetG = 2,

        [Display (Name = "Administrador Ask Us Anything")]
        AdministradorAskUs = 10,

        [Display (Name = "Agente Alfa Careers")]
        AgenteCareers = 4, 

        [Display (Name = "Agente Meet and Greet")]
        AgenteMeetG = 5,

        [Display (Name = "Agente Ask Us Anything")]
        AgenteAskUs = 6
    }
}
