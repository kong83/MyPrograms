using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PassStore
{
  public partial class PassForm : Form
  {
    MainForm mainForm;

    public PassForm(MainForm mf)
    {
      InitializeComponent();

      mainForm = mf;
      labelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
    }

    /// <summary>
    /// Обработчик нажатия на кнопку OK
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OK_Click(object sender, EventArgs e)
    {
      mainForm.PassStr = textPass.Text;
      this.Close();
    }

    /// <summary>
    /// Обработчик нажатия на кнопку Cancel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Cancel_Click(object sender, EventArgs e)
    {      
      this.Close();
    }

    #region Подсказки
    private void buttonOK_MouseEnter(object sender, EventArgs e)
    {
      toolTip1.Show("Подтвердить ввод пароля", buttonOK, 15, -17);
      buttonOK.FlatStyle = FlatStyle.Popup;
    }
    private void buttonOK_MouseLeave(object sender, EventArgs e)
    {
      toolTip1.Hide(buttonOK);
      buttonOK.FlatStyle = FlatStyle.Flat;
    }
    private void buttonDelete_MouseEnter(object sender, EventArgs e)
    {
      toolTip1.Show("Отказаться от ввода пароля", buttonClose, 15, -17);
      buttonClose.FlatStyle = FlatStyle.Popup;
    }
    private void buttonDelete_MouseLeave(object sender, EventArgs e)
    {
      toolTip1.Hide(buttonClose);
      buttonClose.FlatStyle = FlatStyle.Flat;
    }
    #endregion

    /// <summary>
    /// Отлов нажатия кнопок на форме
    /// </summary>
    /// <param name="keyData"></param>
    /// <returns></returns>
    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Enter)
      {
        OK_Click(null, null);
        return true;
      }
      else if (keyData == Keys.Escape)
      {
        Cancel_Click(null, null);
        return true;
      }
      return base.ProcessDialogKey(keyData);
    }

    private void PassForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
    {
      labelLang.Text = InputLanguage.CurrentInputLanguage.Culture.TwoLetterISOLanguageName;
    }
  }
}
