using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Burak_sLibrary
{
    public class YapayZeka
    {
        /// <summary>
        /// Yapay zeka'nın temelini oluşturan belirli algoritmalar
        /// </summary>

        public class YSA
        {
            
            int inputSize, hiddenSize, outputSize;
            double[,] w1, w2;
            double[] hidden, output;

            Random rnd = new Random();
            public YSA(int inputSize, int hiddenSize, int outputSize)
            {
                /// <summary>
                /// Yapay sinir ağları 1 tane ara katman mevcut
                /// 
                /// Data grid üzerinde harf tahmin etme hazır fonksiyonu
                ///
                /// inputSize=Girdi katmanının büyüklüğü
                /// hiddenSize=Ara katmanının büyüklüğü
                /// outputSize=Çıktı katmanının büyüklüğü
                /// </summary>
                this.inputSize = inputSize;
                this.hiddenSize = hiddenSize;
                this.outputSize = outputSize;

                w1 = new double[inputSize, hiddenSize];
                w2 = new double[hiddenSize, outputSize];
                hidden = new double[hiddenSize];
                output = new double[outputSize];

                BaslangicDegerUret();
            }
            private void BaslangicDegerUret()
            {
                for (int i = 0; i < inputSize; i++)
                    for (int j = 0; j < hiddenSize; j++)
                        w1[i, j] = rnd.NextDouble() - 0.5;

                for (int i = 0; i < hiddenSize; i++)
                    for (int j = 0; j < outputSize; j++)
                        w2[i, j] = rnd.NextDouble() - 0.5;
            }
            private double Sigmoid(double x) => 1.0 / (1.0 + Math.Exp(-x));
            private double SigmoidDeriv(double x) => x * (1 - x);

            public void Egit(double[][] inputs, double[][] targets, int epochs, double learningRate)
            {
                for (int epoch = 0; epoch < epochs; epoch++)
                {
                    for (int p = 0; p < inputs.Length; p++)
                    {
                        double[] input = inputs[p];
                        double[] target = targets[p];

                        // Forward
                        for (int i = 0; i < hiddenSize; i++)
                        {
                            double sum = 0;
                            for (int j = 0; j < inputSize; j++)
                                sum += input[j] * w1[j, i];
                            hidden[i] = Sigmoid(sum);
                        }

                        for (int i = 0; i < outputSize; i++)
                        {
                            double sum = 0;
                            for (int j = 0; j < hiddenSize; j++)
                                sum += hidden[j] * w2[j, i];
                            output[i] = Sigmoid(sum);
                        }

                        // Backpropagation
                        double[] outputErrors = new double[outputSize];
                        for (int i = 0; i < outputSize; i++)
                            outputErrors[i] = (target[i] - output[i]) * SigmoidDeriv(output[i]);

                        double[] hiddenErrors = new double[hiddenSize];
                        for (int i = 0; i < hiddenSize; i++)
                        {
                            double error = 0;
                            for (int j = 0; j < outputSize; j++)
                                error += outputErrors[j] * w2[i, j];
                            hiddenErrors[i] = error * SigmoidDeriv(hidden[i]);
                        }

                        // Weight update
                        for (int i = 0; i < hiddenSize; i++)
                            for (int j = 0; j < outputSize; j++)
                                w2[i, j] += learningRate * outputErrors[j] * hidden[i];

                        for (int i = 0; i < inputSize; i++)
                            for (int j = 0; j < hiddenSize; j++)
                                w1[i, j] += learningRate * hiddenErrors[j] * input[i];
                    }
                }
            }

            public double[] Tahmin(double[] input)
            {
                for (int i = 0; i < hiddenSize; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < inputSize; j++)
                        sum += input[j] * w1[j, i];
                    hidden[i] = Sigmoid(sum);
                }

                for (int i = 0; i < outputSize; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < hiddenSize; j++)
                        sum += hidden[j] * w2[j, i];
                    output[i] = Sigmoid(sum);
                }

                return output;
            }
        }
        public class PSO
        {
            private Random r;
            private int parcaciksayisi { get; set; }
            private int c1 { get; set; }
            private int c2 { get; set; }
            private int usttaban { get; set; }
            private int alttaban { get; set; }
            public List<double> X;
            public List<double> Y;
            public List<double> fitness;
            public List<double> PBestX;
            public List<double> PBestY;
            public List<double> Vx;
            public List<double> Vy;
            public double GBestX { get; set; }
            public double GBestY { get; set; }
            public double Denklem { get; set; }

            public PSO(int parcaciksayisi, int c1, int c2, int usttaban, int alttaban)
            {
                
                this.parcaciksayisi = parcaciksayisi;
                this.c1 = c1;
                this.c2 = c2;
                this.usttaban = usttaban;
                this.alttaban = alttaban;
                X = new List<double>();
                Y = new List<double>();
                fitness = new List<double>();
                PBestX = new List<double>();
                PBestY = new List<double>();
                r = new Random();
                Vx = new List<double>();
                Vy = new List<double>();

                for (int i = 0; i < parcaciksayisi; i++)
                {
                    Vx.Add(0);
                    Vy.Add(0);
                }

                baslangicdegeruret();

            }
            private double uygunluk(double x, double y)
            {
                double dneklem = x*y;//bulmak istediğiniz uygunluk fonksiyonunu buraya yazın
                
                return dneklem;

            }
            private double parcacikhizhesapla(int c1, int c2, double x, double pBest, double gBest, double vBaslangic)
            {

                double rand1 = r.NextDouble();
                double rand2 = r.NextDouble();
                double vsonuc = vBaslangic + (c1 * rand1 * (pBest - x)) + (c2 * rand2 * (gBest - x));
                return vsonuc;

            }
            private void baslangicdegeruret()
            {

                for (int i = 0; i < parcaciksayisi; i++)
                {
                    double rastgeleSayix = alttaban + (r.NextDouble() * (usttaban - alttaban));
                    double rastgeleSayiy = alttaban + (r.NextDouble() * (usttaban - alttaban));
                    X.Add(rastgeleSayix);
                    Y.Add(rastgeleSayiy);
                    fitness.Add(uygunluk(rastgeleSayix, rastgeleSayiy));

                    PBestX.Add(rastgeleSayix);
                    PBestY.Add(rastgeleSayiy);

                }

                double gBestX = PBestX[0];
                double gBestY = PBestY[0];
                double gBestFitness = fitness[0];

                for (int i = 1; i < parcaciksayisi; i++)
                {
                    if (fitness[i] < gBestFitness)
                    {
                        gBestFitness = fitness[i];
                        gBestX = PBestX[i];
                        gBestY = PBestY[i];
                    }
                }
                GBestX = gBestX;
                GBestY = gBestY;

            }
            public void islem(int iterasyon)
            {
                for (int t = 0; t < iterasyon; t++)
                {
                    for (int i = 0; i < parcaciksayisi; i++)
                    {
                        //PBest x ve y değişkenlerini kullanacağız GBestx ve GBesty kullanacağız 
                        Vx[i] = parcacikhizhesapla(c1, c2, X[i], PBestX[i], GBestX, Vx[i]); //xler için
                        Vy[i] = parcacikhizhesapla(c1, c2, Y[i], PBestY[i], GBestY, Vy[i]);//yler için

                        double x1 = X[i] + Vx[i];
                        double y1 = Y[i] + Vy[i];

                        X[i] = x1;
                        Y[i] = y1;
                        double yeniFitness = uygunluk(X[i], Y[i]);
                        if (yeniFitness < uygunluk(PBestX[i], PBestY[i]))
                        {
                            PBestX[i] = X[i];
                            PBestY[i] = Y[i];
                        }

                        fitness[i] = yeniFitness;

                    }

                    int gBestIndex = 0;
                    double gBestFitness = uygunluk(PBestX[0], PBestY[0]);
                    for (int i = 1; i < parcaciksayisi; i++)
                    {
                        double suan = uygunluk(PBestX[i], PBestY[i]);
                        if (suan < gBestFitness)
                        {
                            gBestFitness = suan;
                            gBestIndex = i;
                        }
                    }

                    GBestX = PBestX[gBestIndex];
                    GBestY = PBestY[gBestIndex];
                    //HER İTERASYONDA GBestx ve GBesty değerlrini bulluyoruz 
                }

            }
        }
        public class ABC
        {
            public List<double> X;
            public List<double> Y;
            private List<double> fit;
            public List<double> fonksiyon;
            private List<int> sayac;
            private List<double> pdegerleri;
            private List<double> kumulatif;
            public int kaynakSayisi;
            Random rnd = new Random();
            public int limit;
            public int iterasyon;
            public int ust;
            public int alt;
            public double EnIyiX;
            public double EnIyiY;
            public double enIyideger;
            
            public ABC(int kaynakSayisi, int ust, int alt,int limit,int iterasyon)
            {
                
                this.alt = alt;
                this.ust = ust;
                this.iterasyon = iterasyon;
                this.limit = limit;
                this.kaynakSayisi = kaynakSayisi;
                X = new List<double>();
                Y = new List<double>();
                fit = new List<double>();
                fonksiyon = new List<double>();
                sayac = new List<int>();
                pdegerleri = new List<double>();
                kumulatif = new List<double>();
                for (int i = 0; i < kaynakSayisi; i++)
                {
                    sayac.Add(0);
                }
                for (int i = 0; i < kaynakSayisi; i++)
                {
                    pdegerleri.Add(0);
                }
                for (int i = 0; i < kaynakSayisi; i++)
                {
                    kumulatif.Add(0);
                }

                for (int i = 0; i < kaynakSayisi; i++)
                {
                    double rastgeleSayix = alt + (rnd.NextDouble() * (ust - alt));
                    double rastgeleSayiy = alt + (rnd.NextDouble() * (ust - alt));
                    X.Add(rastgeleSayix);
                    Y.Add(rastgeleSayiy);
                    double fonkdegeri = fonk(rastgeleSayix,rastgeleSayiy);
                    fonksiyon.Add(fonkdegeri);
                    fit.Add(fitness(fonkdegeri));

                }

            }
            private double fitness(double fonkdegeri)
            {
                if (fonkdegeri >= 0)
                {
                    return 1 / (1 + fonkdegeri);
                }
                else
                {
                    return 1 + Math.Abs(fonkdegeri);
                }

            }
            private double fonk(double x,double y)
            {
                // teset fonksiyonu yani x^2+y^2

                double deger = Math.Pow(x,2) + Math.Pow(y,2);// Bulmak istediğiniz fonk buraya yazınız
                return deger;

            }
            private double VdegerHesapla(int i, int j, int k)
            {
                Random rnd = new Random();


                double lambda = 2 * rnd.NextDouble() - 1;

                double sonuc = 0;

                if (j == 0)//x değerlerini alığ işlem yapıyoruz
                {
                    sonuc = X[i] + lambda * (X[i] - X[k]);
                    return sonuc;
                }
                else //y değerlerinde işlem
                {
                    sonuc = Y[i] + lambda * (Y[i] - Y[k]);
                    return sonuc;
                }


            }
            private void olasilikHesapla(int kaynk)
            {
                double toplam = 0;
                for (int i = 0; i < kaynk; i++)
                {
                    toplam += fit[i];
                }

                for (int i = 0; i < kaynk; i++)
                {
                    pdegerleri[i] = fit[i] / toplam;

                }
            }
            private int RuletSecimi(List<double> kümülatif, double r)
            {
                int low = 0;
                int high = kümülatif.Count - 1;

                while (low < high)
                {
                    int mid = (low + high) / 2;

                    if (r <= kümülatif[mid])
                        high = mid;
                    else
                        low = mid + 1;
                }

                return low;
            }
            private void kümülatifhesapla(int n)
            {
                kumulatif[0] = pdegerleri[0];

                for (int i = 1; i < n; i++)
                {
                    kumulatif[i] = kumulatif[i - 1] + pdegerleri[i];
                }

            }

            private void yapılanislemisci()
            {

                Random rnd = new Random();
                for (int i = 0; i < kaynakSayisi; i++)
                {

                    double Vfitsonuc;
                    double Vfonksonuc;


                    int j = rnd.Next(0, 2);
                    int k;
                    do
                    {
                        k = rnd.Next(0, kaynakSayisi);
                    } while (k == i);

                    double vdeger = VdegerHesapla(i, j, k);
                    if (j == 0)
                    {
                        Vfonksonuc = fonk(vdeger, Y[i]);
                        Vfitsonuc = fitness(Vfonksonuc);
                    }
                    else
                    {
                        Vfonksonuc = fonk(X[i], vdeger);
                        Vfitsonuc = fitness(Vfonksonuc);
                    }
                    if (fit[i] >= Vfitsonuc)
                    {
                        sayac[i]++;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            X[i] = vdeger;
                        }
                        else
                        {
                            Y[i] = vdeger;
                        }
                        fit[i] = Vfitsonuc;
                        sayac[i] = 0;
                        fonksiyon[i] = Vfonksonuc;


                    }


                }

            }
            private void yapılanislemgozcu()
            {
                kümülatifhesapla(kaynakSayisi);

                for (int t = 0; t < kaynakSayisi; t++)
                {
                    double Vfitsonuc;
                    double Vfonksonuc;
                    double rastgele = rnd.NextDouble();
                    int i = RuletSecimi(kumulatif, rastgele);
                    int j = rnd.Next(0, 2);
                    int k;
                    do
                    {
                        k = rnd.Next(0, kaynakSayisi);
                    } while (k == i);
                    double vdeger = VdegerHesapla(i, j, k);
                    if (j == 0)
                    {
                        Vfonksonuc = fonk(vdeger, Y[i]);
                        Vfitsonuc = fitness(Vfonksonuc);
                    }
                    else
                    {
                        Vfonksonuc = fonk(X[i], vdeger);
                        Vfitsonuc = fitness(Vfonksonuc);
                    }
                    if (fit[i] >= Vfitsonuc)
                    {
                        sayac[i]++;
                    }
                    else
                    {
                        if (j == 0)
                        {
                            X[i] = vdeger;
                        }
                        else
                        {
                            Y[i] = vdeger;
                        }
                        fit[i] = Vfitsonuc;
                        sayac[i] = 0;
                        fonksiyon[i] = Vfonksonuc;


                    }

                }

            }
            private double RastgeleDeger(double alt, double ust)
            {
                return alt + rnd.NextDouble() * (ust - alt);
            }
            public void ABCHesapla()
            {
                for (int iter = 0; iter < iterasyon; iter++)
                {
                    yapılanislemisci();
                    olasilikHesapla(kaynakSayisi);
                    yapılanislemgozcu();
                    for (int i = 0; i < kaynakSayisi; i++)
                    {
                        if (sayac[i] >= limit)
                        {
                            X[i] = RastgeleDeger(alt, ust);
                            Y[i] = RastgeleDeger(alt, ust);

                            double fonkSonuc = fonk(X[i], Y[i]);
                            fonksiyon[i] = fonkSonuc;
                            fit[i] = fitness(fonkSonuc);
                            sayac[i] = 0;
                        }
                    }
                    double Deger = fonksiyon[0];

                    for (int i = 1; i < kaynakSayisi; i++)
                    {
                        if (fonksiyon[i] < Deger)
                        {
                            Deger = fonksiyon[i];

                        }
                    }
                    double enIyiDeger = fonksiyon[0];
                    int enIyiIndex = 0;

                    for (int i = 1; i < kaynakSayisi; i++)
                    {
                        if (fonksiyon[i] < enIyiDeger)
                        {
                            enIyiDeger = fonksiyon[i];
                            enIyiIndex = i;
                        }
                    }

                    EnIyiX = X[enIyiIndex];
                    EnIyiY = Y[enIyiIndex];
                    enIyideger = enIyiDeger;

                }
            }

        }
        public class GenetikAlgo
        {

            Random rnd = new Random();
            private static List<string> X;
            private static List<string> Y;
            private static List<double> Yreel;
            private static List<double> Xreel;
            private static List<double> fonksonuc;
            private static List<string> XY;
            public static double eniyiX;
            public static double eniyiY;
            public static double enIyiFitness;
            public int jenerasyonsayisi { get; set; }
            public double caprazlamaorani { get; set; }
            public int popullasyonsayisi { get; set; }
            public double mutasyonorani { get; set; }
            public GenetikAlgo(int populasyonsayisi,double caprazlamaorani,double mutasyonorani,int jenerasyonsayisi)
            {
                this.jenerasyonsayisi = jenerasyonsayisi;
                popullasyonsayisi = populasyonsayisi;
                this.caprazlamaorani = caprazlamaorani;
                this.mutasyonorani = mutasyonorani;
                X = new List<string>();
                Y = new List<string>();
                Xreel = new List<double>();
                Yreel = new List<double>();
                fonksonuc = new List<double>();
                XY = new List<string>();
            }
            private void FitnessFonk()
            {
                double cevap = 0;
                Xreel.AddRange(ReelDegerConvert(BinaryToDecimal(X)));
                Yreel.AddRange(ReelDegerConvert(BinaryToDecimal(Y)));

                for (int i = 0; i < X.Count; i++)
                {

                    cevap = (1 + (Math.Pow(Xreel[i] + Yreel[i] + 1, 2)) * (19 - 14 * Xreel[i] + 3 * Math.Pow(Xreel[i], 2) - 14 * Yreel[i] + 6 * Xreel[i] * Yreel[i] + 3 * Math.Pow(Yreel[i], 2))) * (30 + (Math.Pow(2 * Xreel[i] - 3 * Yreel[i], 2)) * (18 - 32 * Xreel[i] + 12 * Math.Pow(Xreel[i], 2) + 48 * Yreel[i] - 36 * Xreel[i] * Yreel[i] + 27 * Math.Pow(Yreel[i], 2)));//fonk buradan değiştirebilirsiniz

                    double yuvarlanmiscevap = Math.Round(cevap, 3);
                    fonksonuc.Add(yuvarlanmiscevap);

                }
            }
            private List<string> xybirlestir()
            {
                List<string> list = new List<string>();
                for (int i = 0; i < X.Count; i++)
                {
                    list.Add(X[i] + Y[i]);

                }
                return list;
            }
            private void binarysayiyilistadd()
            {
                for (int i = 0; i < popullasyonsayisi; i++)
                {
                    X.Add(binarysayıuret());
                }
                for (int i = 0; i < popullasyonsayisi; i++)
                {
                    Y.Add(binarysayıuret());
                }
                XY.AddRange(xybirlestir());
            }
            private string binarysayıuret()
            {
                string binarysayi = "";

                for (int i = 0; i < 12; i++)
                {
                    binarysayi += Convert.ToString(rnd.Next(0, 2));
                }

                return binarysayi;
            }
            private List<int> BinaryToDecimal(List<string> list)
            {
                List<int> result = new List<int>();
                foreach (var binary in list)
                {
                    result.Add(Convert.ToInt32(binary, 2));
                }
                return result;
            }
            private List<double> ReelDegerConvert(List<int> deger)
            {
                List<double> result = new List<double>();
                double üs = Math.Pow(2, 12);
                foreach (var d in deger)
                {
                    double sonuc = Math.Round(-2 + (d / (üs - 1.0)) * 4, 3);
                    result.Add(sonuc);
                }

                return result;
            }
            private List<string> secim(double oran)
            {
                List<string> result = new List<string>();
                List<double> res = new List<double>();
                for (int i = 0; i < X.Count; i++)
                {
                    res.Add(rnd.NextDouble());
                }
                for (int i = 0; i < X.Count; i++)
                {
                    if (res[i] < oran)
                    {
                        result.Add(XY[i]);


                    }

                }
                if (result.Count % 2 == 0)
                {

                }
                else
                {
                    result.RemoveAt(0);

                }
                foreach (var item in result)
                {
                    XY.Remove(item);
                }

                return result;

            }
            private void caprazlama(double oran)
            {
                int randoms = rnd.Next(1, 23);
                var secims = secim(oran);
                for (int i = 0; i < secims.Count; i += 2)
                {
                    string ilkili = secims[i].Substring(0, randoms);
                    string ilkinsonu = secims[i].Substring(randoms);
                    string ikincininilki = secims[i + 1].Substring(0, randoms);
                    string ikincininsonu = secims[i + 1].Substring(randoms);
                    string v1 = ilkili + ikincininsonu;
                    string v2 = ikincininilki + ilkinsonu;
                    XY.Add(v1);
                    XY.Add(v2);

                }

            }
            private string Mutasyonstringi()
            {
                string deger = "";
                foreach (var item in XY)
                {
                    deger += item;
                }
                return deger;
            }
            private void Mutasyonİslemi(double d)
            {
                List<double> list = new List<double>();
                int bitsayisi = X.Count * 24;
                string deger = Mutasyonstringi();
                for (int i = 0; i < bitsayisi; i++)
                {
                    list.Add(rnd.NextDouble());
                }
                char[] degerArray = deger.ToCharArray();
                for (int i = 0; i < bitsayisi; i++)
                {
                    if (list[i] < d)
                    {
                        degerArray[i] = degerArray[i] == '0' ? '1' : '0';
                    }

                }
                deger = new string(degerArray);
                XY.Clear();
                for (int i = 0; i < deger.Length; i += 24)
                {

                    string yeniBirey = deger.Substring(i, 24);

                    XY.Add(yeniBirey);
                }

            }
            private void duzenleme()
            {
                X.Clear();
                Y.Clear();

                for (int i = 0; i < XY.Count; i++)
                {
                    X.Add(XY[i].Substring(0, 12));
                    Y.Add(XY[i].Substring(12));
                }
                EnIyiBireyiSec();
                Yreel.Clear();
                Xreel.Clear();
            }
            private void EnIyiBireyiSec()
            {
                enIyiFitness = fonksonuc.Min();
                int enIyiIndex = fonksonuc.IndexOf(enIyiFitness);
                eniyiX = Xreel[enIyiIndex];
                eniyiY = Yreel[enIyiIndex];
            }
            public double GenetikHesapla()
            {
                binarysayiyilistadd();
                for (int i = 1; i <= jenerasyonsayisi; i++)
                {
                    FitnessFonk();
                    caprazlama(caprazlamaorani);
                    Mutasyonİslemi(mutasyonorani);
                    duzenleme();
                    fonksonuc.Clear();
                }
                double x = eniyiX;
                double y = eniyiY;
                double cevap = (1 + (Math.Pow(x + y + 1, 2)) * (19 - 14 * x + 3 * Math.Pow(x, 2) - 14 * y + 6 * x * y + 3 * Math.Pow(y, 2))) * (30 + (Math.Pow(2 * x - 3 * y, 2)) * (18 - 32 * x + 12 * Math.Pow(x, 2) + 48 * y - 36 * x * y + 27 * Math.Pow(y, 2)));
             
                return cevap;
            }
        }
    }
}
