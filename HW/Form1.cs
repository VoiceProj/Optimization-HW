using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW
{
    public partial class Form1 : Form
    {
        double IV = 0, I1 = 0, I2 = 0, I3 = 0, I4 = 0, I5 = 0, I6 = 0, EU = 0, U1 = 0, U2 = 0, U3 = 0;
        double F = 0, U11 = 0, U22 = 0, U33 = 0, Fz = 0, F1=0;
        int z = 0; //flag


        private void recountBtn_Click(object sender, EventArgs e)
        {
            I1 -= 0.09;
            I2 -= 0.09;
            I3 -= 0.09;
            I4 -= 0.09;
            I5 -= 0.09;
            I6 -= 0.09;

            I1tb.Text = I1.ToString();
            I2tb.Text = I2.ToString();
            I3tb.Text = I3.ToString();
            I4tb.Text = I4.ToString();
            I5tb.Text = I5.ToString();
            I6tb.Text = I6.ToString();

            U2 = I2 * EU * IV / ((IV + I2 + I1) * (I2 + I3 + I4 + I5 - Math.Pow(I2, 2) / (IV + I2 + I1) - Math.Pow((I4 + I5), 2) / (I4 + I5 + I6)));
            U1 = U2 * I2 / (IV + I2 + I1) + EU * IV / (IV + I2 + I1);
            U3 = U2 * (I4 + I5) / (I4 + I5 + I6);
            U1 = Math.Round(U1, 3);
            U2 = Math.Round(U2, 3);
            U3 = Math.Round(U3, 3);

            U1label.Text = "U1 = " + U1.ToString();
            U2label.Text = "U2 = " + U2.ToString();
            U3label.Text = "U3 = " + U3.ToString();

            if (z == 1) F1 = Math.Round(Math.Pow(U11 - U1, 2), 3);
            if (z == 2) F1 = Math.Round(Math.Pow(U22 - U2, 2), 3);
            if (z == 3) F1 = Math.Round(Math.Pow(U33 - U3, 2), 3);

            if (F1 < F)
            {
                FTB.Text = F1.ToString();
                F = F1;
                logsRTB.AppendText("Целевая функция уменьшается F="+F1.ToString()+"\n");
                logsRTB.ScrollToCaret();
            }
            else {
                logsRTB.AppendText("Целевая функция увеличилась F=" + F1.ToString() + "\n");
                logsRTB.ScrollToCaret();
                recountBtn.Enabled = false;
            }
            Fz = 0;

            calculateGrad();
            calculateHessian();
        }

        private void calculateHessian()
        {
            if (Fz == 0)
            {
                U2 = Math.Round(U2, 3);
                U1 = Math.Round(U1, 3);
                U3 = Math.Round(U3, 3);
                string u = Convert.ToString(2 * U2 - 2 * U3);
                label48.Text = u;
                label46.Text = u;
                label42.Text = u;
                label40.Text = u;
                string uuu = Convert.ToString(U2 - 2 * U3);
                label44.Text = uuu;
                label38.Text = uuu;
                label36.Text = uuu;
                label34.Text = uuu;
                string uu = Convert.ToString(-2 * U3);
                label32.Text = uu;

            }
            else
            {
                U22 = Math.Round(U22, 3);
                U11 = Math.Round(U11, 3);
                U33 = Math.Round(U33, 3);
                string u = Convert.ToString(2 * U22 - 2 * U33);
                label48.Text = u;
                label46.Text = u;
                label42.Text = u;
                label40.Text = u;
                string uuu = Convert.ToString(U22 - 2 * U33);
                label44.Text = uuu;
                label38.Text = uuu;
                label36.Text = uuu;
                label34.Text = uuu;
                string uu = Convert.ToString(-2 * U33);
                label32.Text = uu;
            }
        }

        private void calculateGrad()
        {
            string nu = "0.0";
            gr1l.Text = nu;
            gr2l.Text = nu;
            if (Fz == 0)
            {
                string u = Convert.ToString(Math.Round((U3 - U2),3));
                gr3l.Text = u;
                string uu = Convert.ToString(Math.Round((U3 - U2),3));
                gr4l.Text = uu;
                string uuu = Convert.ToString(Math.Round(U3,3));
                gr5l.Text = uuu;
            }
            else
            {
                string u = Convert.ToString(Math.Round((U33 - U22),3));
                gr3l.Text = u;
                string uu = Convert.ToString(Math.Round((U33 - U22),3));
                gr4l.Text = uu;
                string uuu = Convert.ToString(Math.Round(U33,3));
                gr5l.Text = uuu;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            logsRTB.Clear();
            U1TB.Clear();
            U2TB.Clear();
            U3TB.Clear();
            F = 0;
            Fz = 0;
            U11 = 0;
            U22 = 0;
            U33 = 0;
            recountBtn.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            F = 0; U11 = 0; U22 = 0; U33 = 0;
            string u1, u2, u3;
            z = 0;
            u1 = U1TB.Text;
            u2 = U2TB.Text;
            u3 = U3TB.Text;
            if (u1.Length !=0  && F == 0)
            {
                z = 1;
                U11 = Int32.Parse(u1);
                U22 = (U11 - EU * IV / (IV + I2 + I1)) * (IV + I2 + I1) / I2;
                U33 = U22 * (I4 + I5) / (I4 + I5 + I6);
                U22 = Math.Round(U22, 3);
                U33 = Math.Round(U33, 3);
                U2TB.Text = U22.ToString();
                U3TB.Text = U33.ToString();
                F = Math.Round(Math.Pow(U11 - U1, 2), 3);
                FTB.Text = F.ToString();
            };

            if (u2.Length !=0 && F == 0)
            {
                z = 2;
                U22 = Int32.Parse(u2);
                U11 = U22 * I2 / (IV + I2 + I1) + EU * IV / (IV + I2 + I1);
                U33 = U22 * (I4 + I5) / (I4 + I5 + I6);
                U1TB.Text = U11.ToString();
                U3TB.Text = U33.ToString();
                U11 = Math.Round(U11, 3);
                U33 = Math.Round(U33, 3);
                F = Math.Round(Math.Pow(U22 - U2, 2), 3);
                FTB.Text = F.ToString();

            };

            if (u3.Length !=0 && F == 0)
            {
                z = 3;
                U33 = Int32.Parse(u3);
                U22 = U33 * (I4 + I5 + I6) / (I4 + I5);
                U11 = U22 * I2 / (IV + I2 + I1) + EU * IV / (IV + I2 + I1);
                U22 = Math.Round(U22, 3);
                U11 = Math.Round(U11, 3);
                U1TB.Text = U11.ToString();
                U2TB.Text = U22.ToString();
                F = Math.Round(Math.Pow(U33 - U3, 2), 3);
                FTB.Text = F.ToString();
            };

            Fz = F;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IV = Double.Parse(Ivtb.Text);
            I1 = Double.Parse(I1tb.Text);
            I2 = Double.Parse(I2tb.Text);
            I3 = Double.Parse(I3tb.Text);
            I4 = Double.Parse(I4tb.Text);
            I5 = Double.Parse(I5tb.Text);
            I6 = Double.Parse(I6tb.Text);
            EU = Double.Parse(Etb.Text);

            U2 = I2 * EU * IV / ((IV + I2 + I1) * (I2 + I3 + I4 + I5 - Math.Pow(I2, 2) / (IV + I2 + I1) - Math.Pow((I4 + I5), 2) / (I4 + I5 + I6)));
            U1 = U2 * I2 / (IV + I2 + I1) + EU * IV / (IV + I2 + I1);
            U3 = U2 * (I4 + I5) / (I4 + I5 + I6);

            U2 = Math.Round(U2, 3);
            U1 = Math.Round(U1, 3);
            U3 = Math.Round(U3, 3);
                        
            U1label.Text = "U1 = " + U1.ToString();
            U2label.Text = "U2 = " + U2.ToString();
            U3label.Text = "U3 = " + U3.ToString();
        }
    }
}
