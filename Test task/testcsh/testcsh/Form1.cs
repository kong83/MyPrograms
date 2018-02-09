using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace testcsh
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      try
      {
        dataSet1.ReadXml(Application.StartupPath + "\\TestBD.xml");
        dataSet1.AcceptChanges();
      }
			catch 
      {
        MessageBox.Show("Ошибка при загрузке базы данных: " + Application.StartupPath + "\\TestBD.xml");
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
 			dataSet1.AcceptChanges();
      dataSet1.WriteXml(Application.StartupPath + "\\TestBD.xml");
    }

    private void button1_Click(object sender, EventArgs e)
    {
      DataGridViewRowCollection drc = dataGridView1.Rows;
      DataGridViewRow dr = drc[dataGridView1.CurrentCellAddress.Y];

      // Получение общей суммы платежей абонента
      // Получение значения логина для выбранной строки в Customers
      string login = dr.Cells[1].Value.ToString();
      // Получение представления таблицы Payments
      DataView payments = new DataView(dataSet1.Tables["Payments"]);
      // Установка фильтра для отбора тех значений, которые относятся к нашему абоненту
      payments.RowFilter = "customer = '" + login + "'";
      // Посчет суммы платежей нашего абонента
      double sum = 0;
      foreach (DataRowView dRow in payments)
        sum += Convert.ToDouble(dRow["the_sum"].ToString());
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select sum(the_summ)
       * from Payments
       * where :login = customer
       * Результат запишем в переменную sum
      */

      // Получение суммы длительностей разговоров
      // Получение представления таблицы Phones
      DataView phones = new DataView(dataSet1.Tables["Phones"]);
      // Получение номера телефона, который зарегистрирован за нашим абонентом
      phones.RowFilter = "cid = '" + login + "'";
      // Если его нет - то ошибка
      if (phones.Count == 0)
      {
        MessageBox.Show("Ошибка. У абонента "+login+" не зарегистрирован телефон.");
        return;
      }
      string phone = phones[0].Row["phone"].ToString();
      // Получение представления таблицы Calls
      DataView calls = new DataView(dataSet1.Tables["Calls"]);
      // Оставление звонков с найденным номером
      calls.RowFilter = "the_number = '" + phone + "'";
      // Подсчет общего количества проговоренных минут
      double sumDuration = 0;
      foreach (DataRowView dRow in calls)
        sumDuration += Convert.ToDouble(dRow["duration"].ToString());
      // Если наш абонент владеет транком, то сумма будет нулем, в этом случае 
      // ищем сумму разговоров через таблицу Trunks
      if (sumDuration == 0)
      {
        // Получение представления таблицы Trunks
        DataView trunks = new DataView(dataSet1.Tables["Trunks"]);
        // Получение номера телефона, который зарегистрирован за нашим абонентом
        trunks.RowFilter = "customer = '" + login + "'";
        // Если у нашего абонента нет транка, то sumDuration не пересчитывается
        // (возможно он еще вообще не звонил)
        if (trunks.Count == 0)
          goto ex;
        int trunk = Convert.ToInt32(trunks[0].Row["trunk"].ToString());                
        // Оставление звонков с найденным номером
        calls.RowFilter = "trunk = " + trunk.ToString();
        // Подсчет общего количества проговоренных минут
        sumDuration = 0;
        foreach (DataRowView dRow in calls)
          sumDuration += Convert.ToDouble(dRow["duration"].ToString());     
        trunks.Dispose();
      }
ex:
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select sum(c.duration)
       * from Calls c, Phones ph
       * where :login = ph.cid && c.the_number = ph.phone
       * 
       * 
       * select sum(c.duration)
       * from Calls c, Trunks t
       * where :login = t.customer && t.trunk = c.trunk       
       * Результатом будет наибольшая из сумм
       * Запишем ее в переменную sumDuration
      */

      // Получение стоимости минуты разговора
      // Получение кода тарифа нашего абонента
      int code = Convert.ToInt32(dr.Cells[7].Value.ToString());
      // Получение представления таблицы Tarif
      DataView tarifs = new DataView(dataSet1.Tables["Tarif"]);
      //  Оставление записи с указанным кодом
      tarifs.RowFilter = "code = "+ code.ToString();
      // Получение стоимости минуты разговора
      double cost = Convert.ToDouble(tarifs[0].Row["cost"].ToString());
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select cost
       * from Tarif
       * where :tarif_id = code
       * Результат запишем в переменную cost
      */

      // Получение налога
      // Получение кода тарифа нашего абонента
      int tax = Convert.ToInt32(dr.Cells[4].Value.ToString());
      // Получение представления таблицы Tarif
      DataView taxs = new DataView(dataSet1.Tables["Tax"]);
      //  Оставление записи с указанным кодом
      taxs.RowFilter = "code = " + tax.ToString();
      // Получение стоимости минуты разговора
      double tax_value = Convert.ToDouble(taxs[0].Row["tax_value"].ToString());
      /*
       * Аналогичный запрос на sql выглядел бы так:
       * select tax_value
       * from Tax
       * where :tax = code
       * Результат запишем в переменную tax
      */

      // Установка нового баланса (из суммы внесенных средств вычитается налог)
      dr.Cells[3].Value = sum - (sum * tax_value / 100) - sumDuration * cost;      

      phones.Dispose();
      tarifs.Dispose();
      calls.Dispose();
      taxs.Dispose();
      payments.Dispose();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      CallsList callList = new CallsList(this);
      callList.ShowDialog();
      callList.Dispose();
      dataSet1.RejectChanges();
    }
  }
}