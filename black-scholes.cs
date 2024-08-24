using System;

class BlackScholes
{
    // This function computes the cumulative normal distribution function (CDF)
    // using the approximation from Abramowitz and Stegun.
    public static double CumulativeNormalDistribution(double x)
    {
        // Determine the sign of x
        double sign = x < 0 ? -1 : 1;
        // Convert x to a positive value and scale
        x = Math.Abs(x) / Math.Sqrt(2.0);

        // Constants used in the approximation
        double t = 1.0 / (1.0 + 0.3275911 * x);
        double y = 1.0 - (((((1.061405429 * t - 1.453152027) * t) + 
                           1.421413741) * t - 0.284496736) * t + 
                           0.254829592) * t * Math.Exp(-x * x);

        // Apply the sign to the result and return
        return 0.5 * (1.0 + sign * y);
    }

    // This function calculates the price of a European call option
    // using the Black-Scholes formula.
    public static double BlackScholesCall(double S, double K, double T, double r, double sigma)
    {
        // Calculate d1 and d2
        double d1 = (Math.Log(S / K) + (r + 0.5 * sigma * sigma) * T) / 
                    (sigma * Math.Sqrt(T));
        double d2 = d1 - sigma * Math.Sqrt(T);
        
        // Calculate the call option price using the Black-Scholes formula
        return S * CumulativeNormalDistribution(d1) - 
               K * Math.Exp(-r * T) * CumulativeNormalDistribution(d2);
    }

    // This function calculates the price of a European put option
    // using the Black-Scholes formula.
    public static double BlackScholesPut(double S, double K, double T, double r, double sigma)
    {
        // Calculate d1 and d2
        double d1 = (Math.Log(S / K) + (r + 0.5 * sigma * sigma) * T) / 
                    (sigma * Math.Sqrt(T));
        double d2 = d1 - sigma * Math.Sqrt(T);
        
        // Calculate the put option price using the Black-Scholes formula
        return K * Math.Exp(-r * T) * CumulativeNormalDistribution(-d2) - 
               S * CumulativeNormalDistribution(-d1);
    }

    // The Main function is the entry point of the program.
    static void Main(string[] args)
    {
        // Prompt user for input values
        Console.Write("Enter the current stock price (S): ");
        double S = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the strike price (K): ");
        double K = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the time to maturity in years (T): ");
        double T = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the risk-free interest rate (r) as a decimal: ");
        double r = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the volatility of the stock (sigma) as a decimal: ");
        double sigma = Convert.ToDouble(Console.ReadLine());

        // Calculate the call and put option prices
        double callPrice = BlackScholesCall(S, K, T, r, sigma);
        double putPrice = BlackScholesPut(S, K, T, r, sigma);

        // Output the results to the console
        Console.WriteLine("Call option price: " + callPrice);
        Console.WriteLine("Put option price: " + putPrice);
    }
}
