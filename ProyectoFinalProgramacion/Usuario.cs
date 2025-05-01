using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class Usuario
    {
        string nombreUsuario;
        string contrasena;

        public Usuario(string nombreUsuario, string contrasena)
        { 
            this.nombreUsuario = nombreUsuario;
            this.contrasena = contrasena;
        }

        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }

        public override bool Equals(object? obj)
        {
            return obj is Usuario usuario &&
                   nombreUsuario == usuario.nombreUsuario &&
                   contrasena == usuario.contrasena &&
                   NombreUsuario == usuario.NombreUsuario &&
                   Contrasena == usuario.Contrasena;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(nombreUsuario, contrasena, NombreUsuario, Contrasena);
        }

        public override string ToString()
        {
            return nombreUsuario + " " + contrasena;
        }
    }
}
