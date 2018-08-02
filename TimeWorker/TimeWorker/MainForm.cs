using System;
using System.Windows.Forms;

namespace TimeWorker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            textBox_TextChanged(null, null);
            comboBoxSign.Text = "+";
            comboBoxSign2.Text = "-";
            dateTimePickerArriveTime1.Value = dateTimePickerArriveTime2.Value =
            dateTimePickerReturnTime1.Value = dateTimePickerReturnTime2.Value =
            dateTimePickerFirst.Value = dateTimePickerResult.Value = DateTime.Now;
            dateTimePickerSecond.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }

        private void buttonGetTime_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimePickerReturnTime1.Value < dateTimePickerArriveTime1.Value)
                {
                    throw new Exception("Время возвращения первого игрока должно быть больше, чем время прибытия");
                }

                var tsArrive = new TimeSpan(dateTimePickerArriveTime1.Value.Day, dateTimePickerArriveTime1.Value.Hour, dateTimePickerArriveTime1.Value.Minute, dateTimePickerArriveTime1.Value.Second);
                var tsReturn = new TimeSpan(dateTimePickerReturnTime1.Value.Day, dateTimePickerReturnTime1.Value.Hour, dateTimePickerReturnTime1.Value.Minute, dateTimePickerReturnTime1.Value.Second);

                TimeSpan diff = tsReturn - tsArrive;

                dateTimePickerStartTime1.Value = dateTimePickerArriveTime1.Value.Add(-diff);
                DateTime walkTime = new DateTime(diff.Ticks);
                labelWalk1.Text = string.Format("В пути: {0}:{1}:{2}", walkTime.Hour.ToString("D2"), walkTime.Minute.ToString("D2"), walkTime.Second.ToString("D2"));

                if (dateTimePickerReturnTime2.Value < dateTimePickerArriveTime2.Value)
                {
                    throw new Exception("Время возвращения второго игрока должно быть больше, чем время прибытия");
                }

                tsArrive = new TimeSpan(dateTimePickerArriveTime2.Value.Day, dateTimePickerArriveTime2.Value.Hour, dateTimePickerArriveTime2.Value.Minute, dateTimePickerArriveTime2.Value.Second);
                tsReturn = new TimeSpan(dateTimePickerReturnTime2.Value.Day, dateTimePickerReturnTime2.Value.Hour, dateTimePickerReturnTime2.Value.Minute, dateTimePickerReturnTime2.Value.Second);

                diff = tsReturn - tsArrive;

                dateTimePickerStartTime2.Value = dateTimePickerArriveTime2.Value.Add(-diff);
                walkTime = new DateTime(diff.Ticks);
                labelWalk2.Text = string.Format("В пути: {0}:{1}:{2}", walkTime.Hour.ToString("D2"), walkTime.Minute.ToString("D2"), walkTime.Second.ToString("D2"));

                labelAnswer.Text = dateTimePickerStartTime1.Value == dateTimePickerStartTime2.Value 
                    ? "Время выхода игроков полностью совпадает" 
                    : string.Format("Игрок {0} вышел первым", dateTimePickerStartTime1.Value < dateTimePickerStartTime2.Value ? textBoxNick1.Text : textBoxNick2.Text);
            }
            catch (Exception ex)
            {
                labelAnswer.Text = ex.Message;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            labelArrive1.Text = "Время прибытия\r\nигрока " + textBoxNick1.Text;
            labelArrive2.Text = "Время прибытия\r\nигрока " + textBoxNick2.Text;
            labelReturn1.Text = "Время возвращения\r\nигрока " + textBoxNick1.Text;
            labelReturn2.Text = "Время возвращения\r\nигрока " + textBoxNick2.Text;
            labelStart1.Text = "Время выхода\r\nигрока " + textBoxNick1.Text;
            labelStart2.Text = "Время выхода\r\nигрока " + textBoxNick2.Text;
        }

        private void buttonEvaluate_Click(object sender, EventArgs e)
        {
            var ts = new TimeSpan(dateTimePickerSecond.Value.Hour, dateTimePickerSecond.Value.Minute, dateTimePickerSecond.Value.Second);

            dateTimePickerResult.Value = comboBoxSign.Text == "+" ? dateTimePickerFirst.Value.Add(ts) : dateTimePickerFirst.Value.Add(-ts);
        }

        private void buttonEvaluate2_Click(object sender, EventArgs e)
        {
            var ts = new TimeSpan(dateTimePickerWalkTime.Value.Hour, dateTimePickerWalkTime.Value.Minute, dateTimePickerWalkTime.Value.Second);

            int secInWay = (int)ts.TotalSeconds;
            int percent = (int)numericUpDownPercent.Value;

            int secToChange = secInWay * percent / 100;

            int resSec = comboBoxSign2.Text == "+" ? secInWay + secToChange : secInWay - secToChange;

            var resultTs = new TimeSpan(0, 0, resSec);

            dateTimePickerResultWalkTime.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, resultTs.Hours, resultTs.Minutes, resultTs.Seconds);
        }
    }
}
