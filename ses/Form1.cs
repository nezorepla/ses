using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ses
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
             FileStream stream = new FileStream("..\\"+txtName.Text.ToString()+".wav", FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);
            int RIFF = 0x46464952;
            int WAVE = 0x45564157;
            int formatChunkSize = 16;
            int headerSize = 8;
            int format = 0x20746D66;
            short formatType = 1;
            short tracks = 1;
            int samplesPerSecond = 44100;
            short bitsPerSample = 16;
            short frameSize = (short)(tracks * ((bitsPerSample + 7) / 8));
            int bytesPerSecond = samplesPerSecond * frameSize;
            int waveSize = 4;
            int data = 0x61746164;
            int sn =int.Parse(txtTime.Text.ToString()); //60;
            int samples = 88200 * 2*sn;
            int dataChunkSize = samples * frameSize;
            int fileSize = waveSize + headerSize + formatChunkSize + headerSize + dataChunkSize;
            writer.Write(RIFF);
            writer.Write(fileSize);
            writer.Write(WAVE);
            writer.Write(format);
            writer.Write(formatChunkSize);
            writer.Write(formatType);
            writer.Write(tracks);
            writer.Write(samplesPerSecond);
            writer.Write(bytesPerSecond);
            writer.Write(frameSize);
            writer.Write(bitsPerSample);
            writer.Write(data);
            writer.Write(dataChunkSize);
           double aNatural = 23000.0;
            double ampl = 10000;
            double perfect = 1.5;
            double concert = 1.498307077;
            double freq = double.Parse(txtFreq.Text.ToString());//aNatural * perfect;
            for (int i = 0; i < samples / 4; i++)
            {
                double t = (double)i / (double)samplesPerSecond;
                short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI)));
                writer.Write(s);
            }


           //double  freq = aNatural * concert;
           // for (int i = 0; i < samples / 4; i++)
           // {
           //     double t = (double)i / (double)samplesPerSecond;
           //     short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI)));
           //     writer.Write(s);
           // }
           // for (int i = 0; i < samples / 4; i++)
           // {
           //     double t = (double)i / (double)samplesPerSecond;
           //     short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI) + Math.Sin(t * freq * perfect * 2.0 * Math.PI)));
           //     writer.Write(s);
           // }
           // for (int i = 0; i < samples / 4; i++)
           // {
           //     double t = (double)i / (double)samplesPerSecond;
           //     short s = (short)(ampl * (Math.Sin(t * freq * 2.0 * Math.PI) + Math.Sin(t * freq * concert * 2.0 * Math.PI)));
           //     writer.Write(s);
           // }
            writer.Close();
            stream.Close();
            Application.Exit();
        }

        }
    }
