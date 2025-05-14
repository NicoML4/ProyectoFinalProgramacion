using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinalProgramacion;

/*
    Abres el archivo → lees la línea → si es "00/00/0000", lo conviertes a DateTime.MinValue.
    Cuando abres un sobre, pones la fecha actual con DateTime.Now.
    Guardas la lista actualizada → ToString() convierte la fecha a "dd/MM/yyyy" o "00/00/0000" según corresponda.
*/

class Pokemon
{
    private int id;
    private string nombre;
    private int vida;
    private string tipo;
    private Ataque ataque1;
    private Ataque ataque2;
    private DateTime? fechaObtencion; //fecha.hasvalue  
    private string asset;
    public Pokemon(int id, string nombre, int vida, string tipo,string ataque1,string ataque2,DateTime? fechaObtencion,string asset)
        {
            this.id = id;
            this.nombre = nombre;
            this.vida = vida;
            this.tipo = tipo;
            this.ataque1 = new Ataque(ataque1);
            this.ataque2 = new Ataque(ataque2);
            this.fechaObtencion = fechaObtencion;
            this.asset = asset;
        }

    public int GetId()
    {
        return id;
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
    public DateTime? GetFechaObtencion()
    {
        return fechaObtencion;
    }
    public Ataque GetAtaque1()
    { 
        return ataque1;
    }
    public Ataque GetAtaque2()
    {
        return ataque2;
    }
    public string getAsset()
    {
        return asset;
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
    public void SetFechaObtencion(DateTime? fechaObtencion)
    {
        this.fechaObtencion = fechaObtencion;
    }
    public void SetAtaque1(string ataque1)
    { 
        this.ataque1 = new Ataque(ataque1);
    }
    public void SetAtaque2(string ataque2)
    {
        this.ataque2 = new Ataque(ataque2);
    }
    public void setAssset(string enlace)
    {
        asset = enlace;
    }
}