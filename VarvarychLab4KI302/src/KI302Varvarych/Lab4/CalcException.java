package KI302Varvarych.Lab4;
/**
 * Class <code>CalcException</code> more precises ArithmeticException
 * @version 1.0
 */
class CalcException extends ArithmeticException {
    public CalcException() {}
    public CalcException(String cause) {
        super(cause);
    }
}

/**
 * Class <code>Equations</code> implements method for ((sin(x) / cos(x)) expression
 * calculation
 * @version 1.0
 */
class Equations {
    public double calculate(int x) throws CalcException {
        x = x % 360;


        double y, rad;
        rad = x * Math.PI / 180.0;

        try {
            y = Math.sin(rad) / Math.cos(rad);

            if (Double.isNaN(y) || Double.isInfinite(y) || x == 90 || x == -90 || x == 270 || x == -270) {
                throw new ArithmeticException();
            }
        } catch (ArithmeticException ex) {
            if (x == 90 || x == -90) {
                throw new CalcException("Exception reason: Illegal value of X for tangent calculation (X = " + x + ")");
            } else if (x == 270 || x == -270) {
                throw new CalcException("Exception reason: Illegal value of X for tangent calculation (X = " + x + ")");
            } else {
                throw new CalcException("Unknown reason of the exception during expression calculation");
            }
        }
        return y;
    }
}

