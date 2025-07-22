using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Burak_sLibrary
{
    public class Kriptoloji
    {
        public class AnahtarliYerdegistirme 
        {
            private const string Alfabe = "abcçdefgğhıijklmnoöprsştuüvyz";
            public string Sifrele(string metin, string anahtar = null)
            {
                int kolonsayisi = int.Parse(anahtar);
                metin = metin.Replace(" ", "").ToLower();
                string newmetin = "";
                foreach (char harf in metin)
                {
                    int idex = Alfabe.IndexOf(harf);
                    if (idex >= 0)
                    {
                        newmetin += harf;

                    }
                    else
                    {
                        newmetin += "";
                    }
                }

                int satirSayisi = (int)Math.Ceiling((double)newmetin.Length / kolonsayisi);

                char[,] tablo = new char[satirSayisi, kolonsayisi];
                int index = 0;

                for (int i = 0; i < satirSayisi; i++)
                    for (int j = 0; j < kolonsayisi; j++)
                        tablo[i, j] = index < newmetin.Length ? newmetin[index++] : 'x';

                StringBuilder sonuc = new StringBuilder();

                for (int j = 0; j < kolonsayisi; j++)
                    for (int i = 0; i < satirSayisi; i++)
                        sonuc.Append(tablo[i, j]);

                return sonuc.ToString();
            }
            public string Coz(string sifreliMetin, string anahtar = null)
            {
                int KolonSayisi = int.Parse(anahtar);

                sifreliMetin = sifreliMetin.ToLower().Replace(" ", "");
                int satirSayisi = (int)Math.Ceiling((double)sifreliMetin.Length / KolonSayisi);

                char[,] tablo = new char[satirSayisi, KolonSayisi];
                int index = 0;


                for (int j = 0; j < KolonSayisi; j++)
                {
                    for (int i = 0; i < satirSayisi; i++)
                    {
                        if (index < sifreliMetin.Length)
                            tablo[i, j] = sifreliMetin[index++];
                        else
                            tablo[i, j] = 'x';
                    }
                }


                StringBuilder sonuc = new StringBuilder();
                for (int i = 0; i < satirSayisi; i++)
                {
                    for (int j = 0; j < KolonSayisi; j++)
                    {
                        sonuc.Append(tablo[i, j]);
                    }
                }

                return sonuc.ToString().TrimEnd('x');
            }
        }
        public class Dogrusal
        {
            private const string Alfabe = "abcçdefgğhıijklmnoöprsştuüvyz";

            public string Sifrele(string metin, string anahtar)
            {
                // Anahtar iki sayıdan oluşmalı: a,b şeklinde (örneğin: "5,8")
                string[] anahtarParcalari = anahtar.Split(',');
                if (anahtarParcalari.Length != 2) return "Anahtar hatalı. Örn: 5,8";

                int a = int.Parse(anahtarParcalari[0]);
                int b = int.Parse(anahtarParcalari[1]);
                int m = Alfabe.Length;

                if (GCD(a, m) != 1)
                    return "'a' değeri ile alfabe uzunluğu aralarında asal olmalı.";

                metin = metin.ToLower().Replace(" ", "");
                string sonuc = "";

                foreach (char harf in metin)
                {
                    int index = Alfabe.IndexOf(harf);
                    if (index >= 0)
                    {
                        int yeniIndex = (a * index + b) % m;
                        sonuc += Alfabe[yeniIndex];
                    }
                    else
                    {
                        sonuc += "";
                    }
                }

                return sonuc;
            }
           
            private int GCD(int a, int b)
            {
                while (b != 0)
                {
                    int t = b;
                    b = a % b;
                    a = t;
                }
                return a;
            }
            public string Coz(string sifreliMetin, string anahtar)
            {
                string[] parcalar = anahtar.Split(',');
                if (parcalar.Length != 2)
                    return "Anahtar 'a,b' formatında olmalıdır. Örn: 5,8";

                int a = int.Parse(parcalar[0]);
                int b = int.Parse(parcalar[1]);
                int m = Alfabe.Length;

                int aTersi = ModulerTers(a, m);
                if (aTersi == -1)
                    return "'a' değeri ile alfabe boyutu aralarında asal değil.";

                sifreliMetin = sifreliMetin.ToLower().Replace(" ", "");
                string sonuc = "";

                foreach (char harf in sifreliMetin)
                {
                    int index = Alfabe.IndexOf(harf);

                    int cozulmusIndex = (aTersi * (index - b + m)) % m;
                    sonuc += Alfabe[cozulmusIndex];


                }

                return sonuc;
            }
            //hangi sayı ile a değerini çarparsak mod 29 alırsak kalan 1 dir'i buluyor
            private int ModulerTers(int a, int m)
            {
                for (int i = 1; i < m; i++)
                    if ((a * i) % m == 1)
                        return i;
                return -1;
            }


        }
        public class DortKare
        {
            private const string Alfabe = "abcçdefgğhıijklmnoöprsştuüvyzx";
            private const string Kare2 = "ğftjpmraüşöıoliuzcdngbhyuçskxe";
            private const string Kare3 = "asdfgertyuıopğüişlkjhzxcvbnmöç";

            public string Sifrele(string metin, string anahtar = null)
            {
                char[,] kare1 = new char[6, 5];
                char[,] kare4 = new char[6, 5];
                char[,] kare2 = new char[6, 5];
                char[,] kare3 = new char[6, 5];


                int idx = 0;

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        kare1[i, j] = Alfabe[idx];
                        kare4[i, j] = Alfabe[idx];
                        kare2[i, j] = Kare2[idx];
                        kare3[i, j] = Kare3[idx];

                        idx++;
                    }
                }


                metin = metin.ToLower().Replace(" ", "");
                string newmetin = "";
                foreach (char harf in metin)
                {
                    int idex = Alfabe.IndexOf(harf);
                    if (idex >= 0)
                    {
                        newmetin += harf;

                    }
                    else
                    {
                        newmetin += "";
                    }
                }


                if (newmetin.Length % 2 != 0) newmetin += "x";

                StringBuilder sonuc = new StringBuilder();

                for (int i = 0; i < newmetin.Length; i += 2)
                {
                    char c1 = newmetin[i];
                    char c2 = newmetin[i + 1];

                    var indeks1 = DegerIndexiniBul(kare1, c1);
                    var indeks2 = DegerIndexiniBul(kare4, c2);

                    int satir1 = indeks1.Item1;
                    int sutun1 = indeks1.Item2;

                    int satir2 = indeks2.Item1;
                    int sutun2 = indeks2.Item2;

                    //kare1 in satırı kare 2nin satırına eşittir
                    //kare1in sütunu kare3 ün sütununa eşittir

                    //kare4ün satırı kare3üm satırına
                    //kare4ün sütunu kare 2nin sütununa eşittir

                    sonuc.Append(kare2[satir1, sutun2]);
                    sonuc.Append(kare3[satir2, sutun1]);
                }

                return sonuc.ToString();
            }
            private (int, int) DegerIndexiniBul(char[,] matris, char hedef)
            {
                for (int i = 0; i < matris.GetLength(0); i++)
                {
                    for (int j = 0; j < matris.GetLength(1); j++)
                    {
                        if (matris[i, j] == hedef)
                        {
                            return (i, j);
                        }
                    }
                }
                return (-1, -1);
            }
            public string Coz(string sifreliMetin, string anahtar = null)
            {
                char[,] kare1 = new char[6, 5];
                char[,] kare4 = new char[6, 5];
                char[,] kare2 = new char[6, 5];
                char[,] kare3 = new char[6, 5];

                int idx = 0;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        kare1[i, j] = Alfabe[idx];
                        kare4[i, j] = Alfabe[idx];
                        kare2[i, j] = Kare2[idx];
                        kare3[i, j] = Kare3[idx];
                        idx++;
                    }
                }



                StringBuilder sonuc = new StringBuilder();

                for (int i = 0; i < sifreliMetin.Length; i += 2)
                {
                    char sc1 = sifreliMetin[i];
                    char sc2 = sifreliMetin[i + 1];

                    var indeks1 = DegerIndexiniBul(kare2, sc1);
                    var indeks2 = DegerIndexiniBul(kare3, sc2);

                    int satir1 = indeks1.Item1;
                    int sutun2 = indeks1.Item2;

                    int satir2 = indeks2.Item1;
                    int sutun1 = indeks2.Item2;

                    char orijinal1 = kare1[satir1, sutun1];
                    char orijinal2 = kare4[satir2, sutun2];

                    sonuc.Append(orijinal1);
                    sonuc.Append(orijinal2);
                }

                return sonuc.ToString();
            }

        }
        public class Hill 
        {
            private const string Alfabe = "abcçdefgğhıijklmnoöprsştuüvyz";
            

            public string Sifrele(string metin, string anahtar = null)
            {
                metin = metin.ToLower().Replace(" ", "");
                string newmetin = "";

                foreach (char harf in metin)
                {
                    int idex = Alfabe.IndexOf(harf);
                    if (idex >= 0)
                    {
                        newmetin += harf;

                    }
                    else
                    {
                        newmetin += "";
                    }
                }


                while (newmetin.Length % 3 != 0) newmetin += 'a';

                int[,] matris = new int[3, 3];
                for (int i = 0; i < 9; i++)
                    matris[i / 3, i % 3] = Alfabe.IndexOf(anahtar[i]);

                StringBuilder sonuc = new StringBuilder();

                for (int i = 0; i < newmetin.Length; i += 3)
                {
                    int[] vektor = new int[3];
                    for (int j = 0; j < 3; j++)
                        vektor[j] = Alfabe.IndexOf(newmetin[i + j]);

                    for (int r = 0; r < 3; r++)
                    {
                        int toplam = 0;
                        for (int c = 0; c < 3; c++)
                            toplam += matris[r, c] * vektor[c];
                        sonuc.Append(Alfabe[((toplam % 29) + 29) % 29]);
                    }
                }

                return sonuc.ToString();
            }
            public string Coz(string sifreli, string anahtar = null)
            {
                sifreli = sifreli.ToLower().Replace(" ", "");
                

                int[,] matris = new int[3, 3];
                for (int i = 0; i < 9; i++)
                    matris[i / 3, i % 3] = Alfabe.IndexOf(anahtar[i]);

                int[,] ters = MatrisTersi(matris, 29);
                if (ters == null) return "Ters matris bulunamıyor.";

                StringBuilder sonuc = new StringBuilder();

                for (int i = 0; i < sifreli.Length; i += 3)
                {
                    int[] vektor = new int[3];
                    for (int j = 0; j < 3; j++)
                        vektor[j] = Alfabe.IndexOf(sifreli[i + j]);

                    for (int r = 0; r < 3; r++)
                    {
                        int toplam = 0;
                        for (int c = 0; c < 3; c++)
                            toplam += ters[r, c] * vektor[c];
                        sonuc.Append(Alfabe[((toplam % 29) + 29) % 29]);
                    }
                }

                return sonuc.ToString().TrimEnd('a');

            }

            private int[,] MatrisTersi(int[,] m, int mod)
            {
                int det = ((m[0, 0] * m[1, 1] * m[2, 2] + m[1, 0] * m[2, 1] * m[0, 2] + m[2, 0] * m[0, 1] * m[1, 2]) - (m[0, 2] * m[1, 1] * m[2, 0] + m[1, 2] * m[2, 1] * m[0, 0] + m[2, 2] * m[0, 1] * m[1, 0])) % mod;


                if (det < 0) det += mod;
                int detTersi = TersMod(det, mod);
                if (detTersi == -1) return null;

                int[,] adj = new int[3, 3];
                adj[0, 0] = (m[1, 1] * m[2, 2] - m[1, 2] * m[2, 1]);
                adj[0, 1] = -(m[1, 0] * m[2, 2] - m[1, 2] * m[2, 0]);
                adj[0, 2] = (m[1, 0] * m[2, 1] - m[1, 1] * m[2, 0]);
                adj[1, 0] = -(m[0, 1] * m[2, 2] - m[0, 2] * m[2, 1]);
                adj[1, 1] = (m[0, 0] * m[2, 2] - m[0, 2] * m[2, 0]);
                adj[1, 2] = -(m[0, 0] * m[2, 1] - m[0, 1] * m[2, 0]);
                adj[2, 0] = (m[0, 1] * m[1, 2] - m[0, 2] * m[1, 1]);
                adj[2, 1] = -(m[0, 0] * m[1, 2] - m[0, 2] * m[1, 0]);
                adj[2, 2] = (m[0, 0] * m[1, 1] - m[0, 1] * m[1, 0]);

                int[,] ters = new int[3, 3];
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        int val = adj[j, i] * detTersi;
                        val %= mod;
                        if (val < 0) val += mod;
                        ters[i, j] = val;
                    }

                return ters;
            }

            private int TersMod(int a, int m)
            {
                for (int x = 1; x < m; x++)
                    if ((a * x) % m == 1)
                        return x;
                return -1;
            }
        }
        public class Kaydirmali 
        {
            private const string Alfabe = "abcçdefgğhıijklmnoöprsştuüvyz";

            public string Sifrele(string metin, string anahtar)
            {
                if (!int.TryParse(anahtar, out int kaydirma))
                    return "Kaydırma değeri geçerli bir sayı olmalıdır.";


                metin = metin.Replace(" ", "").ToLower();
                string sonuc = "";

                foreach (char harf in metin)
                {
                    int index = Alfabe.IndexOf(harf);
                    if (index >= 0)
                        sonuc += Alfabe[(index + kaydirma) % Alfabe.Length];
                    else
                        sonuc += "";
                }
                return sonuc;
            }
            public string Coz(string sifreliMetin, string anahtar)
            {
                if (!int.TryParse(anahtar, out int kaydirma))
                    return "Geçerli bir sayı anahtar girilmelidir.";

                sifreliMetin = sifreliMetin.Replace(" ", "").ToLower();
                string sonuc = "";

                foreach (char harf in sifreliMetin)
                {
                    int index = Alfabe.IndexOf(harf);


                    int yeniIndex = (index - kaydirma + Alfabe.Length) % Alfabe.Length;
                    sonuc += Alfabe[yeniIndex];

                }
                return sonuc;
            }
        }
        public class Rota 
        {
            private const string Alfabe = "abcçdefgğhıijklmnoöprsştuüvyz";
            public string Sifrele(string metin, string anahtar = null)
            {
                int kolonsayisi = int.Parse(anahtar);
                metin = metin.Replace(" ", "").ToLower();
                string newmetin = "";

                foreach (char harf in metin)
                {
                    int idex = Alfabe.IndexOf(harf);
                    if (idex >= 0)
                    {
                        newmetin += harf;

                    }
                    else
                    {
                        newmetin += "";
                    }
                }


                int satirSayisi = (int)Math.Ceiling((double)newmetin.Length / kolonsayisi);

                char[,] matris = new char[satirSayisi, kolonsayisi];
                int index = 0;

                for (int i = 0; i < satirSayisi; i++)
                    for (int j = 0; j < kolonsayisi; j++)
                        matris[i, j] = index < newmetin.Length ? newmetin[index++] : 'x';

                // Spiral saat yönü sol alt köşeden başla
                StringBuilder sonuc = new StringBuilder();
                int katman = 0;
                int sayac = newmetin.Length;
                while (katman < (Math.Min(satirSayisi, kolonsayisi) + 1) / 2)
                {
                    int basSatir = satirSayisi - 1 - katman;
                    int bitSatir = katman;
                    int basSutun = katman;
                    int bitSutun = kolonsayisi - 1 - katman;


                    for (int i = basSatir; i >= bitSatir; i--)
                    {
                        if (sayac == -1)
                        {
                            break;
                        }
                        sonuc.Append(matris[i, basSutun]);
                        sayac--;
                    }



                    for (int j = basSutun + 1; j <= bitSutun; j++)
                    {
                        if (sayac == -1)
                        {
                            break;
                        }
                        sonuc.Append(matris[bitSatir, j]);
                        sayac--;
                    }



                    for (int i = bitSatir + 1; i <= basSatir; i++)
                    {
                        if (sayac == -1)
                        {
                            break;
                        }
                        sonuc.Append(matris[i, bitSutun]);
                        sayac--;
                    }

                    for (int j = bitSutun - 1; j > basSutun; j--)
                    {
                        if (sayac == -1)
                        {
                            break;
                        }
                        sonuc.Append(matris[basSatir, j]);
                        sayac--;
                    }


                    katman++;
                }


                return sonuc.ToString();
            }
            public string Coz(string sifreliMetin, string anahtar = null)
            {
                int kolonsayisi = int.Parse(anahtar);
                int toplamKarakter = sifreliMetin.Length;
                int satirSayisi = (int)Math.Ceiling((double)toplamKarakter / kolonsayisi);

                char[,] matris = new char[satirSayisi, kolonsayisi];
                int index = 0;
                int katman = 0;

                while (katman < (Math.Min(satirSayisi, kolonsayisi) + 1) / 2 && index < sifreliMetin.Length)
                {
                    int basSatir = satirSayisi - 1 - katman;
                    int bitSatir = katman;
                    int basSutun = katman;
                    int bitSutun = kolonsayisi - 1 - katman;

                    for (int i = basSatir; i >= bitSatir && index < sifreliMetin.Length; i--)
                        matris[i, basSutun] = sifreliMetin[index++];


                    for (int j = basSutun + 1; j <= bitSutun && index < sifreliMetin.Length; j++)
                        matris[bitSatir, j] = sifreliMetin[index++];


                    for (int i = bitSatir + 1; i <= basSatir && index < sifreliMetin.Length; i++)
                        matris[i, bitSutun] = sifreliMetin[index++];


                    for (int j = bitSutun - 1; j > basSutun && index < sifreliMetin.Length; j--)
                        matris[basSatir, j] = sifreliMetin[index++];

                    katman++;
                }


                StringBuilder cozulmus = new StringBuilder();
                for (int i = 0; i < satirSayisi; i++)
                {
                    for (int j = 0; j < kolonsayisi; j++)
                    {
                        cozulmus.Append(matris[i, j]);
                    }
                }

                return cozulmus.ToString().TrimEnd('x');
            }
        }
        public class Vigenere
        {
            private const string Alfabe = "abcçdefgğhıijklmnoöprsştuüvyz";

            public string Sifrele(string metin, string anahtar)
            {


                metin = metin.ToLower().Replace(" ", "");
                anahtar = anahtar.ToLower().Replace(" ", "");
                string sonuc = "";
                string newmetin = "";

                foreach (char harf in metin)
                {
                    int idex = Alfabe.IndexOf(harf);
                    if (idex >= 0)
                    {
                        newmetin += harf;

                    }
                    else
                    {
                        newmetin += "";
                    }
                }


                for (int i = 0; i < newmetin.Length; i++)
                {
                    char harf = newmetin[i];
                    int index = Alfabe.IndexOf(harf);



                    char anahtarHarf = anahtar[i % anahtar.Length];
                    int anahtarIndex = Alfabe.IndexOf(anahtarHarf);
                    int yeniIndex = (index + anahtarIndex) % Alfabe.Length;
                    sonuc += Alfabe[yeniIndex];


                }
                return sonuc;
            }
            public string Coz(string sifreliMetin, string anahtar)
            {
                sifreliMetin = sifreliMetin.ToLower().Replace(" ", "");
                anahtar = anahtar.ToLower().Replace(" ", "");
                string sonuc = "";

                for (int i = 0; i < sifreliMetin.Length; i++)
                {
                    char sifreliHarf = sifreliMetin[i];
                    char anahtarHarf = anahtar[i % anahtar.Length];

                    int sifreIndex = Alfabe.IndexOf(sifreliHarf);
                    int anahtarIndex = Alfabe.IndexOf(anahtarHarf);

                    if (sifreIndex == -1 || anahtarIndex == -1)
                        return $"Geçersiz karakter: '{sifreliHarf}' veya '{anahtarHarf}'";

                    int orijinalIndex = (sifreIndex - anahtarIndex + Alfabe.Length) % Alfabe.Length;
                    sonuc += Alfabe[orijinalIndex];
                }

                return sonuc;
            }
        }
        public class Yerdegistirme 
        {
            private const string Alfabe = "abcçdefgğhıijklmnoöprsştuüvyz";
            private const string sifre = "dafszcvertyıuopğüşilçökmjnhgb";
            public string Sifrele(string metin, string anahtar = null)
            {

                metin = metin.ToLower().Replace(" ", "");
                string sonuc = "";
                foreach (var i in metin)
                {
                    int idx = Alfabe.IndexOf(i);
                    if (idx >= 0)
                    {
                        char deger = sifre[idx];
                        sonuc += deger;
                    }
                    else
                    {
                        sonuc += "";
                    }


                }
                return sonuc;


            }
            public string Coz(string sifreliMetin, string anahtar = null)
            {
                sifreliMetin = sifreliMetin.ToLower().Replace(" ", "");

                string sonuc = "";
                foreach (var i in sifreliMetin)
                {
                    int idx = sifre.IndexOf(i);
                    char deger = Alfabe[idx];
                    sonuc += deger;

                }
                return sonuc;


            }
        }
        public class Zigzag 
        {
            private const string Alfabe = "abcçdefgğhıijklmnoöprsştuüvyz";
            public string Sifrele(string metin, string anahtar)
            {
                if (!int.TryParse(anahtar, out int satirSayisi) || satirSayisi < 2)
                    return "Geçerli bir satır sayısı girin (örn: 3)";

                metin = metin.Replace(" ", "").ToLower();
                string newmetin = "";

                foreach (char harf in metin)
                {
                    int idex = Alfabe.IndexOf(harf);
                    if (idex >= 0)
                    {
                        newmetin += harf;

                    }
                    else
                    {
                        newmetin += "";
                    }
                }


                StringBuilder[] satirlar = new StringBuilder[satirSayisi];
                for (int i = 0; i < satirSayisi; i++)
                    satirlar[i] = new StringBuilder();

                int satir = 0;
                bool asagi = true;

                foreach (char harf in newmetin)
                {
                    satirlar[satir].Append(harf);

                    if (asagi)
                    {
                        if (satir < satirSayisi - 1)
                            satir++;
                        else
                        {
                            asagi = false;
                            satir--;
                        }
                    }
                    else
                    {
                        if (satir > 0)
                            satir--;
                        else
                        {
                            asagi = true;
                            satir++;
                        }
                    }
                }

                StringBuilder sonuc = new StringBuilder();
                foreach (var s in satirlar)
                    sonuc.Append(s);

                return sonuc.ToString();
            }
            public string Coz(string sifreliMetin, string anahtar)
            {
                if (!int.TryParse(anahtar, out int satirSayisi) || satirSayisi < 2)
                    return "Geçerli bir satır sayısı girin (örn: 3)";

                sifreliMetin = sifreliMetin.ToLower().Replace(" ", "");
                int uzunluk = sifreliMetin.Length;
                int[] satirSirasi = new int[uzunluk];


                int satir = 0;
                bool asagi = true;
                for (int i = 0; i < uzunluk; i++)
                {
                    satirSirasi[i] = satir;

                    if (asagi)
                    {
                        if (satir < satirSayisi - 1)
                            satir++;
                        else
                        {
                            asagi = false;
                            satir--;
                        }
                    }
                    else
                    {
                        if (satir > 0)
                            satir--;
                        else
                        {
                            asagi = true;
                            satir++;
                        }
                    }
                }

                //her bir satırın kaç elemanlı olduğunu tutuyoruz
                int[] satirUzunluklari = new int[satirSayisi];
                foreach (int s in satirSirasi)
                    satirUzunluklari[s]++;

                //her bir satırın başlangıç indexini buluyoruz
                int[] baslangiclar = new int[satirSayisi];
                for (int i = 1; i < satirSayisi; i++)
                    baslangiclar[i] = baslangiclar[i - 1] + satirUzunluklari[i - 1];


                char[] sonuc = new char[uzunluk];
                int[] satirIndex = new int[satirSayisi];

                for (int i = 0; i < uzunluk; i++)
                {
                    int s = satirSirasi[i];
                    int idx = baslangiclar[s] + satirIndex[s]++;
                    sonuc[i] = sifreliMetin[idx];
                }

                return new string(sonuc);
            }
        }

    }
}
