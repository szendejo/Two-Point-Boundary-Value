/********************** Programming Assignment #4 ***************************** 
* Programmer:  Stephanie Zendejo 
* 
* Course:  CSCI 3321
* 
* Date:  April 18, 2018
* 
* Assignment:  Numerical Solution of a Two-Point Boundary-Value Problem
* 
* Environment:  Visual Studio, C# 
* 
* Purpose:   To design and construct a computer program that will use both the secant
* and the Runge-Kutta method to obtain a numerical solution to the two-point 
* boundary-value problem.
* 
* Input: The following values x = 0.7, x = 1 are initial guesses for the secant
* method. The following value 1.0e-4  is the given tolerance error conditional. The step size
* for the Runge-Kutta method is h = 0.025, and the ODE used is f(t,x) = x + 0.09 x^2 + cos(10 t).
* The boundary condition used to calculate the function g in the secant method is 
* x(0) + x(1) - 3.0.
* 
* 
* Preconditions: The given function will be placed into the secant function and zi+1 (zn)
* will be calculated from the inital guesses. The secant method will call the g method which
* will return a calculated boundary condition value. Embedded in the g method return, the
* runge-kutta method rk3 will be called. The rk3 method will iterate from x = 0 to x = 1 40 
* times.
* 
* Output:  Printed on the console: values of zim1, zi, zip1, g(zip1), values of the function 
* from x = 0 to x = 1 using the given step size, and the found y value for y(0). 
* 
* Algorithm: 
* f
* defined by the given ODE
* returns f
* 
* secant
* defined by the secant method
* prints the values of zim1, zi, zip1, g(zip1)
* returns zip1
* 
* g
* calculates and returns boundary condition
* 
* rk3
* defined by the third order rk method
* iterates through x = 0 to x = 1
* returns y(1) value
* 
* k1 / k2 / k3
* defined by the third order rk method
* 
* Main
* Define exit = 0
* Define zim1, zi
* Print tpbv
* while exit != 1
*   calculate zn through secant method using initial values
*   calculate g(zn)
*   if g(zn) < tolerance value
*       print rk3 method for y(0) = zn to y(1)
*       exit while loop
*   else
*       zimi = zi
*       zi = zn
*           
***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TwoPointBoundaryValue {
    class Program {

        static double f(double x, double y) {
            double f = y + 0.09 * (y * y) + Math.Cos(10 * x);
            return f;
        }

        static double secant(double zim1, double zi) {
            double zn = zi - (g(zi) * (zi - zim1)) / (g(zi) - g(zim1));
            Console.WriteLine("zim1 = {0}\nzi = {1}\nzip1 = {2}\ng(zip1) = {3}\n", zim1, zi, zn, g(zn));
            return zn;
        }


        static double g(double x0) {
            return x0 + rk3(x0) - 3.0;
        }

        static double rk3(double x0) {
            double x = 0.0;
            double y = x0; 
            double ik1, ik2, ik3;
            do {
                ik1 = k1(x, y);
                ik2 = k2(x, y, ik1);
                ik3 = k3(x, y, ik1, ik2);
                y = y + (1.0 / 6.0) * (ik1 + 4.0 * ik2 + ik3) * 0.025;
                x = x + 0.025;
            } while (x < 1.025);
            return y;
        }

        static double znrk3(double x0) {
            Program rk3 = new Program();
            double x = 0.0;
            double y = x0;
            Console.WriteLine("y({0})= {1}", x, y);
            double ik1, ik2, ik3;
            for (int i = 1; i <= 40; i++) {
                ik1 = k1(x, y);
                ik2 = k2(x, y, ik1);
                ik3 = k3(x, y, ik1, ik2);
                y = y + (1.0 / 6.0) * (ik1 + 4.0 * ik2 + ik3) * 0.025;
                x = x + 0.025;
                Console.WriteLine("y({0})= {1}", x, y);
            }
            return y;
        }

        static double k1(double x, double y) {
            double k1 = f(x, y);
            return k1;
        }

        static double k2(double x, double y, double k1) {
            double k2 = f(x + 0.5 * 0.025, y + 0.5 * k1 * 0.025);
            return k2;
        }

        static double k3(double x, double y, double k1, double k2) {
            double k3 = f(x + 0.025, y - k1 * 0.025 + 2.0 * k2 * 0.025);
            return k3;
        }

        static void Main(string[] args) {
            double exit = 0;
            double zn;
            double zim1 = 0.7;
            double zi = 1.0;
            double gzn;
            Console.WriteLine("***** Two-Point Boundary-Value  *****");
            while(exit != 1) {
                zn = secant(zim1, zi);
                gzn = g(zn);
                if (Math.Abs(gzn) < 1.0e-4) {
                    znrk3(zn);
                    exit = 1;
                }
                else {
                    zim1 = zi;
                    zi = zn;
                }
            }
            Console.ReadKey();
        }
    }
}
