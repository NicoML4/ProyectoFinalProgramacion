using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
/*
    Abres el archivo → lees la línea → si es "00/00/0000", lo conviertes a DateTime.MinValue.
    Cuando abres un sobre, pones la fecha actual con DateTime.Now.
    Guardas la lista actualizada → ToString() convierte la fecha a "dd/MM/yyyy" o "00/00/0000" según corresponda.
*/

class Pokemon
{
    int id;
    string nombre;
    int vida;
    string tipo;
    //Ataque ataque1
    //Ataque ataque2
    DateTime fechaObtencion;
    bool aSidoObtenido;

    public int GetId()
    {
        return nombre;
    }
    public string GetNombre()
    {
        return nombre;
    }
    public int GetVida()
    {
        return vida;
    }
    public string GetTipo()
    {
        return tipo;
    }
    public DateTime GetFechaObtencion()
    {
        return fechaObtencion;
    }
    public bool GetASidoObtenido()
    {
        return aSidoObtenido;
    }
    public void SetId(int id)
    {
        this.id = id;
    }
    public void SetNombre(string nombre)
    {
        this.nombre = nombre;
    }
    public void SetVida(int vida)
    {
        this.vida = vida;
    }
    public void SetTipo(string tipo) 
    {
        this.tipo = tipo;
    }
    public void SetFechaObtencion(DateTime fechaObtencion)
    {
        this.fechaObtencion = fechaObtencion;
    }
    public void SetASidoObtenido(bool aSidoObtenido)
    {
        this.aSidoObtenido = aSidoObtenido;
    }
    public Pokemon(int id, string nombre, int vida, string tipo)
    {
        this.id = id;
        this.nombre = nombre;
        this.vida = vida;
        this.tipo = tipo;
        aSidoObtenido = false;
    }
    public override string ToString()
    {
        string fechaTexto = fechaObtencion == DateTime.MinValue ? "00/00/0000" : fechaObtencion.toString("dd/MM/yyyy") ;
        return $"{id};{nombre};{vida}Ps;{tipo}";
    }

}