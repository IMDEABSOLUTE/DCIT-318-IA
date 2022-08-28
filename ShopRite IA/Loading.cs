namespace Shoprite_IA
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
        }
        int startPos = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            startPos += 1;
            progress.Value = startPos;
            PercentageLbl.Text = startPos + "%";
            if(progress.Value == 100)
            {
                progress.Value = 0;
                timer1.Stop();
                Login log = new Login();
                log.Show();
                this.Hide();
            }

        }
        private void Loading_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

    }
}