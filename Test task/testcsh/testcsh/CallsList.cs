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
        MessageBox.Show("������. ������� ������� ����.");
        return;
      }
      DataGridViewRowCollection drc = f1.dataGridView1.Rows;
      DataGridViewRow dr = drc[f1.dataGridView1.CurrentCellAddress.Y];

      // ��������� �������� ������ ��� ��������� ������ � Customers
      string login = dr.Cells[1].Value.ToString();

      // ��������� ��������� ������ ���������
      // ��������� ���� ������ ������ ��������
      int code = Convert.ToInt32(dr.Cells[7].Value.ToString());
      // ��������� ������������� ������� Tarif
      DataView tarifs = new DataView(f1.dataSet1.Tables["Tarif"]);
      //  ���������� ������ � ��������� �����
      tarifs.RowFilter = "code = " + code.ToString();
      // ��������� ��������� ������ ���������
      double cost = Convert.ToDouble(tarifs[0].Row["cost"].ToString());
      /*
       * ����������� ������ �� sql �������� �� ���:
       * select cost
       * from Tarif
       * where :tarif_id = code
       * ��������� ������� � ���������� cost
      */

      // ��������� ������������� ������� Phones
      DataView phones = new DataView(f1.dataSet1.Tables["Phones"]);
      // ��������� ������ ��������, ������� ��������������� �� ����� ���������
      phones.RowFilter = "cid = '" + login + "'";
      // ���� ��� ��� - �� ������
      if (phones.Count == 0)
      {
        MessageBox.Show("������. � �������� " + login + " �� ��������������� �������.");
        return;
      }
      string phone = phones[0].Row["phone"].ToString();
      // ��������� ������������� ������� Calls
      DataView calls = new DataView(f1.dataSet1.Tables["Calls"]);
      // ���������� ������� � ��������� �������
      calls.RowFilter = "the_number = '" + phone + "'";
      if (calls.Count == 0)
      { 
       // ��������� ������������� ������� Trunks
        DataView trunks = new DataView(f1.dataSet1.Tables["Trunks"]);
        // ��������� ������ ��������, ������� ��������������� �� ����� ���������
        trunks.RowFilter = "customer = '" + login + "'";
        // ���� � ������ �������� ��� ������, �� sumDuration �� ���������������
        // (�������� �� ��� ������ �� ������)
        if (trunks.Count == 0)
          goto ex;
        int trunk = Convert.ToInt32(trunks[0].Row["trunk"].ToString());                
        // ���������� ������� � ��������� �������
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
       * ����������� ������ �� sql �������� �� ���:
       * select count(c.*)
       * from Calls c, Phones ph
       * where ph.cid = :login && c.the_number = ph.phone
       * 
       * ���� count(c.*) = 0, ��
       * select c.the_date, (c.duration * cost) as price, c.CallerID
       * from Calls c, Trunks t
       * where c.trunk = t.trunk && t.customer = :login
       * 
       * � ��������� ������
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
     *  ��� ������� �� ������������� � �������� ��� ������� ����, ��� ��� ����� ��
     * ��������� ��� ������������� sql ��������.     
     */
  }
}