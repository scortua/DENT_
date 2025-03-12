// See https://aka.ms/new-console-template for more information
// with this code you can print "Hello, World!" in the console
using ConsoleApp1;

Console.WriteLine("Hello, World!");
Marcador M_Azul;
Marcador M_Negro;
M_Azul = new Marcador("AZUL");
M_Negro = new Marcador("NEGRO");
M_Negro.Color = M_Azul.Tapa ? "NEGRO" : "ROJO";
M_Azul.Destapar();
M_Negro.Evento += Funcion;
M_Negro.Evento += Funcion;
M_Negro.Evento -= Funcion;
//M_Negro.Evento -= Funcion;
M_Negro.Escribir("Primer uso del objeto Negro");
M_Azul.Escribir("Primer uso del objeto");
M_Negro.Destapar();
M_Negro.Escribir("Segundo Objeto, Propiedad Color = " + M_Negro.Color + ", Estado del Marcador: " + M_Negro.Estado);
M_Negro = M_Azul;
M_Negro.Escribir("Objeto referenciado en M_Negro");
M_Negro.Tapar();
M_Azul.Escribir("Escritura con M_Azul");

Console.WriteLine("Metodo ToString: " + M_Azul.ToString());

static void Funcion(int x, int y)
{
    Console.WriteLine(x + " + " + y + " = " + (x + y));
}

