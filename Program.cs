/*Mad Chemist
 * Parses chemical formulas like H2O or NaCl and calculates their total molecular mass.
 * file PeriodicFormula.cs
 *
 * Created by Sarah Decker, Timmy Maggs, Brian Ly, Dorothy Bennet, and Bella Calvo with assistance from Professor Joshua Tallman
 */
using System;
class PeriodicFormula
{
 public static double CalculateMass(string molecule, PeriodicTable pt)
 {
 int a = 0; int z = 1;
 double totalMass = 0;
 //step 1 calculate number of atomic symbols
 int capitalsCount = 0;
 for (int i = 0; i < molecule.Length; i++)
 {
 char curr = molecule[i];
 if (char.IsUpper(curr))
 {
 capitalsCount++;
 }
 }
 //step 2 extract each SymbolNumber
 string[] atomIDs = new string[capitalsCount];
 int next = 0;

 while (a < molecule.Length)
 {
 if (molecule[a] =='(')
 {
 //(O2SCa)14
 while (z <= molecule.Length && molecule[z] != ')')
 { z++; }
 z++;
 //pull out number after ()
 while (z <= molecule.Length && char.IsNumber(molecule[z]))
 { z++; }
 }
 else
 {
 while (z < molecule.Length) // placed operations into a while loop and slightly adjusted logic
 {
 if (char.IsLower(molecule[z]) || char.IsNumber(molecule[z]))
 {
 z++;
 }
 else if (char.IsUpper(molecule[z]) || molecule[z] =='(')
 {
 break;
 }
 }
 }
 atomIDs[next] = molecule.Substring(a, z - a);

 next++;
 a = z;
 z = a + 1;
 }
 //step 3 separate symbol from number
 int[] counts = new int[capitalsCount];
 string[] symbols = new string[capitalsCount];
 int idx = 0;
 for (idx = 0; idx < atomIDs.Length; idx++)
 {
 string atomID = atomIDs[idx];
 if (atomID ==null ||atomID.Length == 0)
 {
 break;
 }
 int b = 0;
 if (atomID[0] == '(')
 {
 while (b < atomID.Length && atomID[b] != ')')
 {
 b++;
 }
 b++;//acount for close )
 }
 else
 {
 while (b < atomID.Length && !char.IsNumber(atomID[b]))
 {
 b++;
 }
 }
 symbols[idx] = atomID.Substring(0, b);
 if (b < atomID.Length)
 {
 string temp = atomID.Substring(b);
 counts[idx] = int.Parse(temp);
 }
 else
 {
 counts[idx] = 1;
 }


 }
 // step 4 calculate masses
 double[] masses = new double[capitalsCount];
 for (int i = 0; i < masses.Length; i++)
 {
 string symbol = symbols[i];
 if (symbol == null|| symbol.Length == 0)
 {
 break;
 }
 try
 {
 double mass = 0;
 if (symbol[0] == '(')
 {
 //(O2SCa)
 symbol = symbol.Substring(1, symbol.Length - 2);
 mass = CalculateMass(symbol,pt);
 }
 else
 {
 mass = pt.GetAtomicMass(symbol);
 }
 masses[i] = mass * counts[i];

 }
 catch (ArgumentException e)
 {
 Console.WriteLine(e.Message);
 throw e;
 }
 totalMass += masses[i];
 }
 return totalMass;
 }
}
