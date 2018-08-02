using System;
using System.Windows.Forms;

using SurgeryHelper.Tools;

namespace SurgeryHelper.Forms
{
    public partial class NegatoscopForm : Form
    {
        private Ramp _currentRamp;

        public NegatoscopForm()
        {
            InitializeComponent();

            _currentRamp = new Ramp 
            {
                Red = new ushort[256], 
                Green = new ushort[256], 
                Blue = new ushort[256]
            };
            Win32Engine.GetDeviceGammaRamp(Win32Engine.GetDC(Handle), ref _currentRamp);
            
            var newRamp = new Ramp 
            {
                Red = new ushort[256], 
                Green = new ushort[256], 
                Blue = new ushort[256]
            };
            for (int i = 1; i < 256; i++)
            {
                newRamp.Red[i] = newRamp.Green[i] = newRamp.Blue[i] =
                    (ushort)(Math.Min(65535, Math.Max(0, Math.Pow((i + 1) / 256.0, 44 * 0.1) * 65535 + 0.5)));
            }

            Win32Engine.SetDeviceGammaRamp(Win32Engine.GetDC(Handle), ref newRamp);
        }

        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData">Нажатая клавиша</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            Close();

            return true;
        }

        private void NegatoscopForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Win32Engine.SetDeviceGammaRamp(Win32Engine.GetDC(Handle), ref _currentRamp);
        }
    }
}
