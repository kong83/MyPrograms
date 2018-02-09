using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace testcsh
{
  public partial class CallsList : Form
  {    
    public CallsList(Form1 f1)
    {
      InitializeComponent();

      try
      {
        Convert.ToDateTime(f1.textBox1.Text);
        Convert.ToDateTime(f1.textBox2.Text);
      }
      catch (FormatException )
      {
        MessageBox.Show("Ошибка. Неверно указаны даты.");
        return;
      }
      DataGridViewRowCollection drc = f1.dataGridView1.Rows;
      DataGridViewRow dr = drc[f1.dataGridView1.CurrentCellAddress.Y];

      // Получение значения логина для выбранной строки в Customers
      string login = dr.Cells[1].Value.ToString();

      // Получение стоимости минуты разговора
      // Получение кода тарифа нашего абонента
      int code = Convert.ToInt32(dr.Cells[7].Value.ToString());
      // Получение представления таблицы Tarif
      DataView tarifs = new DataView(f1.dataSet1.Tables["Tarif"]);
      //  Оставление записи с указанным кодом
      tarifs.RowFilter = "code = " + code.ToString();
      // Получение стоимости минуты разговора
      double cost = Convert.ToDouble(tarifs[0].Row["cost"].ToString());
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select cost
       * from Tarif
       * where :tarif_id = code
       * Результат запишем в переменную cost
      */

      // Получение представления таблицы Phones
      DataView phones = new DataView(f1.dataSet1.Tables["Phones"]);
      // Получение номера телефона, который зарегистрирован за нашим абонентом
      phones.RowFilter = "cid = '" + login + "'";
      // Если его нет - то ошибка
      if (phones.Count == 0)
      {
        MessageBox.Show("Ошибка. У абонента " + login + " не зарегистрирован телефон.");
        return;
      }
      string phone = phones[0].Row["phone"].ToString();
      // Получение представления таблицы Calls
      DataView calls = new DataView(f1.dataSet1.Tables["Calls"]);
      // Оставление звонков с найденным номером
      calls.RowFilter = "the_number = '" + phone + "'";
      if (calls.Count == 0)
      { 
       // Получение представления таблицы Trunks
        DataView trunks = new DataView(f1.dataSet1.Tables["Trunks"]);
        // Получение номера телефона, который зарегистрирован за нашим абонентом
        trunks.RowFilter = "customer = '" + login + "'";
        // Если у нашего абонента нет транка, то sumDuration не пересчитывается
        // (возможно он еще вообще не звонил)
        if (trunks.Count == 0)
          goto ex;
        int trunk = Convert.ToInt32(trunks[0].Row["trunk"].ToString());                
        // Оставление звонков с найденным номером
        calls.RowFilter = "trunk = " + trunk.ToString();        
      }
    ex:
      calls.RowFilter += " and the_date>='" + Convert.ToDateTime(f1.textBox1.Text) + "' and the_date<='" + Convert.ToDateTime(f1.textBox2.Text) + "'";
      double sumCalls = 0;
      foreach (DataRowView dRow in calls)
      {
        dRow["duration"] = Convert.ToDouble(dRow["duration"].ToString()) * cost;
        sumCalls += Convert.ToDouble(dRow["duration"].ToString());
      }
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select count(c.*)
       * from Calls c, Phones ph
       * where ph.cid = :login && c.the_number = ph.phone
       * 
       * Если count(c.*) = 0, то
       * select c.the_date, (c.duration * cost) as price, c.CallerID
       * from Calls c, Trunks t
       * where c.trunk = t.trunk && t.customer = :login
       * 
       * в противном случае
       * 
       * select c.the_date, (c.duration * cost) as price, c.CallerID
       * from Calls c, Phones ph
       * where c.the_number = ph.phone && ph.cid = :login       
      */

      dataGridView1.DataMember = "";
      dataGridView1.DataSource = calls;
      dataGridView1.Columns[0].Visible = false;
      dataGridView1.Columns[2].Visible = false;
      dataGridView1.Columns[3].HeaderText = "cost";
      textBox1.Text = sumCalls.ToString();
    }

    private void button1_Click(object sender, EventArgs e)
    {      
      this.Close();
    }
    /*
     *  Все запросы не тестировались и писались для примера того, как это могло бы
     * выглядеть при использовании sql запросов.     
     */
  }
}