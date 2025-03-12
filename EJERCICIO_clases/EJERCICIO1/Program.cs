// MADE BY: SANTIAGO CORTES TOVAR
using COORDENADAS;
Console.WriteLine("Programa de coordenadas:\n");
coordenadas A, B;
A = new coordenadas(5, 4);
B = new coordenadas();
Console.WriteLine("A: " + A.X + "," + A.Y);
Console.WriteLine("Norma de A: " + A.norma(A));
Console.WriteLine("Angulo de A: " + A.angulo(A));
Console.WriteLine("B: " + B.X + "," + B.Y);
Console.WriteLine("Norma de B: " + B.norma(B));
Console.WriteLine("Angulo de B: " + B.angulo(B));
coordenadas C = A + B;
Console.WriteLine("C: A + B: " + C.X + "," + C.Y);
Console.WriteLine("Norma de C: " + C.norma(C));
Console.WriteLine("Angulo de C: " + C.angulo(C));
coordenadas D = A * B;
Console.WriteLine("D: A * B" + D.X + "," + D.Y);
Console.WriteLine("Norma de D: " + D.norma(D));
Console.WriteLine("Angulo de D: " + D.angulo(D));
coordenadas E = A - B;
Console.WriteLine("E: A - B" + E.X + "," + E.Y);
Console.WriteLine("Norma de E: " + E.norma(E));
Console.WriteLine("Angulo de E: " + E.angulo(E));
Console.WriteLine("\nFin del programa");


