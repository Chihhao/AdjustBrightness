using System;
using System.Diagnostics;

namespace AdjustBrightness{
    class Program{

        static BrightnessControl brightnessControl;

        static void Main(string[] args){ 
            
            if(args.Length != 2) {
                Console.WriteLine("First argument is monitor id, ex: 0, 1 or 2");
                Console.WriteLine("Second argument is increasement of brightness, ex: +4, +10, -5");
                Console.WriteLine("Ex: AdjustBrightness.exe 0 +10");
                Console.WriteLine("Ex: AdjustBrightness.exe 1 -5");
                return;
            }

            int iMonitorId = 0;
            if (!int.TryParse(args[0], out iMonitorId)) {
                Console.WriteLine($"args 1 is not a number: {args[0]}");
                return;
            }

            int iAdjustment = 0;
            if (!int.TryParse(args[1], out iAdjustment)) {
                Console.WriteLine($"args 2 is not a number: {args[1]}");
                return;
            }
            
            try {
                IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
                brightnessControl = new BrightnessControl(hWnd);

                var mInfo = brightnessControl.GetBrightnessCapabilities(iMonitorId);

                var target = mInfo.current + iAdjustment;
                if(target > mInfo.maximum) {
                    target = mInfo.maximum;
                }
                else if (target < mInfo.minimum) {
                    target = mInfo.minimum;
                }               

                brightnessControl.SetBrightness(Convert.ToInt16(target), iMonitorId);
            }
            catch (Exception ex ) {
                Console.WriteLine("Exception: " + ex.Message);
            }

        }

    }
}
